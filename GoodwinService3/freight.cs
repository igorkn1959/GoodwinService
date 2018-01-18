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
    
    public partial class freight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public freight()
        {
            this.freight1 = new HashSet<freight>();
            this.order = new HashSet<order>();
        }
    
        public int inc { get; set; }
        public string name { get; set; }
        public string lname { get; set; }
        public int trantype { get; set; }
        public int partner { get; set; }
        public int source { get; set; }
        public Nullable<int> srcport { get; set; }
        public Nullable<System.DateTime> srctime { get; set; }
        public int target { get; set; }
        public Nullable<int> trgport { get; set; }
        public Nullable<System.DateTime> trgtime { get; set; }
        public Nullable<short> days { get; set; }
        public string flydays { get; set; }
        public Nullable<short> stopsale { get; set; }
        public Nullable<int> back { get; set; }
        public Nullable<short> childage { get; set; }
        public Nullable<short> weight { get; set; }
        public Nullable<bool> commission { get; set; }
        public Nullable<bool> onlyback { get; set; }
        public Nullable<bool> onlycompany { get; set; }
        public Nullable<int> tpartner { get; set; }
        public bool isback { get; set; }
        public bool isarchive { get; set; }
        public bool enablealtfreight { get; set; }
        public bool anybackfreight { get; set; }
        public int ftype { get; set; }
        public byte[] stamp { get; set; }
        public int printtype { get; set; }
        public bool islocal { get; set; }
        public Nullable<int> color { get; set; }
        public Nullable<int> currency { get; set; }
        public Nullable<int> stopeditdays { get; set; }
        public string note { get; set; }
        public Nullable<int> earlydep_service { get; set; }
        public Nullable<System.DateTime> earlydep_time { get; set; }
        public Nullable<int> latedep_service { get; set; }
        public Nullable<System.DateTime> latedep_time { get; set; }
        public Nullable<int> mixairline_service { get; set; }
        public string internal_note { get; set; }
        public Nullable<System.DateTime> latearrival_time { get; set; }
        public Nullable<int> backfreight_service { get; set; }
        public Nullable<int> freight_for_supplement { get; set; }
        public short weight_infant { get; set; }
    
        public virtual currency currency1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<freight> freight1 { get; set; }
        public virtual freight freight2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
        public virtual service service { get; set; }
        public virtual service service1 { get; set; }
        public virtual service service2 { get; set; }
        public virtual service service3 { get; set; }
        public virtual partner partner1 { get; set; }
        public virtual partner partner2 { get; set; }
    }
}
