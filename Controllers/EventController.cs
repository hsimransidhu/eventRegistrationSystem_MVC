 using ERM_BLL;
using ERM_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERS.Controllers
{
    /// <summary>
    /// This controller manages CRUD operations for events.
    /// </summary>
    public class EventController : Controller
    {
        /// <summary>
        /// Displays a list of all events.
        /// </summary>
        public ActionResult Index()
        {
            EventService eventService = new EventService();
            var events = eventService.GetAllEvents();
            return View(events);
        }

        /// <summary>
        /// Displays details of a specific event.
        /// </summary>
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// Renders a form to create a new event.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles form submission for creating a new event.
        /// </summary>
        [HttpPost]
        public ActionResult Create(Event e)
        {
            try
            {
                EventService eventService = new EventService();
                if (eventService.AddEventService(e))
                {
                    ViewBag.Message = "Event added successfully";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Renders a form to edit an existing event.
        /// </summary>
        public ActionResult Edit(int id)
        {
            EventService eventService = new EventService();
            var Eventt = eventService.GetAllEvents().Find(e => e.EventId == id);
            return View(Eventt);
        }

        /// <summary>
        /// Handles form submission for editing an event.
        /// </summary>
        [HttpPost]
        public ActionResult Edit(Event e)
        {
            try
            {
                EventService eventService = new EventService();
                if (eventService.UpdateEventService(e))
                {
                    ViewBag.Message = "Event updated successfully";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Deletes the specified event.
        /// </summary>
        public ActionResult Delete(int id)
        {
            EventService eventService = new EventService();
            if (eventService.DeleteEventService(id))
            {
                ViewBag.Message = "Event deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Event not deleted";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Handles form submission for deleting an event.
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
