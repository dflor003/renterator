//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Renterator.DataAccess.Model
{
    using System.Collections.Generic;
    
    public partial class BillType
    {
        public BillType()
        {
            this.Bills = new HashSet<Bill>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
