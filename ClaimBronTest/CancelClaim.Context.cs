﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClaimBronTest
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class kompasEntities : DbContext
    {
        public kompasEntities()
            : base("name=kompasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int up_rlock(Nullable<int> uS_CODE, string tB_NAME, Nullable<int> tB_UCODE, Nullable<int> rEC_CODE)
        {
            var uS_CODEParameter = uS_CODE.HasValue ?
                new ObjectParameter("US_CODE", uS_CODE) :
                new ObjectParameter("US_CODE", typeof(int));
    
            var tB_NAMEParameter = tB_NAME != null ?
                new ObjectParameter("TB_NAME", tB_NAME) :
                new ObjectParameter("TB_NAME", typeof(string));
    
            var tB_UCODEParameter = tB_UCODE.HasValue ?
                new ObjectParameter("TB_UCODE", tB_UCODE) :
                new ObjectParameter("TB_UCODE", typeof(int));
    
            var rEC_CODEParameter = rEC_CODE.HasValue ?
                new ObjectParameter("REC_CODE", rEC_CODE) :
                new ObjectParameter("REC_CODE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("up_rlock", uS_CODEParameter, tB_NAMEParameter, tB_UCODEParameter, rEC_CODEParameter);
        }
    
        public virtual int up_unlock(Nullable<int> uS_CODE, string tB_NAME, Nullable<int> tB_UCODE, Nullable<int> rEC_CODE)
        {
            var uS_CODEParameter = uS_CODE.HasValue ?
                new ObjectParameter("US_CODE", uS_CODE) :
                new ObjectParameter("US_CODE", typeof(int));
    
            var tB_NAMEParameter = tB_NAME != null ?
                new ObjectParameter("TB_NAME", tB_NAME) :
                new ObjectParameter("TB_NAME", typeof(string));
    
            var tB_UCODEParameter = tB_UCODE.HasValue ?
                new ObjectParameter("TB_UCODE", tB_UCODE) :
                new ObjectParameter("TB_UCODE", typeof(int));
    
            var rEC_CODEParameter = rEC_CODE.HasValue ?
                new ObjectParameter("REC_CODE", rEC_CODE) :
                new ObjectParameter("REC_CODE", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("up_unlock", uS_CODEParameter, tB_NAMEParameter, tB_UCODEParameter, rEC_CODEParameter);
        }
    }
}
