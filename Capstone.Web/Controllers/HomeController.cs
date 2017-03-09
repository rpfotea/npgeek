﻿using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INpGeekDAL npGeekDal;
        private readonly IWeatherDAL weatherDal;

        public HomeController(INpGeekDAL npGeekDal, IWeatherDAL weatherDal)
        {
            this.npGeekDal = npGeekDal;
            this.weatherDal = weatherDal;
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


        //public ActionResult Forecast(string id)
        //{
        //    List<Weather> weather = new List<Weather>();
        //    weather = weatherDal.GetWeathers(id);
        //    return View("Forecast", weather);

        //}
        [HttpGet]
        public ActionResult Forecast(string parkId)

        {
            string unit;
            if (Session["TempUnit"] != null)
            {
                unit = Session["TempUnit"] as string;

            }
            else
            {
                unit = "fahrenheit";
            }
            Session["TempUnit"] = unit;
            return View("Forecast", weatherDal.GetWeathers(parkId));

        }
        [HttpPost]
        public ActionResult Forecast(string tempUnit)
        {
            Session["TempUnit"] = tempUnit;
            Redirect("Forecast");
        }

    }
}
