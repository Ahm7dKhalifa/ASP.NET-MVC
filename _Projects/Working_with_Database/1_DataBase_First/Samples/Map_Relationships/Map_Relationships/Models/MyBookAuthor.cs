using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map_Relationships.Models
{
    public class MyBookAuthor
    {

        public int Pk_Book_Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public int Pk_Author_Id { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public int Fk_Book_Id { get; set; }
    }
}