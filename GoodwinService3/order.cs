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
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.external_document = new HashSet<external_document>();
            this.opeople = new HashSet<opeople>();
        }
    
        public int inc { get; set; }
        public Nullable<int> claim { get; set; }
        public Nullable<int> reserve { get; set; }
        public Nullable<int> hotel { get; set; }
        public Nullable<int> freight { get; set; }
        public Nullable<int> service { get; set; }
        public Nullable<int> insure { get; set; }
        public Nullable<int> visapr { get; set; }
        public Nullable<int> partner { get; set; }
        public Nullable<int> room { get; set; }
        public Nullable<int> @class { get; set; }
        public Nullable<int> htplace { get; set; }
        public Nullable<int> frplace { get; set; }
        public Nullable<int> meal { get; set; }
        public Nullable<System.DateTime> datebeg { get; set; }
        public Nullable<System.DateTime> dateend { get; set; }
        public Nullable<short> rcount { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> net { get; set; }
        public int ccurrency { get; set; }
        public int ncurrency { get; set; }
        public bool commission { get; set; }
        public bool sent { get; set; }
        public short confirmed { get; set; }
        public Nullable<short> index { get; set; }
        public Nullable<short> routeindex { get; set; }
        public int spos { get; set; }
        public int spotype { get; set; }
        public Nullable<decimal> sumcom { get; set; }
        public Nullable<System.DateTime> timebeg { get; set; }
        public Nullable<System.DateTime> timeend { get; set; }
        public int cure { get; set; }
        public int status { get; set; }
        public Nullable<bool> AddInfant { get; set; }
        public string netdetail { get; set; }
        public string pricedetail { get; set; }
        public string id { get; set; }
        public bool calcafter { get; set; }
        public int tariff { get; set; }
        public Nullable<int> shotel { get; set; }
        public Nullable<int> stown { get; set; }
        public Nullable<int> starget { get; set; }
        public Nullable<int> smeal { get; set; }
        public Nullable<int> sfrclass { get; set; }
        public Nullable<int> sairline { get; set; }
        public Nullable<decimal> penalty { get; set; }
        public Nullable<bool> billreceived { get; set; }
        public Nullable<System.DateTime> brdate { get; set; }
        public Nullable<decimal> kickback { get; set; }
        public string note { get; set; }
        public Nullable<int> sroom { get; set; }
        public string order_info { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> TIMELIMIT { get; set; }
        public Nullable<int> fare { get; set; }
        public Nullable<int> room_reserve { get; set; }
        public bool fixprice { get; set; }
        public Nullable<decimal> partner_commission { get; set; }
        public Nullable<decimal> cnet { get; set; }
        public Nullable<int> owner { get; set; }
        public Nullable<System.DateTime> timelimit_our { get; set; }
        public Nullable<int> cncurrency { get; set; }
        public string cnetdetail { get; set; }
        public Nullable<int> srcport { get; set; }
        public Nullable<int> trgport { get; set; }
        public Nullable<int> frplacement_group { get; set; }
        public Nullable<decimal> anet { get; set; }
    
        public virtual currency currency { get; set; }
        public virtual currency currency1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<external_document> external_document { get; set; }
        public virtual freight freight1 { get; set; }
        public virtual hotel hotel1 { get; set; }
        public virtual hotel hotel2 { get; set; }
        public virtual htplace htplace1 { get; set; }
        public virtual meal meal1 { get; set; }
        public virtual meal meal2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<opeople> opeople { get; set; }
        public virtual room room1 { get; set; }
        public virtual room room2 { get; set; }
        public virtual room room3 { get; set; }
        public virtual status status1 { get; set; }
        public virtual service service1 { get; set; }
        public virtual insure insure1 { get; set; }
        public virtual visapr visapr1 { get; set; }
        public virtual reserve reserve1 { get; set; }
        public virtual @class class1 { get; set; }
        public virtual @class class2 { get; set; }
        public virtual currency currency11 { get; set; }
        public virtual claim claim1 { get; set; }
        public virtual partner partner1 { get; set; }
        public virtual partner partner2 { get; set; }
        public virtual partner partner3 { get; set; }
    }
}