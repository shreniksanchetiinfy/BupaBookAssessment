//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace BupaBookAPI.Models
{
    public class Book
    {
        private string _name;
        private string _type;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Book(string paramName, string paramType)
        {
            name  = paramName;
            type = paramType;

        }
    }
}