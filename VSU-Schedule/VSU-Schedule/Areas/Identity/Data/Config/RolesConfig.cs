﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSU_Schedule.Areas.Identity.Data.Config
{
    public class RolesConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new[] {
                new IdentityRole("Admin"),
                new IdentityRole("User")
            });
        }
    }
}