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
    
    public partial class PassportDetail
    {
        public int Pk_Passport_Id { get; set; }
        public string Passport_Number { get; set; }
        public Nullable<int> Fk_Person_Id { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
