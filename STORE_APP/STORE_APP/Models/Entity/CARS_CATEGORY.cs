//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STORE_APP.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CARS_CATEGORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARS_CATEGORY()
        {
            this.APPOINTMENT = new HashSet<APPOINTMENT>();
            this.CARS = new HashSet<CARS>();
        }

        public int CAR_CATEGORY_ID { get; set; }
        [Required(ErrorMessage = "Category should not be empty")]
        public string CAR_CATEGORY_NAME { get; set; }
        [Required(ErrorMessage = "Activity should not be empty")]
        [Range(0, 1, ErrorMessage = "Activity should not be 0,1")]
        public Nullable<int> IS_ACTIV { get; set; }
        public Nullable<System.DateTime> C_DATE { get; set; }
        public Nullable<System.DateTime> U_DATE { get; set; }
        public string CAR_IMG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPOINTMENT> APPOINTMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARS> CARS { get; set; }
    }

}