using Capstone.Web.DAL;
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
        private readonly ISurveyDAL surveyDal;

        public HomeController(INpGeekDAL npGeekDal, IWeatherDAL weatherDal, ISurveyDAL surveyDal)
        {
            this.npGeekDal = npGeekDal;
            this.weatherDal = weatherDal;
            this.surveyDal = surveyDal;
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


        public ActionResult Forecast(string id)
        {
            List<Weather> weather = new List<Weather>();
            weather = weatherDal.GetWeathers(id);
            return View("Forecast", weather);

        }
        //[HttpGet]
        //public ActionResult Forecast(string parkId)

        //{
        //    string unit;
        //    if (Session["TempUnit"] != null)
        //    {
        //        unit = Session["TempUnit"] as string;

        //    }
        //    else
        //    {
        //        unit = "fahrenheit";
        //    }
        //    Session["TempUnit"] = unit;

        //    if (unit == "fahrenheit")
        //    {
        //        return View("Forecast", weatherDal.GetWeathers(parkId));
        //    }
        //    else
        //    {


        //    }
        //}
        //[HttpPost]
        //    public ActionResult Forecast(string tempUnit)
        //    {
        //        Session["TempUnit"] = tempUnit;
        //        Redirect("Forecast", weatherDal.GetWeathers(parkId)));
        //    }


        //  --------------------------------Survey----------------------------------------------

        [HttpGet]
        public ActionResult Survey()
        {
            return View("Survey");
        }

        [HttpPost]
        public ActionResult Survey(Survey survey)
        {
            surveyDal.SaveSurvey(survey);

            return RedirectToAction("SummarySurveys");
        }

        [HttpGet]
        public ActionResult SummarySurveys()
        {
            return View("SummarySurveys");
        }

    }
}
