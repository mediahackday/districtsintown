﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DistrictsInTown.DbModel
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class DistrictsInTownModelContainer : DbContext
{
    public DistrictsInTownModelContainer()
        : base("name=DistrictsInTownModelContainer")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Places> Places { get; set; }

}

}

