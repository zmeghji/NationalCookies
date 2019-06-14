﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalCookies.Data.Interfaces
{
    public interface ICookieService
    {
        void ClearCache();
        List<Cookie> GetAllCookies();
    }
}
