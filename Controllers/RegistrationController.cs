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
    // Check if eventName is null
    if (string.IsNullOrEmpty(eventName))
    {
        // Fetch event name if not provided
        EventService eventService = new EventService();
        var events = eventService.GetAllEvents();
        var selectedEvent = events.FirstOrDefault(e => e.EventId == eventId);
        if (selectedEvent != null)
            eventName = selectedEvent.EventName;
    }

    // Retrieve registrations for the specified event
    RegistrationService registrationService = new RegistrationService();
    var registrations = registrationService.GetAllRegistrations(eventId);

    // Store eventId and eventName in TempData
    TempData["eventId"] = eventId;
    TempData["eventName"] = eventName;

    return View(registrations);
}

public ActionResult Create()
{
    Registration registration = null;
    int eventId = (int)TempData["eventId"];

    // Create new registration if eventId is not 0
    if (eventId != 0)
        registration = new Registration() { EventId = eventId, RegistrationDate = null };

    // Fetch all events
    EventService eventService = new EventService();
    var events = eventService.GetAllEvents();

    // Create SelectListItem for each event
    var eventItems = events.Select(e => new SelectListItem
    {
        Text = e.EventName,
        Value = e.EventId.ToString()
    }).ToList();

    // Store eventItems in TempData
    TempData["eventsList"] = eventItems;

    // Retain TempData
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
