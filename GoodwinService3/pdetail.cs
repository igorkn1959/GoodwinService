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
    
    public partial class pdetail
    {
        public int inc { get; set; }
        public int payment { get; set; }
        public int claim { get; set; }
        public int currency { get; set; }
        public decimal pay { get; set; }
        public decimal rate { get; set; }
        public decimal realpay { get; set; }
        public Nullable<System.DateTime> adate { get; set; }
    
        public virtual claim claim1 { get; set; }
        public virtual currency currency1 { get; set; }
        public virtual payment payment1 { get; set; }
    }
}