using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queues.Models
{
    public class Request
    {
       
        public string message { get; set; }
        public int Id { get; set; }
        public int user_Id { get; set; }
        public Request(string m,int d,int uid)
        {
            this.message = m;
            this.user_Id = uid;
            this.Id = d;
        }
        public Request()
        {

        }
    }
}