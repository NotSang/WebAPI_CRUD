using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_CRUD.Models
{
    public class CustomerViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

    }
}