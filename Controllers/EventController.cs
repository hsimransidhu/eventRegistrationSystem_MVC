using ERM_BLL;
using ERM_ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERS.Controllers
{
    public class EventController : Controller
    {
    
        public ActionResult Index()
        {
           
            EventService eventService = new EventService();

            var events = eventService.GetAllEvents();

           
            return View(events);
        }

   
        public ActionResult Details(int id)
        {
            return View();
        }

 
        public ActionResult Create()
        {
            return View();
        }
 
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
 
        public ActionResult Edit(int id)
        { 
            EventService eventService = new EventService();

            var Eventt = eventService.GetAllEvents().Find(e => e.EventId == id);
 
            return View(Eventt);
        }
 
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
