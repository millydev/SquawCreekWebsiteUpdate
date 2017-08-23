using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SquawCreekWebSite.Models;

namespace SquawCreekWebSite.DB
{
    public class EventsDB : DatabaseLayer
    {
        public List<Models.Event> SelectAll()
        {
            string query = "SELECT * FROM calendarevents";

            //Create a list to store the result
            List<Event> list = new List<Event>();

            //Open connection
            if (this.OpenDBConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dbConnection);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        list.Add(new Event((int)dataReader["calendarEventsId"], (string)dataReader["eventName"], (DateTime)dataReader["startDate"], (DateTime)dataReader["endDate"]));
                    }

                    dataReader.Close();

                    this.CloseDBConnection();
                }
                catch (MySqlException ex)
                {
                    return list;
                }

                return list;
            }
            else
            {
                return list;
            }
        }

        public List<Models.Event> SelectEventsByDates(DateTime start, DateTime end)
        {
            string query = "SELECT * FROM calendarevents where startDate >= @startDate and endDate <= @endDate";

            //Create a list to store the result
            List<Event> list = new List<Event>();

            //Open connection
            if (this.OpenDBConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                    cmd.Parameters.AddWithValue("@startDate", start);
                    cmd.Parameters.AddWithValue("@endDate", end);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list.Add(new Event((int)dataReader["calendarEventsId"], (string)dataReader["eventName"], (DateTime)dataReader["startDate"], (DateTime)dataReader["endDate"]));
                    }
                    dataReader.Close();

                    this.CloseDBConnection();
                }
                catch (MySqlException ex)
                {
                    return list;
                }

                return list;
            }
            else
            {
                return list;
            }
        }

        public long InsertEvent(Event ev)
        {
            long newID = -1;
            //Open connection
            if (this.OpenDBConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "INSERT INTO calendarevents(eventName, startDate, endDate) VALUES(@description, @startDate, @endDate)";
                    cmd.Parameters.AddWithValue("@description", ev.text);
                    cmd.Parameters.AddWithValue("@startDate", ev.start);
                    cmd.Parameters.AddWithValue("@endDate", ev.end);
                    cmd.ExecuteNonQuery();
                    newID = cmd.LastInsertedId;
                    this.CloseDBConnection();
                    return newID;
                }
                catch (MySqlException ex)
                {
                    return newID;
                }
            }
            else
            {
                return newID;
            }
        }

        public bool UpdateEventDates(Event ev)
        {
            //Open connection
            if (this.OpenDBConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = dbConnection.CreateCommand();
                    cmd.CommandText = "UPDATE calendarevents SET startDate=@startDate, endDate=@endDate WHERE calendarEventsId=@id";
                    cmd.Parameters.AddWithValue("@id", ev.id);
                    cmd.Parameters.AddWithValue("@startDate", ev.start);
                    cmd.Parameters.AddWithValue("@endDate", ev.end);
                    cmd.ExecuteNonQuery();
                    CloseDBConnection();
                    return true;
                }
                catch (MySqlException ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}