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
    
    public partial class people
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public people()
        {
            this.opeople = new HashSet<opeople>();
        }
    
        public int inc { get; set; }
        public Nullable<int> claim { get; set; }
        public string name { get; set; }
        public string lname { get; set; }
        public string human { get; set; }
        public Nullable<System.DateTime> born { get; set; }
        public string pserie { get; set; }
        public string pnumber { get; set; }
        public Nullable<int> dischum { get; set; }
        public Nullable<short> index { get; set; }
        public string peopmemo { get; set; }
        public Nullable<int> tourist { get; set; }
        public string address { get; set; }
        public string phprefix { get; set; }
        public string phones { get; set; }
        public string faxes { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> givendate { get; set; }
        public Nullable<System.DateTime> receiveddate { get; set; }
        public Nullable<System.DateTime> pvalid { get; set; }
        public Nullable<System.DateTime> pissue { get; set; }
        public string pgiven { get; set; }
        public int state { get; set; }
        public int placeofborn { get; set; }
        public bool male { get; set; }
        public int vstatus { get; set; }
        public Nullable<System.DateTime> visaforminputed { get; set; }
        public Nullable<System.DateTime> visareceiveddate { get; set; }
        public string anote { get; set; }
        public string pnote { get; set; }
        public Nullable<System.DateTime> passportgivendate { get; set; }
        public string form_visa_number { get; set; }
        public Nullable<System.DateTime> fulltakendate { get; set; }
        public Nullable<System.DateTime> prepareddate { get; set; }
        public Nullable<System.DateTime> realreceiveddate { get; set; }
        public Nullable<System.DateTime> visaexpiredate { get; set; }
        public string fplaceofborn { get; set; }
        public string inn { get; set; }
        public bool visareceived { get; set; }
        public bool fulltakendoc { get; set; }
        public bool prepareddoc { get; set; }
        public bool givendoc { get; set; }
        public bool receiveddoc { get; set; }
        public string visaform { get; set; }
        public Nullable<System.DateTime> fingerprintdate { get; set; }
        public Nullable<int> town_visacenter { get; set; }
        public string original_name { get; set; }
        public string original_lname { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<opeople> opeople { get; set; }
        public virtual claim claim1 { get; set; }
    }
}
