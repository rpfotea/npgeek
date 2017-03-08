﻿using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public interface INpGeekDAL
    {
        List<Park> GetParks();
        Park GetParkForDisplay(string id);
    }
}