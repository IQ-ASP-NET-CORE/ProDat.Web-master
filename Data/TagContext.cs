using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using ProDat.Web2.Models;
using ProDat.Web2.Models.ETL;
using ProDat.Web2.Models.SAPRequirements;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace ProDat.Web2.Data
{
    public partial class TagContext : DbContext
    {

        public TagContext(DbContextOptions<TagContext> options)
               : base(options)
        {
        }

        // what fields to view in UC1
        public virtual DbSet<ColumnSets> ColumnSets { get; set; }

        public virtual DbSet<History> History { get; set; }

        public virtual DbSet<TagView> TagView { get; set; }
        public virtual DbSet<TagViewColumns> TagViewColumns { get; set; }
        public virtual DbSet<TagViewColumnsUser> TagViewColumnsUser { get; set; }

        public virtual DbSet<SAPExportDetail> SAPExportDetail { get; set; }

        public virtual DbSet<UC2ViewColumns> UC2ViewColumns { get; set; }
        public virtual DbSet<UC2ViewColumnsUser> UC2ViewColumnsUser { get; set; }

        public virtual DbSet<SortField> SortField { get; set; }
        public virtual DbSet<PlannerPlant> PlannerPlant { get; set; }
        public virtual DbSet<CompanyCode> CompanyCode { get; set; }
        public virtual DbSet<WBSElement> WBSElement { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<ClassCharacteristics> ClassCharacteristics { get; set; }
        public virtual DbSet<ClassCharsTaskListHeader> ClassCharsTaskListHeader { get; set; }
        public virtual DbSet<MaintClassXEngDataCode> MaintClassXEngDataCode { get; set; }
        public virtual DbSet<CommClass> CommClass { get; set; }
        public virtual DbSet<CommSubSystem> CommSubSystem { get; set; }
        public virtual DbSet<CommZone> CommZone { get; set; }
        //public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<ControlKey> ControlKey { get; set; }
        //public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Doc> Doc { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<EngClass> EngClass { get; set; }
        public virtual DbSet<EngDataClass> EngDataClass { get; set; }
        public virtual DbSet<EngClassRequiredDocs> EngClassRequiredDocs { get; set; }

        public virtual DbSet<EngDataClassxEngDataCode> EngDataClassxEngDataCode { get; set; }
        public virtual DbSet<EngDataCode> EngDataCode { get; set; }
        public virtual DbSet<EngDataCodeDropDown> EngDataCodeDropDown { get; set; }

        public virtual DbSet<EngDisc> EngDisc { get; set; }
        //public virtual DbSet<EngPlant> EngPlant { get; set; }
        public virtual DbSet<EngStatus> EngStatus { get; set; }
        //public virtual DbSet<EngPlantSection> EngPlantSection { get; set; }

        public virtual DbSet<EnvZone> EnvZone { get; set; }
        public virtual DbSet<ExMethod> ExMethod { get; set; }
        public virtual DbSet<FlocXmaintItem> FlocXmaintItem { get; set; }
        public virtual DbSet<FlocXmaintLoad> FlocXmaintLoad { get; set; }
        public virtual DbSet<FlocXmaintQuery> FlocXmaintQuery { get; set; }
        public virtual DbSet<FlocXpmassembly> FlocXpmassembly { get; set; }
        public virtual DbSet<Ipf> Ipf { get; set; }

        public virtual DbSet<KeyList> KeyList { get; set; }
        public virtual DbSet<KeyListxEngClass> KeyListxEngClass { get; set; }
        public virtual DbSet<KeyListxEngDataCode> KeyListxEngDataCode { get; set; }
        public virtual DbSet<LoadTemplate> LoadTemplate { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationTypes> LocationTypes { get; set; }
        public virtual DbSet<MaintArea> MaintArea { get; set; }
        public virtual DbSet<MaintClass> MaintClass { get; set; }
        public virtual DbSet<MaintCriticality> MaintCriticality { get; set; }
        public virtual DbSet<MaintEdcCode> MaintEdcCode { get; set; }
        public virtual DbSet<MaintItem> MaintItem { get; set; }
        public virtual DbSet<MaintItemXmaintTaskListHeader> MaintItemXmaintTaskListHeader { get; set; }
        public virtual DbSet<MaintLoad> MaintLoad { get; set; }
        public virtual DbSet<MaintLocation> MaintLocation { get; set; }
        public virtual DbSet<MaintObjectType> MaintObjectType { get; set; }
        public virtual DbSet<MaintPackage> MaintPackage { get; set; }
        public virtual DbSet<MaintPlan> MaintPlan { get; set; }
        public virtual DbSet<MaintPlannerGroup> MaintPlannerGroup { get; set; }
        public virtual DbSet<MaintQuery> MaintQuery { get; set; }
        public virtual DbSet<MaintQueryNote> MaintQueryNote { get; set; }
        public virtual DbSet<MaintScePsReviewTeam> MaintScePsReviewTeam { get; set; }
        public virtual DbSet<MaintSortProcess> MaintSortProcess { get; set; }
        public virtual DbSet<MaintStrategy> MaintStrategy { get; set; }
        public virtual DbSet<MaintStructureIndicator> MaintStructureIndicator { get; set; }
        public virtual DbSet<MaintType> MaintType { get; set; }
        //public virtual DbSet<MaintTypeXMaintClass> MaintTypeXMaintClass { get; set; }

        public virtual DbSet<MaintStatus> MaintStatus { get; set; }

        public virtual DbSet<MaintWorkCentre> MaintWorkCentre { get; set; }
        public virtual DbSet<MaintenancePlant> MaintenancePlant { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<MeasPoint> MeasPoint { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Pbs> Pbs { get; set; }
        public virtual DbSet<PerformanceStandard> PerformanceStandard { get; set; }
        public virtual DbSet<PlantSection> PlantSection { get; set; }
        public virtual DbSet<Pmassembly> Pmassembly { get; set; }
        public virtual DbSet<Po> Po { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RbiSil> RbiSil { get; set; }
        public virtual DbSet<Rbm> Rbm { get; set; }
        public virtual DbSet<Rcm> Rcm { get; set; }
        public virtual DbSet<RegulatoryBody> RegulatoryBody { get; set; }
        public virtual DbSet<RelationshipToOperation> RelationshipToOperation { get; set; }
        public virtual DbSet<RelationshipType> RelationshipType { get; set; }

        public virtual DbSet<SAPStatus> SAPStatus { get; set; }

        public virtual DbSet<ScePsreview> ScePsreview { get; set; }
        public virtual DbSet<Sp> Sp { get; set; }
        public virtual DbSet<SubSystem> SubSystem { get; set; }
        public virtual DbSet<SuperClass> SuperClass { get; set; }
        public virtual DbSet<SysCond> SysCond { get; set; }
        public virtual DbSet<Systems> System { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagEngData> TagEngData { get; set; }
        public virtual DbSet<TagPo> TagPo { get; set; }
        public virtual DbSet<TagXdoc> TagXdoc { get; set; }
        public virtual DbSet<TaskListCat> TaskListCat { get; set; }
        public virtual DbSet<TaskListGroup> TaskListGroup { get; set; }
        public virtual DbSet<TaskListHeader> TaskListHeader { get; set; }
        public virtual DbSet<TaskListOperations> TaskListOperations { get; set; }
        public virtual DbSet<TaskListXscePsreview> TaskListXscePsreview { get; set; }
        public virtual DbSet<Vib> Vib { get; set; }

        public virtual DbSet<Priority> Priority { get; set; }

        public virtual DbSet<SchedulingPeriodUOM> SchedulingPeriodUOM { get; set; }

        // ETL Tables
        public virtual DbSet<Import> Import { get; set; }
        public virtual DbSet<ImportType> ImportType { get; set; }
        public virtual DbSet<ImportExtract> ImportExtract { get; set; }
        public virtual DbSet<ImportTransform> ImportTransform { get; set; }
        public virtual DbSet<ImportError> ImportError { get; set; }
        public virtual DbSet<ImportReport> ImportReport { get; set; }

        // SAP Requirements
        public virtual DbSet<EntityAttribute> EntityAttribute { get; set; }
        public virtual DbSet<EntityAttributeRequirement> EntityAttributeRequirement { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // view in the database
            modelBuilder.Entity<ImportReport>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("ImportReport");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasIndex(e => e.AreaName)
                    .HasName("U_Area")
                    .IsUnique();

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.AreaDisc)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MaintenancePlantId).HasColumnName("MaintenancePlantID");

                //entity.Property(e => e.Longititude);

                //entity.Property(e => e.Latitude);

                //entity.Property(e => e.Elevation);

                //entity.HasOne(d => d.MaintenancePlant)
                //    .WithMany(p => p.Areas)
                //   .HasForeignKey(d => d.EngPlantSectionId)
                //   .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ClassCharacteristics>(entity =>
            {
                entity.HasIndex(e => e.Class)
                    .HasName("U_Class")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CharDesc).HasMaxLength(255);

                entity.Property(e => e.Characteristic).HasMaxLength(255);

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ClassDesc).HasMaxLength(255);

                entity.Property(e => e.DropdownText).HasMaxLength(255);

                entity.Property(e => e.DropdownTextValue)
                    .HasColumnName("DropdownText Value")
                    .HasMaxLength(255);

                entity.Property(e => e.DropdownValDesc).HasMaxLength(255);
            });

            modelBuilder.Entity<ClassCharsTaskListHeader>(entity =>
            {
                entity.Property(e => e.ClassCharsTaskListHeaderId).HasColumnName("ClassCharsTaskListHeaderID");

                entity.Property(e => e.CharDesc).HasMaxLength(255);

                entity.Property(e => e.CharValue).HasMaxLength(255);

                entity.Property(e => e.Characteristic).HasMaxLength(255);

                entity.Property(e => e.Class).HasMaxLength(255);

                entity.Property(e => e.ClassDesc).HasMaxLength(255);

                entity.Property(e => e.CntrAssociation).HasMaxLength(255);

                entity.Property(e => e.GroupAssociation).HasMaxLength(255);
            });

            modelBuilder.Entity<MaintObjectTypeXMaintClass>(entity =>
            {
                entity.HasKey(e => new { e.MaintObjectTypeId, e.MaintClassId });
            });


            // ARGH
            // CHECK AGAIN
            modelBuilder.Entity<MaintClassXEngDataCode>(entity =>
            {
                entity.HasKey(e => new { e.MaintClassId, e.EngDataCodeId });

                entity.HasOne(d => d.MaintClass)
                    .WithMany(p => p.MaintClassXEngDataCode)
                    .HasForeignKey(d => d.MaintClassId);
                //.HasConstraintName("FK_MaintClassXEngDataCode_MaintClass");

                //entity.HasOne(d => d.EngDataCode)
                //    .WithMany(p => p.MaintClassXEngDataCode)
                //    .HasForeignKey(d => d.EngDataCodeId);
                //    //.HasConstraintName("FK_MaintClassXEngDataCode_EngDataCode");
            });

            modelBuilder.Entity<CommClass>(entity =>
            {
                entity.HasIndex(e => e.CommClassName)
                    .HasName("U_CommClass")
                    .IsUnique();

                entity.Property(e => e.CommClassId).HasColumnName("CommClassID");

                entity.Property(e => e.CommClassDesc).HasMaxLength(255);

                entity.Property(e => e.CommClassName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CommSubSystem>(entity =>
            {
                entity.HasIndex(e => e.CommSubSystemNo)
                    .HasName("U_CommSubSystem")
                    .IsUnique();

                entity.Property(e => e.CommSubSystemId).HasColumnName("CommSubSystemID");

                entity.Property(e => e.CommSubSystemName).HasMaxLength(255);

                entity.Property(e => e.CommSubSystemNo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Spid).HasColumnName("SPID");

                entity.Property(e => e.SystemId).HasColumnName("SystemID");

                entity.HasOne(d => d.Sp)
                    .WithMany(p => p.CommSubSystems)
                    .HasForeignKey(d => d.Spid)
                    .HasConstraintName("FK_CommSubSystem_SP");

                entity.HasOne(d => d.Systems)
                    .WithMany(p => p.CommSubSystems)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommSubSystem_System");
            });

            modelBuilder.Entity<CommZone>(entity =>
            {
                entity.HasIndex(e => e.CommZoneName)
                    .HasName("U_CommZone")
                    .IsUnique();

                entity.Property(e => e.CommZoneId).HasColumnName("CommZoneID");

                entity.Property(e => e.CommZoneDesc).HasMaxLength(255);

                entity.Property(e => e.CommZoneName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CommZones)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommZone_Project");
            });




            modelBuilder.Entity<ControlKey>(entity =>
            {
                entity.Property(e => e.ControlKeyId)
                    .HasColumnName("ControlKeyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ControlKeyDesc).HasMaxLength(255);

                entity.Property(e => e.ControlKeyName).HasMaxLength(255);
            });


            //modelBuilder.Entity<Division>(entity =>
            //{
            //    entity.Property(e => e.DivisionId).HasColumnName("DivisionId");

            //    entity.Property(e => e.DivisionName).HasMaxLength(255);

            //    entity.Property(e => e.CompanyId);

            //    entity.HasOne(d => d.Company)
            //        .WithMany(p => p.Divisions)
            //        .HasForeignKey(d => d.CompanyId)
            //        .HasConstraintName("FK_Division_Company");
            //});


            modelBuilder.Entity<Doc>(entity =>
            {
                entity.HasIndex(e => e.DocNum)
                    .HasName("U_DocNum")
                    .IsUnique();

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.DocAlias).HasMaxLength(25);

                entity.Property(e => e.DocComments).HasMaxLength(255);

                entity.Property(e => e.DocLink).HasMaxLength(255);

                entity.Property(e => e.DocNum)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.DocSource).HasMaxLength(255);

                entity.Property(e => e.DocTitle).HasMaxLength(255);

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.HasOne(d => d.DocType)
                    .WithMany(p => p.Docs)
                    .HasForeignKey(d => d.DocTypeId)
                    .HasConstraintName("FK_Doc_DocType");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.HasIndex(e => e.DocTypeName)
                    .HasName("U_DocType")
                    .IsUnique();

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeID");

                entity.Property(e => e.DocTypeDesc).HasMaxLength(255);

                entity.Property(e => e.DocTypeName)
                    .IsRequired()
                    .HasMaxLength(255);

            });


            modelBuilder.Entity<EngClass>(entity =>
            {
                entity.HasIndex(e => e.EngClassName)
                    .HasName("U_EngClass")
                    .IsUnique();

                entity.Property(e => e.EngClassId).HasColumnName("EngClassID");

                entity.Property(e => e.EngClassDesc).HasMaxLength(255);

                entity.Property(e => e.EngClassName)
                    .IsRequired()
                    .HasMaxLength(255);


            });

            modelBuilder.Entity<EngClassRequiredDocs>(entity =>
            {

                entity.Property(e => e.DocTypeId).HasColumnName("DocTypeId");

                //entity.Ignore(e => e.DocTypes);

                //entity.HasMany(e => e.DocTypes)
                //   .WithOne(p => p.DocTypeId)
                //   .HasForeignKey(e => e.DocTypeId)
                //   .HasConstraintName("FK_EngReqDocs_DocType");

            });


            //entity.HasMany(d => d.DocType)
            //       .WithOne(p => p.DocTypeId)
            //       .HasForeignKey(d => d.DocTypeId)
            //       .HasConstraintName("FK_EngReqDocs_DocType");


            //modelBuilder.Entity<EngDataClass>(entity =>
            //{
            //    entity.HasIndex(e => e.EngDataClassName)
            //        .HasName("u_EngDataClass")
            //        .IsUnique();

            //    entity.Property(e => e.EngDataClassId).HasColumnName("EngClassID");

            //    entity.Property(e => e.EngDataClassName)
            //        .IsRequired()
            //        .HasMaxLength(255);
            //});

            modelBuilder.Entity<EngDataCode>(entity =>
            {
                entity.HasIndex(e => e.EngDataCodeName)
                    .HasName("U_EngDataCodeName")
                    .IsUnique();

                entity.Property(e => e.EngDataCodeId).HasColumnName("EngDataCodeID");

                entity.Property(e => e.EngDataCodeDesc).HasMaxLength(255);

                entity.Property(e => e.EngDataCodeName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EngDataCodeNotes).HasMaxLength(50);
            });

            modelBuilder.Entity<EngDisc>(entity =>
            {
                entity.HasIndex(e => e.EngDiscName)
                    .HasName("U_EngDisc")
                    .IsUnique();

                entity.Property(e => e.EngDiscId).HasColumnName("EngDiscID");

                entity.Property(e => e.EngDiscDesc).HasMaxLength(255);

                entity.Property(e => e.EngDiscName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            //modelBuilder.Entity<EngPlant>(entity =>
            //{
            //    entity.Property(e => e.EngPlantId);
            //    entity.Property(e => e.EngPlantName);
            //    entity.Property(e => e.DivisionId);
            //    entity.Property(e => e.Longitude);
            //    entity.Property(e => e.Latitude);

            //    entity.HasOne(d => d.Division)
            //    .WithMany(p => p.EngPlants)
            //    .HasForeignKey(d =>  d.DivisionId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_EngPlant_Division");

            //});

            modelBuilder.Entity<EngStatus>(entity =>
            {
                entity.HasIndex(e => e.EngStatusName)

                    .IsUnique();

                entity.Property(e => e.EngStatusId).HasColumnName("EngStatusID");

                entity.Property(e => e.EngStatusName)

                    .HasMaxLength(255);

            });


            modelBuilder.Entity<EnvZone>(entity =>
            {
                entity.HasIndex(e => e.EnvZoneName)
                    .HasName("U_EnvZone")
                    .IsUnique();

                entity.Property(e => e.EnvZoneId).HasColumnName("EnvZoneID");

                entity.Property(e => e.EnvZoneDesc).HasMaxLength(50);

                entity.Property(e => e.EnvZoneName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExMethod>(entity =>
            {
                entity.HasIndex(e => e.ExMethodName)
                    .HasName("U_ExMethod")
                    .IsUnique();

                entity.Property(e => e.ExMethodId).HasColumnName("ExMethodID");

                entity.Property(e => e.ExMethodDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ExMethodName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<FlocXmaintItem>(entity =>
            {
                entity.HasKey(e => new { e.FlocId, e.MaintItemId });

                entity.ToTable("FlocXMaintItem");

                entity.Property(e => e.FlocId).HasColumnName("FlocID");

                entity.Property(e => e.MaintItemId).HasColumnName("MaintItemID");

                entity.HasOne(d => d.Floc)
                    .WithMany(p => p.FlocXmaintItems)
                    .HasForeignKey(d => d.FlocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_FlocXMaintItem_Tag");

                entity.HasOne(d => d.MaintItem)
                    .WithMany(p => p.FlocXmaintItems)
                    .HasForeignKey(d => d.MaintItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_FlocXMaintItem_MaintItem");
            });

            modelBuilder.Entity<FlocXmaintLoad>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.MaintLoadId });

                entity.ToTable("FlocXMaintLoad");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.MaintLoadId).HasColumnName("MaintLoadID");

                entity.HasOne(d => d.MaintLoad)
                    .WithMany(p => p.FlocXmaintLoads)
                    .HasForeignKey(d => d.MaintLoadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXMaintLoad_MaintLoad");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.FlocXmaintLoads)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXMaintLoad_Floc");
            });

            modelBuilder.Entity<FlocXmaintQuery>(entity =>
            {
                entity.HasKey(e => new { e.FlocId, e.MaintQueryId });

                entity.ToTable("FlocXMaintQuery");

                entity.Property(e => e.FlocId).HasColumnName("FlocID");

                entity.Property(e => e.MaintQueryId).HasColumnName("MaintQueryID");

                entity.HasOne(d => d.Floc)
                    .WithMany(p => p.FlocXmaintQuerys)
                    .HasForeignKey(d => d.FlocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXMaintQuery_Floc");

                entity.HasOne(d => d.MaintQuery)
                    .WithMany(p => p.FlocXmaintQuerys)
                    .HasForeignKey(d => d.MaintQueryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXMaintQuery_MaintQuery");
            });

            modelBuilder.Entity<FlocXpmassembly>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.PmassemblyId });

                entity.ToTable("FlocXPMAssembly");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.PmassemblyId).HasColumnName("PMAssemblyID");

                entity.HasOne(d => d.Pmassembly)
                    .WithMany(p => p.FlocXpmassemblys)
                    .HasForeignKey(d => d.PmassemblyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXPMAssembly_PMAssembly");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.FlocXpmassemblys)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlocXPMAssembly_Tag");
            });

            modelBuilder.Entity<Ipf>(entity =>
            {
                entity.HasIndex(e => e.IpfName)
                    .HasName("U_Ipf")
                    .IsUnique();

                entity.Property(e => e.IpfId).HasColumnName("IpfID");

                entity.Property(e => e.IpfDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IpfName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<LoadTemplate>(entity =>
            {
                entity.HasIndex(e => e.LoadTemplateName)
                    .HasName("U_LoadTemplate")
                    .IsUnique();

                entity.Property(e => e.LoadTemplateId).HasColumnName("LoadTemplateID");

                entity.Property(e => e.LoadTemplateDesc).HasMaxLength(255);

                entity.Property(e => e.LoadTemplateName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => new { e.AreaId, e.LocationName })
                    .HasName("U_Location")
                    .IsUnique();

                //entity.Property(e => e.LocationId).HasColumnName("LocationID");

                //entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.LocationDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude);

                entity.Property(e => e.Latitude);

                entity.Property(e => e.Elevation);

                entity.Property(e => e.LocationType);

                entity.Property(e => e.ParentLocationID);



                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Area");


            });

            modelBuilder.Entity<MaintArea>(entity =>
            {
                entity.HasIndex(e => e.MaintAreaName)
                    .HasName("U_MaintArea")
                    .IsUnique();

                entity.Property(e => e.MaintAreaId).HasColumnName("MaintAreaId");

                entity.Property(e => e.MaintAreaDesc).HasMaxLength(255);

                entity.Property(e => e.MaintAreaName)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.PlantSectionId).HasColumnName("PlantSectionID");

                entity.HasOne(d => d.PlantSection)
                    .WithMany(p => p.MaintAreas)
                    .HasForeignKey(d => d.PlantSectionId)
                    .HasConstraintName("FK_MaintArea_PlantSection");
            });

            modelBuilder.Entity<MaintClass>(entity =>
            {
                entity.HasKey(e => new { e.MaintClassId });

                entity.Property(e => e.MaintClassDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintClassName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintCriticality>(entity =>
            {
                entity.HasIndex(e => e.MaintCriticalityName)
                    .HasName("U_MaintCriticality")
                    .IsUnique();

                entity.Property(e => e.MaintCriticalityId).HasColumnName("MaintCriticalityID");

                entity.Property(e => e.MaintCriticalityDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintCriticalityName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintEdcCode>(entity =>
            {
                entity.HasIndex(e => e.MaintEdcCodeName)
                    .HasName("U_MaintEdcCode")
                    .IsUnique();

                entity.Property(e => e.MaintEdcCodeId).HasColumnName("MaintEdcCodeID");

                entity.Property(e => e.MaintEdcCodeDesc).HasMaxLength(255);

                entity.Property(e => e.MaintEdcCodeName)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<MaintItem>(entity =>
            {
                entity.HasIndex(e => e.MaintItemNum)
                    .HasName("U_MaintItemNum")
                    .IsUnique();

                entity.Property(e => e.MaintItemId).HasColumnName("MaintItemID");

                entity.Property(e => e.FMaintItemHeaderFloc)
                    .HasColumnName("fMaintItemHeaderFloc")
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemActivityTypeId)
                    .HasColumnName("MaintItemActivityTypeID")
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemConsequence).HasMaxLength(255);

                entity.Property(e => e.MaintItemConsequenceCategory).HasMaxLength(255);

                entity.Property(e => e.MaintItemDoNotRelImmed).HasMaxLength(255);

                entity.Property(e => e.MaintItemHeaderEquipId)
                    .HasColumnName("MaintItemHeaderEquipID")
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemLikelihood).HasMaxLength(255);

                entity.Property(e => e.MaintItemLongText).HasMaxLength(255);

                entity.Property(e => e.MaintItemMainWorkCentre).HasMaxLength(255);

                entity.Property(e => e.MaintItemMainWorkCentrePlant).HasMaxLength(255);

                entity.Property(e => e.MaintPlanId)
                    .IsRequired()
                    .HasColumnName("MaintPlanID")
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemObjectListEquip).HasMaxLength(255);

                entity.Property(e => e.MaintItemObjectListFloc).HasMaxLength(255);

                entity.Property(e => e.MaintItemOrderType).HasMaxLength(255);

                entity.Property(e => e.MaintPlannerGroupId)
                    .HasColumnName("MaintPlannerGroupID")
                    .HasMaxLength(255);

                entity.Property(e => e.MaintItemProposedPriority).HasMaxLength(255);

                entity.Property(e => e.MaintItemProposedTi).HasMaxLength(255);

                entity.Property(e => e.MaintItemRevNo).HasMaxLength(255);

                entity.Property(e => e.MaintItemShortText).HasMaxLength(255);

                entity.Property(e => e.SysCondId);

                entity.Property(e => e.MaintItemTasklistExecutionFactor).HasMaxLength(255);

                entity.Property(e => e.MaintItemUserStatus).HasMaxLength(255);
            });

            modelBuilder.Entity<MaintItemXmaintTaskListHeader>(entity =>
            {
                entity.HasKey(e => new { e.MaintItemId, e.TaskListHeaderId });

                entity.ToTable("MaintItemXMaintTaskList");

                entity.Property(e => e.MaintItemId).HasColumnName("MaintItemID");

                entity.Property(e => e.TaskListHeaderId).HasColumnName("TaskListHeaderId");
            });

            modelBuilder.Entity<MaintLoad>(entity =>
            {
                entity.HasIndex(e => e.MaintLoadNum)
                    .HasName("U_MaintLoadNum")
                    .IsUnique();

                entity.Property(e => e.MaintLoadId).HasColumnName("MaintLoadID");

                entity.Property(e => e.LoadTemplateId).HasColumnName("LoadTemplateID");

                entity.Property(e => e.MaintLoadComment).HasMaxLength(255);

                entity.Property(e => e.MaintLoadNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.LoadTemplate)
                    .WithMany(p => p.MaintLoads)
                    .HasForeignKey(d => d.LoadTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaintLoad_LoadTemplate");
            });

            modelBuilder.Entity<MaintLocation>(entity =>
            {
                entity.HasIndex(e => e.MaintLocationName)
                    .HasName("U_MaintLocation")
                    .IsUnique();

                entity.Property(e => e.MaintLocationId).HasColumnName("MaintLocationID");

                entity.Property(e => e.MaintAreaId).HasColumnName("MaintAreaId");

                entity.Property(e => e.MaintLocationDesc).HasMaxLength(255);

                entity.Property(e => e.MaintLocationName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.MaintArea)
                    .WithMany(p => p.MaintLocations)
                    .HasForeignKey(d => d.MaintAreaId)
                    .HasConstraintName("FK_MaintLocation_MaintArea");
            });

            modelBuilder.Entity<MaintObjectType>(entity =>
            {
                entity.HasIndex(e => e.MaintObjectTypeName)
                    .HasName("U_maintobjecttype")
                    .IsUnique();

                entity.Property(e => e.MaintObjectTypeId).HasColumnName("MaintObjectTypeID");

                entity.Property(e => e.MaintObjectTypeDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintObjectTypeDescExt).HasMaxLength(255);

                entity.Property(e => e.MaintObjectTypeName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StdNounModifier).HasMaxLength(255);
            });

            modelBuilder.Entity<MaintPackage>(entity =>
            {
                entity.HasIndex(e => e.MaintPackageName)
                    .HasName("U_MaintPackageName")
                    .IsUnique();

                entity.Property(e => e.MaintPackageId).HasColumnName("MaintPackageID");

                entity.Property(e => e.MaintPackageCycleText).HasMaxLength(255);

                entity.Property(e => e.MaintPackageCycleUnit).HasMaxLength(255);

                entity.Property(e => e.MaintPackageName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintPlan>(entity =>
            {
                entity.HasIndex(e => e.MaintPlanName)
                    .HasName("U_MaintPlan")
                    .IsUnique();

                entity.HasKey(e => e.MaintPlanId);

                entity.Property(e => e.MaintPlanId).HasColumnName("MaintPlanID");

                entity.Property(e => e.CallHorizon).HasMaxLength(255);

                entity.Property(e => e.ChangeStatus).HasMaxLength(255);

                entity.Property(e => e.CycleModFactor).HasMaxLength(255);

                entity.Property(e => e.MaintPlanName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintSortProcessId).HasColumnName("MaintSortProcessID");

                entity.Property(e => e.MaintStrategyId).HasColumnName("MaintStrategyID");

                entity.Property(e => e.MeasPointId).HasColumnName("MeasPointID");

                //entity.Property(e => e.SchedulingPeriodUomId).HasMaxLength(255);

                entity.Property(e => e.SchedulingPeriodValue).HasMaxLength(255);

                entity.Property(e => e.ShortText).HasMaxLength(255);

                entity.Property(e => e.Sort).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasMaxLength(255);

                entity.Property(e => e.StartingInstructions).HasMaxLength(255);

                entity.HasOne(d => d.MaintSortProcess)
                    .WithMany(p => p.MaintPlans)
                    .HasForeignKey(d => d.MaintSortProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaintPlan_MaintSortProcess");

                entity.HasOne(d => d.MaintStrategy)
                    .WithMany(p => p.MaintPlans)
                    .HasForeignKey(d => d.MaintStrategyId)
                    .HasConstraintName("FK_MaintPlan_MaintStrategy");

                entity.HasOne(d => d.MeasPoint)
                    .WithMany(p => p.MaintPlans)
                    .HasForeignKey(d => d.MeasPointId)
                    .HasConstraintName("FK_MaintPlan_MeasPoint");
            });

            modelBuilder.Entity<MaintPlannerGroup>(entity =>
            {
                entity.HasIndex(e => e.MaintPlannerGroupName)
                    .HasName("U_MaintPlannerGroup")
                    .IsUnique();

                entity.Property(e => e.MaintPlannerGroupId).HasColumnName("MaintPlannerGroupID");

                entity.Property(e => e.MaintPlannerGroupDesc).HasMaxLength(255);

                entity.Property(e => e.MaintPlannerGroupName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintQuery>(entity =>
            {
                entity.HasIndex(e => e.MaintQueryNum)
                    .HasName("U_MaintQueryNum")
                    .IsUnique();

                entity.Property(e => e.MaintQueryId).HasColumnName("MaintQueryID");

                entity.Property(e => e.MaintQueryClosedBy).HasMaxLength(255);

                entity.Property(e => e.MaintQueryClosedDate).HasColumnType("datetime");

                entity.Property(e => e.MaintQueryNoteId).HasColumnName("MaintQueryNoteID");

                entity.Property(e => e.MaintQueryNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintQueryRaisedBy).HasMaxLength(255);

                entity.Property(e => e.MaintQueryRaisedDate).HasColumnType("datetime");

                entity.Property(e => e.MaintQueryTitle).HasMaxLength(255);

                entity.HasOne(d => d.MaintQueryNote)
                    .WithMany(p => p.MaintQuerys)
                    .HasForeignKey(d => d.MaintQueryNoteId)
                    .HasConstraintName("FK_MaintQuery_MaintQueryNote");
            });

            modelBuilder.Entity<MaintQueryNote>(entity =>
            {
                entity.Property(e => e.MaintQueryNoteId).HasColumnName("MaintQueryNoteID");

                entity.Property(e => e.MaintQueryId).HasColumnName("MaintQueryID");

                entity.Property(e => e.MaintQueryNoteAttachments).HasMaxLength(255);

                entity.Property(e => e.MaintQueryNoteBy).HasMaxLength(255);

                entity.Property(e => e.MaintQueryNoteDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MaintScePsReviewTeam>(entity =>
            {
                entity.HasIndex(e => e.MaintScePsReviewTeamName)
                    .HasName("U_MaintScePsReviewTeam")
                    .IsUnique();

                entity.Property(e => e.MaintScePsReviewTeamId).HasColumnName("MaintScePsReviewTeamID");

                entity.Property(e => e.MaintScePsReviewTeamDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintScePsReviewTeamName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintSortProcess>(entity =>
            {
                entity.HasIndex(e => e.MaintSortProcessName)
                    .HasName("U_MaintSortProcess")
                    .IsUnique();

                entity.Property(e => e.MaintSortProcessId).HasColumnName("MaintSortProcessID");

                entity.Property(e => e.MaintSortProcessDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintSortProcessName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintStrategy>(entity =>
            {
                entity.HasIndex(e => e.MaintStrategyName)
                    .HasName("U_MaintStrategy")
                    .IsUnique();

                entity.Property(e => e.MaintStrategyId).HasColumnName("MaintStrategyID");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.MaintStrategyDesc).HasMaxLength(255);

                entity.Property(e => e.MaintStrategyName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.MaintStrategys)
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("FK_MaintStrategy_Doc");
            });

            modelBuilder.Entity<MaintStructureIndicator>(entity =>
            {
                entity.HasIndex(e => e.MaintStructureIndicatorName)
                    .HasName("U_MaintStructureIndicator")
                    .IsUnique();

                entity.Property(e => e.MaintStructureIndicatorId).HasColumnName("MaintStructureIndicatorID");

                entity.Property(e => e.MaintStructureIndicatorDesc).HasMaxLength(255);

                entity.Property(e => e.MaintStructureIndicatorName)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<MaintType>(entity =>
            {
                entity.HasIndex(e => e.MaintTypeName)
                    .HasName("U_MaintType")
                    .IsUnique();

                entity.Property(e => e.MaintTypeId).HasColumnName("MaintTypeID");

                entity.Property(e => e.MaintTypeDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MaintTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MaintWorkCentre>(entity =>
            {
                entity.HasIndex(e => e.MaintWorkCentreName)
                    .HasName("U_MaintWorkCentre")
                    .IsUnique();

                entity.Property(e => e.MaintWorkCentreId).HasColumnName("MaintWorkCentreID");

                entity.Property(e => e.MaintWorkCentreDesc).HasMaxLength(255);

                entity.Property(e => e.MaintWorkCentreName)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<MaintenancePlant>(entity =>
            {
                entity.Property(e => e.MaintenancePlantId).HasColumnName("MaintenancePlantID");

                entity.Property(e => e.MaintenancePlantDesc).HasMaxLength(255);

                entity.Property(e => e.MaintenancePlantNum)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasIndex(e => e.ManufacturerName)
                    .HasName("U_Manufacturer")
                    .IsUnique();

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ManufacturerDesc).HasMaxLength(255);

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MeasPoint>(entity =>
            {
                entity.Property(e => e.MeasPointId).HasColumnName("MeasPointID");

                entity.Property(e => e.MeasPointData)
                    .IsRequired()
                    .HasMaxLength(2056);

                entity.Property(e => e.MeasPointName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasIndex(e => e.ModelName)
                    .HasName("U_Model")
                    .IsUnique();

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.ModelDesc).HasMaxLength(255);

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.OperationId)
                    .HasColumnName("OperationID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OperationName)
                    .HasColumnName("OperationNAme")
                    .HasMaxLength(255);

                entity.Property(e => e.OperationNotes).HasMaxLength(255);
            });

            modelBuilder.Entity<Pbs>(entity =>
            {
                entity.HasIndex(e => e.PbsName)
                    .HasName("U_PBS")
                    .IsUnique();

                entity.Property(e => e.PbsId).HasColumnName("PbsID");

                entity.Property(e => e.PbsDesc).HasMaxLength(150);

                entity.Property(e => e.PbsName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PerformanceStandard>(entity =>
            {
                entity.HasIndex(e => e.PerformanceStandardName)
                    .HasName("U_PerformanceStandard")
                    .IsUnique();

                entity.Property(e => e.PerformanceStandardId).HasColumnName("PerformanceStandardID");

                entity.Property(e => e.PerformanceStandardDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PerformanceStandardName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PlantSection>(entity =>
            {
                entity.HasIndex(e => e.PlantSectionName)
                    .HasName("U_PlantSection")
                    .IsUnique();

                entity.Property(e => e.PlantSectionId).HasColumnName("PlantSectionID");

                entity.Property(e => e.PlantSectionDesc).HasMaxLength(255);

                entity.Property(e => e.PlantSectionName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Pmassembly>(entity =>
            {
                entity.ToTable("PMAssembly");

                entity.HasIndex(e => e.PmassemblyName)
                    .HasName("U_PMAssembly")
                    .IsUnique();

                entity.Property(e => e.PmassemblyId)
                    .HasColumnName("PMAssemblyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Make).HasMaxLength(255);

                entity.Property(e => e.Model).HasMaxLength(255);

                entity.Property(e => e.PmassemblyName)
                    .IsRequired()
                    .HasColumnName("PMAssemblyName")
                    .HasMaxLength(255);

                entity.Property(e => e.Rev).HasMaxLength(255);

                entity.Property(e => e.ShortText).HasMaxLength(255);
            });

            modelBuilder.Entity<Po>(entity =>
            {
                entity.ToTable("PO");

                entity.HasIndex(e => e.PoName)
                    .HasName("U_PO")
                    .IsUnique();

                entity.Property(e => e.PoId).HasColumnName("POID");

                entity.Property(e => e.PoCompany)
                    .HasColumnName("POCompany")
                    .HasMaxLength(255);

                entity.Property(e => e.PoDesc)
                    .HasColumnName("PODesc")
                    .HasMaxLength(255);

                entity.Property(e => e.PoName)
                    .IsRequired()
                    .HasColumnName("POName")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.ProjectCode)
                    .HasName("U_ProjectCode")
                    .IsUnique();

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.MaintenancePlantId).HasColumnName("MaintenancePlantID");

                entity.Property(e => e.ProjectCode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ProjectName).HasMaxLength(255);

                entity.HasOne(d => d.MaintenancePlant)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.MaintenancePlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_MaintenancePlant");
            });

            modelBuilder.Entity<RbiSil>(entity =>
            {
                entity.HasIndex(e => e.RbiSilName)
                    .HasName("U_RbiSil")
                    .IsUnique();

                entity.Property(e => e.RbiSilId).HasColumnName("RbiSilID");

                entity.Property(e => e.RbiSilDate).HasColumnType("datetime");

                entity.Property(e => e.RbiSilDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RbiSilName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Rbm>(entity =>
            {
                entity.HasIndex(e => e.RbmName)
                    .HasName("U_Rbm")
                    .IsUnique();

                entity.Property(e => e.RbmId).HasColumnName("RbmID");

                entity.Property(e => e.RbmDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RbmName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Rcm>(entity =>
            {
                entity.HasIndex(e => e.RcmName)
                    .HasName("U_Rcm")
                    .IsUnique();

                entity.Property(e => e.RcmId).HasColumnName("RcmID");

                entity.Property(e => e.RcmDate).HasColumnType("datetime");

                entity.Property(e => e.RcmDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RcmName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RegulatoryBody>(entity =>
            {
                entity.HasIndex(e => e.RegulatoryBodyName)
                    .HasName("U_RegulatoryBody")
                    .IsUnique();

                entity.Property(e => e.RegulatoryBodyId)
                    .HasColumnName("RegulatoryBodyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegulatoryBodyDesc).HasMaxLength(255);

                entity.Property(e => e.RegulatoryBodyName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RelationshipToOperation>(entity =>
            {
                entity.Property(e => e.RelationshipToOperationId)
                    .HasColumnName("RelationshipToOperationID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RelationshipToOperationName).HasMaxLength(255);

                entity.Property(e => e.RelationshipToOperationNotes).HasMaxLength(255);

                entity.Property(e => e.RelationshipTypeId).HasColumnName("RelationshipTypeID");

                entity.HasOne(d => d.RelationshipType)
                    .WithMany(p => p.RelationshipToOperations)
                    .HasForeignKey(d => d.RelationshipTypeId)
                    .HasConstraintName("FK_RelationshipToOperation_RelationshipType");
            });

            modelBuilder.Entity<RelationshipType>(entity =>
            {
                entity.Property(e => e.RelationshipTypeId)
                    .HasColumnName("RelationshipTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RelationshipTypeDesc).HasMaxLength(255);

                entity.Property(e => e.RelationshipTypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<ScePsreview>(entity =>
            {
                entity.ToTable("ScePSReview");

                entity.HasIndex(e => e.ScePsreviewName)
                    .HasName("U_ScePsReviewID")
                    .IsUnique();

                entity.Property(e => e.ScePsreviewId)
                    .HasColumnName("ScePSReviewID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ScePsreviewDesc)
                    .HasColumnName("ScePSReviewDesc")
                    .HasMaxLength(255);

                entity.Property(e => e.ScePsreviewName)
                    .IsRequired()
                    .HasColumnName("ScePSReviewName")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Sp>(entity =>
            {
                entity.ToTable("SP");

                entity.HasIndex(e => e.Spnum)
                    .HasName("U_SPnum")
                    .IsUnique();

                entity.Property(e => e.Spid).HasColumnName("SPID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Spdesc)
                    .HasColumnName("SPdesc")
                    .HasMaxLength(50);

                entity.Property(e => e.Spnum)
                    .IsRequired()
                    .HasColumnName("SPnum")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Sps)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SP_Project");
            });

            modelBuilder.Entity<SubSystem>(entity =>
            {
                entity.HasIndex(e => e.SubSystemNum)
                    .HasName("U_SubSystem")
                    .IsUnique();

                entity.Property(e => e.SubSystemId).HasColumnName("SubSystemID");

                entity.Property(e => e.SubSystemName).HasMaxLength(255);

                entity.Property(e => e.SubSystemNum)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SystemsId).HasColumnName("SystemID");

                entity.HasOne(d => d.Systems)
                    .WithMany(p => p.SubSystems)
                    .HasForeignKey(d => d.SystemsId)
                    .HasConstraintName("FK_SubSystem_System");
            });

            modelBuilder.Entity<SuperClass>(entity =>
            {
                entity.HasIndex(e => e.SuperclassID)
                                .HasName("U_SuperClass")
                                .IsUnique();
                entity.Property(e => e.SuperclassID).HasColumnName("SuperclassID");

                entity.Property(e => e.SuperclassName)
                                .IsRequired()
                                .HasColumnName("SuperclassName")
                                .HasMaxLength(50);


                entity.Property(e => e.Superclassdescription)
                                .IsRequired()
                                .HasMaxLength(255);

            });


            modelBuilder.Entity<SysCond>(entity =>
            {
                entity.HasIndex(e => e.SysCondName)
                    .HasName("U_SysCond")
                    .IsUnique();

                entity.Property(e => e.SysCondId)
                    .HasColumnName("SysCondID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SysCondName)
                    .IsRequired()
                    .HasColumnName("SysCondName")
                    .HasMaxLength(255);

                entity.Property(e => e.SysCondDesc).HasMaxLength(255);
            });

            modelBuilder.Entity<Systems>(entity =>
            {
                entity.HasIndex(e => e.SystemNum)
                    .HasName("U_System")
                    .IsUnique();

                entity.Property(e => e.SystemsId).HasColumnName("SystemID");

                entity.Property(e => e.SystemName).HasMaxLength(255);

                entity.Property(e => e.SystemNum)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(e => e.TagFloc)
                    .HasName("U_NoNull_TagFLOC")
                    .IsUnique()
                    .HasFilter("([TagFLOC] IS NOT NULL)");

                entity.HasIndex(e => e.TagNumber)
                    .HasName("IX_Tag")
                    .IsUnique();

                entity.Property(e => e.TagDeleted).HasDefaultValue(false);

                entity.Property(e => e.TagMaintQuery).HasDefaultValue(false);

                entity.Property(e => e.Tagnoneng).HasDefaultValue(false);

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.CommZoneId).HasColumnName("CommZoneID");

                entity.Property(e => e.CommClassId).HasColumnName("CommClassID");

                entity.Property(e => e.CommissioningSubsystemId).HasColumnName("CommissioningSubsystemID");

                entity.Property(e => e.MaintEdcCodeId).HasColumnName("MaintEdcCodeId");

                entity.Property(e => e.EngClassId).HasColumnName("EngClassID");

                entity.Property(e => e.EngDiscId).HasColumnName("EngDiscId");

                entity.Property(e => e.EngParentId).HasColumnName("EngParentID");

                entity.Property(e => e.EngStatusId).HasColumnName("EngStatusID");

                entity.Property(e => e.ExMethodId).HasColumnName("ExMethodID");

                entity.Property(e => e.IpfId).HasColumnName("IpfID");

                entity.Property(e => e.KeyDocId).HasColumnName("KeyDocID");

                entity.Property(e => e.LocationID).HasColumnName("LocationID");

                //entity.Property(e => e.MaintClassId).HasColumnName("MaintClassID");

                entity.Property(e => e.MaintCriticalityId).HasColumnName("MaintCriticalityID");

                entity.Property(e => e.MaintLocationId).HasColumnName("MaintLocationID");

                entity.Property(e => e.MaintObjectTypeId).HasColumnName("MaintObjectTypeID");

                entity.Property(e => e.MaintParentId).HasColumnName("MaintParentID");

                entity.Property(e => e.MaintPlannerGroupId).HasColumnName("MaintPlannerGroupID");

                entity.Property(e => e.MaintScePsJustification).HasMaxLength(50);

                entity.Property(e => e.MaintScePsReviewTeamId).HasColumnName("MaintScePsReviewTeamID");

                entity.Property(e => e.MaintSortProcessId).HasColumnName("MaintSortProcessID");

                entity.Property(e => e.MaintStatusId).HasColumnName("MaintStatusID");

                entity.Property(e => e.MaintStructureIndicatorId).HasColumnName("MaintStructureIndicatorID");

                entity.Property(e => e.MaintTypeId).HasColumnName("MaintTypeID");

                entity.Property(e => e.MaintWorkCentreId).HasColumnName("MaintWorkCentreID");

                entity.Property(e => e.MaintenanceplanId).HasColumnName("MaintenanceplanID");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.PerformanceStandardId).HasColumnName("PerformanceStandardID");

                entity.Property(e => e.PoId).HasColumnName("PoID");

                entity.Property(e => e.RbiSilId).HasColumnName("RbiSilID");

                entity.Property(e => e.RbmId).HasColumnName("RbmID");

                entity.Property(e => e.RcmId).HasColumnName("RcmID");

                entity.Property(e => e.SubSystemId).HasColumnName("SubSystemID");

                entity.Property(e => e.TagBomReq).HasMaxLength(4);

                entity.Property(e => e.TagCharDesc).HasMaxLength(255);

                entity.Property(e => e.TagCharValue).HasMaxLength(255);

                entity.Property(e => e.TagCharacteristic).HasMaxLength(255);

                entity.Property(e => e.TagFloc)
                    .HasColumnName("TagFLOC")
                    .HasMaxLength(50);

                entity.Property(e => e.TagFlocDesc).HasMaxLength(100);

                entity.Property(e => e.TagMaintCritComments).HasMaxLength(50);

                entity.Property(e => e.TagNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TagRawDesc).HasMaxLength(255);

                entity.Property(e => e.TagRawNumber).HasMaxLength(128);

                entity.Property(e => e.TagRbmMethod).HasMaxLength(50);

                entity.Property(e => e.TagService).HasMaxLength(255);

                entity.Property(e => e.TagSource).HasMaxLength(255);

                entity.Property(e => e.TagSpNo).HasMaxLength(10);

                entity.Property(e => e.TagSrcKeyList).HasMaxLength(40);

                entity.Property(e => e.TagVendorTag).HasMaxLength(50);

                entity.Property(e => e.TagVib).HasMaxLength(5);

                entity.Property(e => e.VibId).HasColumnName("VibID");

                entity.HasOne(d => d.CommZone)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.CommZoneId)
                    .HasConstraintName("FK_Tag_CommZone");

                entity.HasOne(d => d.CommClass)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.CommClassId)
                    .HasConstraintName("FK_Tag_CommClass");

                entity.HasOne(d => d.CommissioningSubsystem)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.CommissioningSubsystemId)
                    .HasConstraintName("FK_Tag_CommSubSystem");

                entity.HasOne(d => d.MaintEdcCode)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintEdcCodeId)
                    .HasConstraintName("FK_Tag_MaintEdcCode");

                entity.HasOne(d => d.EngClass)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.EngClassId)
                    .HasConstraintName("FK_Tag_EngClass");

                entity.HasOne(d => d.EngDisc)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.EngDiscId)
                    .HasConstraintName("FK_Tag_EngDisc");

                entity.HasOne(d => d.EngParent)
                    .WithMany(p => p.InverseEngParents)
                    .HasForeignKey(d => d.EngParentId)
                    .HasConstraintName("FK_Tag_EngParent");

                entity.HasOne(d => d.EngStatus)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.EngStatusId)
                    .HasConstraintName("FK_Tag_EngStatus");

                entity.HasOne(d => d.ExMethod)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ExMethodId)
                    .HasConstraintName("FK_Tag_ExMethod");

                entity.HasOne(d => d.Ipf)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.IpfId)
                    .HasConstraintName("FK_Tag_Ipf");

                entity.HasOne(d => d.KeyDoc)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.KeyDocId)
                    .HasConstraintName("FK_Tag_Doc");

                //entity.HasOne(d => d.Location)
                //    .WithMany(p => p.Tags)
                //    .HasForeignKey(d => d.LocationId)
                //    .HasConstraintName("FK_Tag_Location");

                //entity.HasOne(d => d.MaintClass)
                //    .WithMany(p => p.Tags)
                //    .HasForeignKey(d => d.MaintClassId)
                //    .HasConstraintName("FK_Tag_MaintClass");

                entity.HasOne(d => d.MaintCriticality)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintCriticalityId)
                    .HasConstraintName("FK_Tag_MaintCriticality");

                entity.HasOne(d => d.MaintLocation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintLocationId)
                    .HasConstraintName("FK_Tag_MaintLocation");

                entity.HasOne(d => d.MaintObjectType)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintObjectTypeId)
                    .HasConstraintName("FK_Tag_MaintObjectType");

                entity.HasOne(d => d.MaintParent)
                    .WithMany(p => p.InverseMaintParents)
                    .HasForeignKey(d => d.MaintParentId)
                    .HasConstraintName("FK_Tag_MaintParent");

                entity.HasOne(d => d.MaintPlannerGroup)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintPlannerGroupId)
                    .HasConstraintName("FK_Tag_MaintPlannerGroup");

                entity.HasOne(d => d.MaintScePsReviewTeam)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintScePsReviewTeamId)
                    .HasConstraintName("FK_Tag_MaintScePsReviewTeam");

                entity.HasOne(d => d.MaintSortProcess)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintSortProcessId)
                    .HasConstraintName("FK_Tag_MaintSortProcess");

                entity.HasOne(d => d.MaintStructureIndicator)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintStructureIndicatorId)
                    .HasConstraintName("FK_Tag_MaintStructIndicator");

                entity.HasOne(d => d.MaintType)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintTypeId)
                    .HasConstraintName("FK_Tag_MaintType");

                entity.HasOne(d => d.MaintWorkCentre)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintWorkCentreId)
                    .HasConstraintName("FK_Tag_MaintWorkCentre");

                entity.HasOne(d => d.Maintenanceplan)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.MaintenanceplanId)
                    .HasConstraintName("FK_Tag_MaintPlan");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK_Tag_Manufacturer");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Tag_Model");

                entity.HasOne(d => d.PerformanceStandard)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.PerformanceStandardId)
                    .HasConstraintName("FK_Tag_PerformanceStandard");

                entity.HasOne(d => d.RbiSil)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.RbiSilId)
                    .HasConstraintName("FK_Tag_RbiSil");

                entity.HasOne(d => d.Rbm)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.RbmId)
                    .HasConstraintName("FK_Tag_Rbm");

                entity.HasOne(d => d.Rcm)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.RcmId)
                    .HasConstraintName("FK_Tag_Rcm");

                entity.HasOne(d => d.SubSystem)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.SubSystemId)
                    .HasConstraintName("FK_Tag_SubSystem");

                entity.HasOne(d => d.Vib)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.VibId)
                    .HasConstraintName("FK_Tag_Vib");
            });

            modelBuilder.Entity<TagEngData>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.EngDataCodeId });

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.EngDataCodeId).HasColumnName("EngDataCodeID");

                entity.Property(e => e.EngDataComment).HasMaxLength(50);

                entity.Property(e => e.EngDatasource).HasMaxLength(50);

                entity.Property(e => e.EngDatavalue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.EngDataCode)
                    .WithMany(p => p.TagEngDatas)
                    .HasForeignKey(d => d.EngDataCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tagengdata_engdatacode");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagEngDatas)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tagengdata_Tag");
            });

            modelBuilder.Entity<TagPo>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.Poid });

                entity.ToTable("TagPO");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.Poid).HasColumnName("POID");

                entity.HasOne(d => d.Po)
                    .WithMany(p => p.TagPos)
                    .HasForeignKey(d => d.Poid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagPO_PO");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagPos)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagPO_Tag");
            });

            modelBuilder.Entity<TagXdoc>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.DocId });

                entity.ToTable("TagXDoc");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.XComment)
                    .HasColumnName("xComment")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.TagXdocs)
                    .HasForeignKey(d => d.DocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagXDoc_Doc");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagXdocs)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TagXDoc_Tag");
            });

            modelBuilder.Entity<TaskListCat>(entity =>
            {
                entity.HasIndex(e => e.TaskListCatName)
                    .HasName("U_TaskListCat")
                    .IsUnique();

                entity.Property(e => e.TaskListCatId)
                    .HasColumnName("TaskListCatID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TaskListCatDesc).HasMaxLength(255);

                entity.Property(e => e.TaskListCatName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TaskListGroup>(entity =>
            {
                entity.HasIndex(e => e.TaskListGroupName)
                    .HasName("U_TaskListGroup")
                    .IsUnique();

                entity.Property(e => e.TaskListGroupId)
                    .HasColumnName("TaskListGroupID");
                    //.HasMaxLength(255);

                entity.Property(e => e.TaskListGroupDesc).HasMaxLength(255);

                entity.Property(e => e.TaskListGroupName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TaskListHeader>(entity =>
            {
                entity.HasKey(e => e.TaskListHeaderId);

                entity.HasIndex(e => e.TaskListShortText)
                    .HasName("U_TaskListHeader")
                    .IsUnique();

                //entity.Property(e => e.TaskListHeaderId)
                //    .HasColumnName("TaskListHeaderID");
                //.ValueGeneratedNever();

                entity.Property(e => e.ChangeRequired).HasMaxLength(255);

                entity.Property(e => e.MaintPackageId).HasColumnName("MaintPackageID");

                entity.Property(e => e.MaintStrategyId).HasColumnName("MaintStrategyID");

                entity.Property(e => e.MaintWorkCentreId).HasColumnName("MaintWorkCentreID");

                entity.Property(e => e.MaintenancePlantId).HasColumnName("MaintenancePlantID");

                entity.Property(e => e.PerfStdAppDel).HasMaxLength(255);

                entity.Property(e => e.PerformanceStandardId).HasColumnName("PerformanceStandardID");

                entity.Property(e => e.PmassemblyId).HasColumnName("PMAssemblyID");

                entity.Property(e => e.RegBodyAppDel).HasMaxLength(255);

                entity.Property(e => e.RegulatoryBodyId).HasColumnName("RegulatoryBodyID");

                entity.Property(e => e.ScePsReviewId).HasColumnName("ScePsReviewID");

                entity.Property(e => e.SysCondId).HasColumnName("SysCondID");

                entity.Property(e => e.TaskListClassId).HasColumnName("TaskListClassID");

                entity.Property(e => e.TaskListGroupId)
                    .IsRequired()
                    .HasColumnName("TaskListGroupID");
                    //.HasMaxLength(255);

                entity.Property(e => e.TaskListShortText)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TasklistCatId).HasColumnName("TasklistCatID");

                entity.HasOne(d => d.MaintPackage)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.MaintPackageId)
                    .HasConstraintName("FK_TaskListHeader_MaintPackage");

                entity.HasOne(d => d.MaintStrategy)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.MaintStrategyId)
                    .HasConstraintName("FK_TaskListHeader_MaintStrategy");

                entity.HasOne(d => d.MaintWorkCentre)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.MaintWorkCentreId)
                    .HasConstraintName("FK_TaskListHeader_MaintWorkCentre");

                entity.HasOne(d => d.MaintenancePlant)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.MaintenancePlantId)
                    .HasConstraintName("FK_TaskListHeader_MaintenancePlant");

                entity.HasOne(d => d.PerformanceStandard)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.PerformanceStandardId)
                    .HasConstraintName("FK_TaskListHeader_PerformanceStandard");

                entity.HasOne(d => d.Pmassembly)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.PmassemblyId)
                    .HasConstraintName("FK_TaskListHeader_PMAssembly");

                entity.HasOne(d => d.RegulatoryBody)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.RegulatoryBodyId)
                    .HasConstraintName("FK_TaskListHeader_RegulatoryBody");

                //entity.HasOne(d => d.SysCond)
                //    .WithMany(p => p.TaskListHeaders)
                //    .HasForeignKey(d => d.SysCondId)
                //    .HasConstraintName("FK_TaskListHeader_SysCond");

                //entity.HasOne(d => d.TaskListGroup)
                //    .WithMany(p => p.TaskListHeaders)
                //    .HasForeignKey(d => d.TaskListGroupId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_TaskListHeader_TaskListGroup");

                entity.HasOne(d => d.TasklistCat)
                    .WithMany(p => p.TaskListHeaders)
                    .HasForeignKey(d => d.TasklistCatId)
                    .HasConstraintName("FK_TaskListHeader_TaskListCat");
            });

            modelBuilder.Entity<TaskListOperations>(entity =>
            {
                entity.HasKey(e => e.TaskListOperationId);

                entity.Property(e => e.ChangeRequired).HasMaxLength(255);

                entity.Property(e => e.ControlKeyId).HasColumnName("ControlKeyID");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.MaintPackageId).HasColumnName("MaintPackageID");

                entity.Property(e => e.SubOperationNum).HasColumnName("SubOperationNum").HasDefaultValue(0);

                entity.Property(e => e.MaintWorkCentreId).HasColumnName("MaintWorkCentreID");

                entity.Property(e => e.MaintenancePlantId).HasColumnName("MaintenancePlantID");

                entity.Property(e => e.OperationId).HasColumnName("OperationID");

                entity.Property(e => e.OperationLongText).HasMaxLength(255);

                entity.Property(e => e.OperationShortText).HasMaxLength(255);

                entity.Property(e => e.RelationshiptoOperationId).HasColumnName("RelationshiptoOperationID");

                entity.Property(e => e.SysCondId).HasColumnName("SysCondID");

                entity.Property(e => e.TaskListHeaderId).HasColumnName("TaskListHeaderID");

                entity.Property(e => e.TaskListOperationId).HasColumnName("TaskListOPerationID");

                entity.Property(e => e.Ti).HasColumnName("TI").HasDefaultValue(false);

                entity.Property(e => e.OffSite).HasColumnName("Offsite").HasDefaultValue(false);

                entity.Property(e => e.WorkHrs).HasMaxLength(255);

                entity.HasOne(d => d.ControlKey)
                    .WithMany()
                    .HasForeignKey(d => d.ControlKeyId)
                    .HasConstraintName("FK_TaskListOperations_ControlKey");

                entity.HasOne(d => d.Doc)
                    .WithMany()
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("FK_TaskListOperations_Doc");

                entity.HasOne(d => d.MaintPackage)
                    .WithMany()
                    .HasForeignKey(d => d.MaintPackageId)
                    .HasConstraintName("FK_TaskListOperations_MaintPackage");

                entity.HasOne(d => d.MaintWorkCentre)
                    .WithMany()
                    .HasForeignKey(d => d.MaintWorkCentreId)
                    .HasConstraintName("FK_TaskListOperations_MaintWorkCentre");

                entity.HasOne(d => d.MaintenancePlant)
                    .WithMany()
                    .HasForeignKey(d => d.MaintenancePlantId)
                    .HasConstraintName("FK_TaskListOperations_MaintenancePlant");

                entity.HasOne(d => d.Operation)
                    .WithMany()
                    .HasForeignKey(d => d.OperationId)
                    .HasConstraintName("FK_TaskListOperations_Operation");

                entity.HasOne(d => d.RelationshiptoOperation)
                    .WithMany()
                    .HasForeignKey(d => d.RelationshiptoOperationId)
                    .HasConstraintName("FK_TaskListOperations_RelationshipToOperation");

                entity.HasOne(d => d.SysCond)
                    .WithMany()
                    .HasForeignKey(d => d.SysCondId)
                    .HasConstraintName("FK_TaskListOperations_SysCond");
            });

            modelBuilder.Entity<TaskListXscePsreview>(entity =>
            {
                entity.HasKey(e => new { e.ScePsreviewId, e.TaskListHeaderId });

                entity.ToTable("TaskListXScePSReview");

                entity.Property(e => e.ScePsreviewId).HasColumnName("ScePSReviewID");

                entity.Property(e => e.TaskListHeaderId).HasColumnName("TaskListHeaderID");

                entity.HasOne(d => d.ScePsreview)
                    .WithMany(p => p.TaskListXscePsreviews)
                    .HasForeignKey(d => d.ScePsreviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskListXScePSReview_ScePSReview");

                entity.HasOne(d => d.TaskListHeader)
                    .WithMany(p => p.TaskListXscePsreviews)
                    .HasForeignKey(d => d.TaskListHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskListXScePSReview_TaskListHeader");
            });

            modelBuilder.Entity<Vib>(entity =>
            {
                entity.HasIndex(e => e.VibName)
                    .HasName("U_Vib")
                    .IsUnique();

                entity.Property(e => e.VibId).HasColumnName("VibID");

                entity.Property(e => e.VibDesc)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.VibName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
