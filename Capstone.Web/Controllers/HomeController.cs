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
        private readonly ISummarySurveysDAL summarySurveysDal;

        public HomeController(INpGeekDAL npGeekDal, IWeatherDAL weatherDal, ISurveyDAL surveyDal, ISummarySurveysDAL summarySurveysDal)
        {
            this.npGeekDal = npGeekDal;
            this.weatherDal = weatherDal;
            this.surveyDal = surveyDal;
            this.summarySurveysDal = summarySurveysDal;
        }


        public ActionResult Index()
        {
            return View("Index", npGeekDal.GetParks());
        }

        public ActionResult Detail(string id)
        {
            // var park = GetParkForDisplay().Find(park => park.ParkCode == id.ToUpper());
            Park park = npGeekDal.GetParkForDisplay(id);

            if(park != null)
            {
                return View("Detail", park);
            }
            else
            {
                return HttpNotFound();
            }
            
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
               //Weather Model from the ddatabase
               //model.TemperatureUnit = unit;
        //    
        //        return View("Forecast", model);
        
        //}
        //[HttpPost]
        //    public ActionResult Forecast(string tempUnit)
        //    {
        //        Session["TempUnit"] = tempUnit;
        //          return Redirect(Request.Referrer.ToString());
        //    }


        //  --------------------------------Survey----------------------------------------------

        [HttpGet]
        public ActionResult Survey()
        {
            List<Park> parks = npGeekDal.GetParks();
            Survey model = new Survey();
            foreach(Park park in parks)
            {
                model.ParksCode.Add(new SelectListItem() { Text = park.ParkName, Value = park.ParkCode });
            }
            return View("Survey", model);
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
            return View("SummarySurveys", summarySurveysDal.GetSummarySurveys());
        }

    }
}
