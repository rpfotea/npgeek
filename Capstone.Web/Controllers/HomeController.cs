using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INpGeekDAL npGeekDal;

        public HomeController(INpGeekDAL npGeekDal)
        {
            this.npGeekDal = npGeekDal;
        }

       
        public ActionResult Index()
        {
            return View("Index", npGeekDal.GetParks());
        }

        public ActionResult Detail(string id)
        {
            // var park = GetParkForDisplay().Find(park => park.ParkCode == id.ToUpper());
            Park park = npGeekDal.GetParkForDisplay(id);
            return View("Detail", park);
        }

        
    }
}