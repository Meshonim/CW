using DalToWeb.Models;
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
    public class RuleConfiguration : EntityTypeConfiguration<Rule>
    {
        public RuleConfiguration()
        {
            Property(c => c.RuleValue)
                .IsRequired()
                .HasMaxLength(300);

            Property(c => c.RulePriority)
                .IsRequired();

            ToTable("Rule");
        }
    }
}
