//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Map_Relationships.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Author
    {
        public int Pk_Author_Id { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public Nullable<int> Fk_Book_Id { get; set; }
    
        public virtual Book Book { get; set; }
    }
}