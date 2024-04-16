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

    public partial class EMPLOYEES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEES()
        {
            this.SALES_HISTORY = new HashSet<SALES_HISTORY>();
        }

        public int EMPLOYEE_ID { get; set; }
        public Nullable<int> DEPARTMENT_ID { get; set; }
        [Required(ErrorMessage = "Name should not be empty")]
        public string NAME { get; set; }
        [Required(ErrorMessage = "SurName should not be empty")]
        public string SURNAME { get; set; }
        public Nullable<System.DateTime> BIRT_DATE { get; set; }
        [Required(ErrorMessage = "Identity Number should not be empty")]
        public Nullable<int> IDENTIFY_NUMBER { get; set; }
        public Nullable<int> EMPLOYEE_POINT { get; set; }
        public Nullable<int> TOTAL_SERVICE { get; set; }
        [Required(ErrorMessage = "Activity should not be empty")]
        [Range(0, 1, ErrorMessage = "Activity should not be 0,1")]
        public Nullable<int> IS_ACTIV { get; set; }
        public Nullable<System.DateTime> C_DATE { get; set; }
        public Nullable<System.DateTime> U_DATE { get; set; }
        [Required(ErrorMessage = "Password should not be empty")]
        public string PASSWORD { get; set; }
        [Required(ErrorMessage = "E-Mail should not be empty")]
        public string EMAIL { get; set; }

        public virtual DEPARTMENTS DEPARTMENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SALES_HISTORY> SALES_HISTORY { get; set; }
    }
}