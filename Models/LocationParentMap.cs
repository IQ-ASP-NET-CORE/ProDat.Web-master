using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProDat.Web2.Models
{
    public class LocationParentMap : EntityTypeConfiguration<Location>
    {
        public LocationParentMap() {
            HasKey(p => p.LocationID);

            Property(p => p.LocationID)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasOptional(p => p.ParentLocations)
            //    .WithMany(p => p.Children)
            //    .HasForeignKey(p => p.ParentLocation)
            //    .WillCascadeOnDelete(false);
        }

    }
    
}
