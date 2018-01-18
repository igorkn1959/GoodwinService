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
    
    public partial class claim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public claim()
        {
            this.external_document = new HashSet<external_document>();
            this.opeople = new HashSet<opeople>();
            this.order = new HashSet<order>();
            this.people = new HashSet<people>();
            this.pdetail = new HashSet<pdetail>();
        }
    
        public int inc { get; set; }
        public Nullable<int> partner { get; set; }
        public Nullable<System.DateTime> cdate { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> reserve { get; set; }
        public int tour { get; set; }
        public Nullable<short> rcount { get; set; }
        public Nullable<System.DateTime> pdate { get; set; }
        public Nullable<System.DateTime> datebeg { get; set; }
        public Nullable<System.DateTime> dateend { get; set; }
        public string ndog { get; set; }
        public string note { get; set; }
        public Nullable<decimal> commission { get; set; }
        public Nullable<short> penalty { get; set; }
        public bool sent { get; set; }
        public bool confirmed { get; set; }
        public bool attention { get; set; }
        public bool partpayment { get; set; }
        public Nullable<short> user { get; set; }
        public bool raw { get; set; }
        public bool edited { get; set; }
        public Nullable<System.DateTime> edate { get; set; }
        public Nullable<System.DateTime> adate { get; set; }
        public Nullable<System.DateTime> idate { get; set; }
        public bool access { get; set; }
        public int spog { get; set; }
        public Nullable<short> author { get; set; }
        public Nullable<decimal> penaltysum { get; set; }
        public int currency { get; set; }
        public bool mark { get; set; }
        public Nullable<int> owner { get; set; }
        public Nullable<int> mediator { get; set; }
        public Nullable<decimal> mediatorsum { get; set; }
        public System.Guid guid { get; set; }
        public Nullable<bool> exported { get; set; }
        public Nullable<bool> notforexport { get; set; }
        public string comment { get; set; }
        public string privatecomment { get; set; }
        public Nullable<System.DateTime> confirmeddate { get; set; }
        public Nullable<decimal> earlycommission { get; set; }
        public Nullable<decimal> sumcommission { get; set; }
        public Nullable<decimal> sumearlycommission { get; set; }
        public Nullable<int> currencycommission { get; set; }
        public Nullable<int> currencyearlycommission { get; set; }
        public Nullable<int> letter { get; set; }
        public string id { get; set; }
        public bool unread { get; set; }
        public bool issued { get; set; }
        public Nullable<System.DateTime> issueddate { get; set; }
        public Nullable<System.DateTime> cdatetime { get; set; }
        public Nullable<int> confirmationtimehh { get; set; }
        public Nullable<bool> invoiceneed { get; set; }
        public Nullable<System.DateTime> rdate { get; set; }
        public Nullable<int> reason { get; set; }
        public Nullable<short> iconfirmed { get; set; }
        public Nullable<System.DateTime> iconfirmeddate { get; set; }
        public Nullable<int> buyer { get; set; }
        public Nullable<System.DateTime> canceldate { get; set; }
        public string contact { get; set; }
        public Nullable<bool> ienable2send { get; set; }
        public int ctype { get; set; }
        public Nullable<System.DateTime> requestcanceldate { get; set; }
        public Nullable<int> partpass { get; set; }
        public bool locked { get; set; }
        public Nullable<System.DateTime> pdate1 { get; set; }
        public Nullable<int> currencyclaim { get; set; }
        public Nullable<int> cgroup { get; set; }
        public string partnercomment { get; set; }
        public Nullable<int> ebooking { get; set; }
        public bool edocument { get; set; }
        public Nullable<int> nights { get; set; }
        public Nullable<int> phys_buyer { get; set; }
        public bool accesspay { get; set; }
        public Nullable<int> status_full { get; set; }
        public int confirmationstatus { get; set; }
    
        public virtual currency currency1 { get; set; }
        public virtual currency currency2 { get; set; }
        public virtual currency currency3 { get; set; }
        public virtual currency currency4 { get; set; }
        public virtual reserve reserve1 { get; set; }
        public virtual status status1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<external_document> external_document { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<opeople> opeople { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<people> people { get; set; }
        public virtual partner partner1 { get; set; }
        public virtual partner partner2 { get; set; }
        public virtual partner partner3 { get; set; }
        public virtual partner partner4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pdetail> pdetail { get; set; }
    }
}
