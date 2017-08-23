using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SquawCreekWebSite.DB;
using SquawCreekWebSite.Models;
using System.Web;

namespace SquawCreekWebSite.Controllers
{
    /// <summary>
    /// The controller for the events actions
    /// </summary>
    [System.Web.Mvc.RoutePrefix("api/events")]
    public class EventsController : ApiController
    {
        List<Event> events = new List<Event>();
        
        /// <summary>
        /// GET restful service that returns all the events from the DB
        /// </summary>
        /// <returns></returns>
        public string GetAllEvents()
        {
            events = (new EventsDB()).SelectAll();
            return JsonConvert.SerializeObject(events);
        }

        /// <summary>
        /// GET restful service to filter the events based on the text
        /// The request comes in a string format
        /// </summary>
        /// <param name="id">The text based on wich we will filter the DB and return the vents that match the criteria</param>
        /// <returns></returns>
        public IHttpActionResult GetEvent(string id)
        {
            events = (new EventsDB()).SelectAll();
            var e = events.FirstOrDefault((p) => p.text == id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        /// <summary>
        /// POST resful service
        /// The request comes in a JSON format
        /// </summary>
        /// <param name="ev">Is an eventFilter object, that will have an action member, based on which the service will call different methods</param>
        /// <returns></returns>
        public IHttpActionResult PostEvents(EventFilter ev)
        {
            switch (ev.action)
            {
                case "create":
                    if (HttpContext.Current.Request.Cookies["sqkwebsite"] != null)
                        return Ok(CreateEvent(ev.text, ev.start, ev.end));
                    return Ok(-1);
                case "move":
                    if (HttpContext.Current.Request.Cookies["sqkwebsite"] != null)
                        return Ok(UpdateEvent(ev.id, ev.start, ev.end));
                    return Ok(-1);
                case "filter":
                    return Ok(FilterEventsByDate(ev.start, ev.end).ToList());
                default:
                    return Ok(-1);
            }
        }

        /// <summary>
        /// Used to create an event
        /// </summary>
        /// <param name="text">The text of the event</param>
        /// <param name="start">The event start date</param>
        /// <param name="end">The event end date</param>
        /// <returns></returns>       
        private Event CreateEvent(string text, DateTime start, DateTime end)
        {
            Event ev = new Event(-1, text, start, end);
            var id = (new EventsDB()).InsertEvent(ev);
            return new Event(id, text, start, end);
        }

        /// <summary>
        /// Used to update an event time
        /// </summary>
        /// <param name="id">The id of the event</param>
        /// <param name="start">The new startdate</param>
        /// <param name="end">The new enddate</param>
        /// <returns></returns>
        private bool UpdateEvent(long id, DateTime start, DateTime end)
        {
            Event ev = new Event(id, "", start, end);
            return (new EventsDB()).UpdateEventDates(ev);
        }

        /// <summary>
        /// Used to filter the events based based on th start and end dates
        /// 
        /// <param name="start">The event start date</param>
        /// <param name="end">The event end date</param>>
        /// <returns></returns>
        private IEnumerable<Event> FilterEventsByDate( DateTime start, DateTime end)
        {
            events = (new EventsDB()).SelectEventsByDates(start, end);
            return events;
        }

        /*
        [ResponseType(typeof(Event))]
        [System.Web.Mvc.Route]
        public IHttpActionResult PostEventFilter(EventFilter ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            events = (new DatabaseLayer()).SelectAll();
            var e = events.FirstOrDefault((p) => p.start == ev.start && p.end == ev.end);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }*/
    }

    /// <summary>
    /// The EventFilter class used for the POST call
    /// </summary>
    public class EventFilter
    {
        public string action { get; set; }
        public long id { get; set; }
        public string text { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
