﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkLoskutova.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class loskutEntities : DbContext
    {
        public loskutEntities()
            : base("name=loskutEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public static loskutEntities GetContext()
        {
            return new loskutEntities();
        }

                public virtual DbSet<Books> Books { get; set; }
                public virtual DbSet<Genres> Genres { get; set; }
    }
}
