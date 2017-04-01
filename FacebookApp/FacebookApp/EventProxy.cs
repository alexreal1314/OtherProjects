using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookApp
{
    public class EventProxy
    {
        public Event Event { get; set; }

        public string PictureNormalURL { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public EventProxy(Event i_Event)
        {
            Event = i_Event;
            PictureNormalURL = i_Event.PictureNormalURL;
            Description = (i_Event.Description == null) ? "No Description Availble" : i_Event.Description;
            Location = (i_Event.Location == null) ? "No Location Availble" : i_Event.Location;
        }

        public override string ToString()
        {
            DateTime eventStart = (DateTime)Event.StartTime;
            string dateStartStr = eventStart.ToString();
            return string.Format("{0}, Date: {1}, Owner: {2}", Event.Name, dateStartStr, Event.Owner.Name);      
        }     
    }
}
