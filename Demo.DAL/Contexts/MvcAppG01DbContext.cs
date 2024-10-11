﻿using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MvcAppG01DbContext:IdentityDbContext<ApplictionUser>
    {
        public MvcAppG01DbContext(DbContextOptions<MvcAppG01DbContext> options) : base(options) { } 
      //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // =>
        //    optionsBuilder.UseSqlServer("Server=.;Database=MvcAppG01Db;Trusted_Connection=true;");
        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
     

    }
}
