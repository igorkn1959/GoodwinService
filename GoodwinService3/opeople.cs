//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoodwinService3
{
    using System;
    using System.Collections.Generic;
    
    public partial class opeople
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public opeople()
        {
            this.external_document1 = new HashSet<external_document>();
        }
    
        public int inc { get; set; }
        public Nullable<int> claim { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<int> people { get; set; }
        public Nullable<int> common { get; set; }
        public bool wait { get; set; }
        public bool extrasettle { get; set; }
        public bool settled { get; set; }
        public string docum { get; set; }
        public bool distributed { get; set; }
        public Nullable<System.DateTime> ddate { get; set; }
        public string seat { get; set; }
        public Nullable<System.DateTime> odate { get; set; }
        public string link { get; set; }
        public Nullable<int> oinc { get; set; }
        public Nullable<int> placement_code { get; set; }
        public Nullable<int> external_document { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<external_document> external_document1 { get; set; }
        public virtual external_document external_document2 { get; set; }
        public virtual order order1 { get; set; }
        public virtual people people1 { get; set; }
        public virtual claim claim1 { get; set; }
    }
}
