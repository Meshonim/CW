﻿using DalToWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Configurations
{
    public class ExemplarConfiguration : EntityTypeConfiguration<Exemplar>
    {
        public ExemplarConfiguration()
        {
            Property(h => h.ExemplarCost)
                .IsRequired();

            Property(h => h.EditionId)
                .IsRequired();

            ToTable("Exemplar");
        }
    }
}
