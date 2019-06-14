using Microsoft.Extensions.Caching.Distributed;
using NationalCookies.Data.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NationalCookies.Data.Services
{
    public class CookieService : ICookieService
    {
        private CookieContext _context;
        private readonly IDistributedCache _cache;
        public CookieService(CookieContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public void ClearCache()
        {
            _cache.Remove("cookies");
        }
        public List<Cookie> GetAllCookies()
        {

            var cachedCookiesString = _cache.GetString("cookies");
            if (!string.IsNullOrWhiteSpace(cachedCookiesString))
            {
                return JsonConvert.DeserializeObject<List<Cookie>>(cachedCookiesString);
            }
            else
            {
                var cookies = _context.Cookies.ToList();

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(0, 0, 15));
                _cache.SetString("cookies",
                    JsonConvert.SerializeObject(cookies),
                    options
                    );
                return cookies;
            }
        }
    }
}
