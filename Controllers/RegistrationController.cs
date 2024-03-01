using ERM_BLL;
using ERM_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERS.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index(int eventId, string eventName)
        {
            if (eventName == null)
            {
                EventService es = new EventService();
                var events = es.GetAllEvents();
                eventName = events.FirstOrDefault(e => e.EventId == eventId).EventName;
            }
            RegistrationService registrationService = new RegistrationService();
            var registrations = registrationService.GetAllRegistrations(eventId);

            TempData["eventId"] = eventId;
            TempData["eventName"] = eventName;

            return View(registrations);
        }


        public ActionResult Create()
        {
            Registration registration = null;

            int eventId = (int)TempData["eventId"];

            if (eventId != 0)
                registration = new Registration() { EventId = eventId, RegistrationDate = null };

            EventService es = new EventService();
            var events = es.GetAllEvents();

            var eventItems = events.Select(e => new SelectListItem
            {
                Text = e.EventName,
                Value = e.EventId.ToString()
            }).ToList();
            TempData["eventsList"] = eventItems;

            TempData.Keep();

            return View(registration);
        }

        [HttpPost]
        public ActionResult Create(Registration registration)
        {
            RegistrationService registrationService = new RegistrationService();
            
            if (registrationService.AddRegistrationService(registration))
            {
                ViewBag.Message = "Registration is added successfully";
            }

            return View(registration);
        }


        public ActionResult Edit(int registrationId)
        {
            int eventId = (int)TempData["eventId"];

            RegistrationService registrationService = new RegistrationService();
            var registration = registrationService.GetAllRegistrations(eventId).Find(x => x.RegistrationId == registrationId);

    
            return View(registration);
        }

        [HttpPost]
        public ActionResult Edit(Registration registration)
        {
            RegistrationService registrationService = new RegistrationService();
            if (registrationService.UpdateRegistrationService(registration))
            {
                return RedirectToAction("Index", new { eventId = registration.EventId });
            }
            else
            {
                return View(registration);
            }
        }


        public ActionResult DeleteRegistration(int registrationId )
        {
          
            RegistrationService registrationService = new RegistrationService();

            if (registrationService.DeleteRegistrationService(registrationId))
            {
                ViewBag.Message = "Success";
            }
            return RedirectToAction("Index", new { eventId = TempData["EventId"] });
        }
    }

}
