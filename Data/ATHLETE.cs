//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ATHLETE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATHLETE()
        {
            this.RESULT = new HashSet<RESULT>();
            this.TRAINING_PLAN = new HashSet<TRAINING_PLAN>();
            this.ATHLETE1 = new HashSet<ATHLETE>();
            this.GYM1 = new HashSet<GYM>();
        }
    
        public decimal ID { get; set; }
        public string NAME { get; set; }
        public string BORN_IN { get; set; }
        public Nullable<System.DateTime> BORN_DATE { get; set; }
        public Nullable<System.DateTime> MEMBER_FROM { get; set; }
        public Nullable<decimal> HEIGHT { get; set; }
        public Nullable<decimal> WEIGHT { get; set; }
        public Nullable<decimal> TRAINER_ID { get; set; }
        public Nullable<decimal> FAV_EXERCISE { get; set; }
        public Nullable<decimal> GYM_ID { get; set; }
        public Nullable<bool> IS_PUNISHED { get; set; }
    
        public virtual EXERCISE EXERCISE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESULT> RESULT { get; set; }
        public virtual GYM GYM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAINING_PLAN> TRAINING_PLAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATHLETE> ATHLETE1 { get; set; }
        public virtual ATHLETE Trainer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GYM> GYM1 { get; set; }
    }
}
