using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SquawCreekWebSite.Models
{
    public class Event
    {
        public long id { get; set; }
        public string text { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public Event(long id, string text, DateTime start, DateTime end)
        {
            this.id = id;
            this.text = text;
            this.start = start;
            this.end = end;
        }
    }
}