using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BccCode",
                columns: table => new
                {
                    BccCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BccCodeNumber = table.Column<int>(nullable: false),
                    BccCodeDesc = table.Column<string>(nullable: true),
                    BccColour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BccCode", x => x.BccCodeId);
                });

            migrationBuilder.CreateTable(
                name: "ClassCharacteristics",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(maxLength: 255, nullable: false),
                    ClassDesc = table.Column<string>(maxLength: 255, nullable: true),
                    Characteristic = table.Column<string>(maxLength: 255, nullable: true),
                    CharDesc = table.Column<string>(maxLength: 255, nullable: true),
                    DropdownTextValue = table.Column<string>(name: "DropdownText Value", maxLength: 255, nullable: true),
                    DropdownValDesc = table.Column<string>(maxLength: 255, nullable: true),
                    DropdownText = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCharacteristics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClassCharsTaskListHeader",
                columns: table => new
                {
                    ClassCharsTaskListHeaderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupAssociation = table.Column<string>(maxLength: 255, nullable: true),
                    CntrAssociation = table.Column<string>(maxLength: 255, nullable: true),
                    Class = table.Column<string>(maxLength: 255, nullable: true),
                    ClassDesc = table.Column<string>(maxLength: 255, nullable: true),
                    Characteristic = table.Column<string>(maxLength: 255, nullable: true),
                    CharValue = table.Column<string>(maxLength: 255, nullable: true),
                    CharDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCharsTaskListHeader", x => x.ClassCharsTaskListHeaderID);
                });

            migrationBuilder.CreateTable(
                name: "ColumnSets",
                columns: table => new
                {
                    ColumnSetsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnSetsEntity = table.Column<string>(nullable: true),
                    ColumnSetsOrder = table.Column<int>(nullable: false),
                    ColumnSetsName = table.Column<string>(nullable: true),
                    ColumnName = table.Column<string>(nullable: true),
                    ColumnOrder = table.Column<int>(nullable: false),
                    ColumnWidth = table.Column<int>(nullable: false),
                    ColumnVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnSets", x => x.ColumnSetsId);
                });

            migrationBuilder.CreateTable(
                name: "CommClass",
                columns: table => new
                {
                    CommClassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommClassName = table.Column<string>(maxLength: 255, nullable: false),
                    CommClassDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommClass", x => x.CommClassID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCode",
                columns: table => new
                {
                    CompanyCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCodeName = table.Column<string>(nullable: false),
                    CompanyCodeDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCode", x => x.CompanyCodeId);
                });

            migrationBuilder.CreateTable(
                name: "ControlKey",
                columns: table => new
                {
                    ControlKeyID = table.Column<int>(nullable: false),
                    ControlKeyName = table.Column<string>(maxLength: 255, nullable: true),
                    ControlKeyDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlKey", x => x.ControlKeyID);
                });

            migrationBuilder.CreateTable(
                name: "EngDataClass",
                columns: table => new
                {
                    EngDataClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngDataClassName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngDataClass", x => x.EngDataClassId);
                });

            migrationBuilder.CreateTable(
                name: "EngDisc",
                columns: table => new
                {
                    EngDiscID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngDiscName = table.Column<string>(maxLength: 255, nullable: false),
                    EngDiscDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngDisc", x => x.EngDiscID);
                });

            migrationBuilder.CreateTable(
                name: "EngStatus",
                columns: table => new
                {
                    EngStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngStatusName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngStatus", x => x.EngStatusID);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttribute",
                columns: table => new
                {
                    EntityAttributeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(nullable: true),
                    EntityAttributeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttribute", x => x.EntityAttributeId);
                });

            migrationBuilder.CreateTable(
                name: "EnvZone",
                columns: table => new
                {
                    EnvZoneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnvZoneName = table.Column<string>(maxLength: 50, nullable: false),
                    EnvZoneDesc = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvZone", x => x.EnvZoneID);
                });

            migrationBuilder.CreateTable(
                name: "ExMethod",
                columns: table => new
                {
                    ExMethodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExMethodName = table.Column<string>(maxLength: 255, nullable: false),
                    ExMethodDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExMethod", x => x.ExMethodID);
                });

            migrationBuilder.CreateTable(
                name: "FlocType",
                columns: table => new
                {
                    FlocTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlocTypeName = table.Column<string>(nullable: true),
                    FlocTypeDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlocType", x => x.FlocTypeId);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportId = table.Column<int>(nullable: false),
                    EntityName = table.Column<int>(nullable: false),
                    Pk1 = table.Column<int>(nullable: false),
                    Pk2 = table.Column<int>(nullable: false),
                    AttributeName = table.Column<string>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "ImportAttributeType",
                columns: table => new
                {
                    ImportAttributeTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportTypeName = table.Column<string>(nullable: true),
                    StarAttributeName = table.Column<string>(nullable: true),
                    EntityId = table.Column<int>(nullable: false),
                    StarType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportAttributeType", x => x.ImportAttributeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ImportType",
                columns: table => new
                {
                    ImportTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportType", x => x.ImportTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Ipf",
                columns: table => new
                {
                    IpfID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpfName = table.Column<string>(maxLength: 255, nullable: false),
                    IpfDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipf", x => x.IpfID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCatalog",
                columns: table => new
                {
                    ItemCatalogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCatalogClientNum = table.Column<int>(nullable: false),
                    ItemcatalogDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCatalog", x => x.ItemCatalogID);
                });

            migrationBuilder.CreateTable(
                name: "KeyList",
                columns: table => new
                {
                    KeyListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyListName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyList", x => x.KeyListId);
                });

            migrationBuilder.CreateTable(
                name: "LoadTemplate",
                columns: table => new
                {
                    LoadTemplateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadTemplateName = table.Column<string>(maxLength: 255, nullable: false),
                    LoadTemplateDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadTemplate", x => x.LoadTemplateID);
                });

            migrationBuilder.CreateTable(
                name: "LocationTypes",
                columns: table => new
                {
                    LocationTypesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationTypeDescription = table.Column<string>(nullable: false),
                    LocationsLocationTypesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypes", x => x.LocationTypesId);
                    table.ForeignKey(
                        name: "FK_LocationTypes_LocationTypes_LocationsLocationTypesId",
                        column: x => x.LocationsLocationTypesId,
                        principalTable: "LocationTypes",
                        principalColumn: "LocationTypesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintClass",
                columns: table => new
                {
                    MaintClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintClassName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintClassDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintClass", x => x.MaintClassId);
                });

            migrationBuilder.CreateTable(
                name: "MaintCriticality",
                columns: table => new
                {
                    MaintCriticalityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintCriticalityName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintCriticalityDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintCriticality", x => x.MaintCriticalityID);
                });

            migrationBuilder.CreateTable(
                name: "MaintEdcCode",
                columns: table => new
                {
                    MaintEdcCodeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintEdcCodeName = table.Column<string>(maxLength: 8, nullable: false),
                    MaintEdcCodeDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintEdcCode", x => x.MaintEdcCodeID);
                });

            migrationBuilder.CreateTable(
                name: "MaintenancePlant",
                columns: table => new
                {
                    MaintenancePlantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenancePlantNum = table.Column<string>(maxLength: 255, nullable: false),
                    MaintenancePlantDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenancePlant", x => x.MaintenancePlantID);
                });

            migrationBuilder.CreateTable(
                name: "MaintObjectType",
                columns: table => new
                {
                    MaintObjectTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintObjectTypeName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintObjectTypeDesc = table.Column<string>(maxLength: 255, nullable: false),
                    MaintObjectTypeDescExt = table.Column<string>(maxLength: 255, nullable: true),
                    StdNounModifier = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintObjectType", x => x.MaintObjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MaintPackage",
                columns: table => new
                {
                    MaintPackageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintPackageName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintPackageCycleLength = table.Column<int>(nullable: false),
                    MaintPackageCycleUnit = table.Column<string>(maxLength: 255, nullable: true),
                    MaintPackageCycleText = table.Column<string>(maxLength: 255, nullable: true),
                    MaintPackageSeq = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintPackage", x => x.MaintPackageID);
                });

            migrationBuilder.CreateTable(
                name: "MaintPlannerGroup",
                columns: table => new
                {
                    MaintPlannerGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintPlannerGroupName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintPlannerGroupDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintPlannerGroup", x => x.MaintPlannerGroupID);
                });

            migrationBuilder.CreateTable(
                name: "MaintQueryNote",
                columns: table => new
                {
                    MaintQueryNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintQueryID = table.Column<int>(nullable: true),
                    MaintQueryNoteBy = table.Column<string>(maxLength: 255, nullable: true),
                    MaintQueryNoteDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaintQueryNoteText = table.Column<string>(nullable: true),
                    MaintQueryNoteAttachments = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintQueryNote", x => x.MaintQueryNoteID);
                });

            migrationBuilder.CreateTable(
                name: "MaintScePsReviewTeam",
                columns: table => new
                {
                    MaintScePsReviewTeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintScePsReviewTeamName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintScePsReviewTeamDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintScePsReviewTeam", x => x.MaintScePsReviewTeamID);
                });

            migrationBuilder.CreateTable(
                name: "MaintSortProcess",
                columns: table => new
                {
                    MaintSortProcessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintSortProcessName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintSortProcessDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintSortProcess", x => x.MaintSortProcessID);
                });

            migrationBuilder.CreateTable(
                name: "MaintStatus",
                columns: table => new
                {
                    MaintStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintStatusName = table.Column<string>(nullable: false),
                    MaintStatusDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintStatus", x => x.MaintStatusId);
                });

            migrationBuilder.CreateTable(
                name: "MaintStructureIndicator",
                columns: table => new
                {
                    MaintStructureIndicatorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintStructureIndicatorName = table.Column<string>(maxLength: 5, nullable: false),
                    MaintStructureIndicatorDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintStructureIndicator", x => x.MaintStructureIndicatorID);
                });

            migrationBuilder.CreateTable(
                name: "MaintType",
                columns: table => new
                {
                    MaintTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintTypeName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintTypeDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintType", x => x.MaintTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MaintWorkCentre",
                columns: table => new
                {
                    MaintWorkCentreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintWorkCentreName = table.Column<string>(maxLength: 8, nullable: false),
                    MaintWorkCentreDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintWorkCentre", x => x.MaintWorkCentreID);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ManufacturerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(maxLength: 255, nullable: false),
                    ManufacturerDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ManufacturerID);
                });

            migrationBuilder.CreateTable(
                name: "MeasPoint",
                columns: table => new
                {
                    MeasPointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasPointData = table.Column<string>(maxLength: 2056, nullable: false),
                    MeasPointName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasPoint", x => x.MeasPointID);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    OperationID = table.Column<int>(nullable: false),
                    OperationNAme = table.Column<string>(maxLength: 255, nullable: false),
                    OperationNotes = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.OperationID);
                });

            migrationBuilder.CreateTable(
                name: "Pbs",
                columns: table => new
                {
                    PbsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PbsName = table.Column<string>(maxLength: 50, nullable: false),
                    PbsDesc = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pbs", x => x.PbsID);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceStandard",
                columns: table => new
                {
                    PerformanceStandardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceStandardName = table.Column<string>(maxLength: 255, nullable: false),
                    PerformanceStandardDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceStandard", x => x.PerformanceStandardID);
                });

            migrationBuilder.CreateTable(
                name: "PlannerPlant",
                columns: table => new
                {
                    PlannerPlantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannerPlantName = table.Column<string>(nullable: false),
                    PlannerPlantDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerPlant", x => x.PlannerPlantId);
                });

            migrationBuilder.CreateTable(
                name: "PlantSection",
                columns: table => new
                {
                    PlantSectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantSectionName = table.Column<string>(maxLength: 255, nullable: false),
                    PlantSectionDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSection", x => x.PlantSectionID);
                });

            migrationBuilder.CreateTable(
                name: "PMAssembly",
                columns: table => new
                {
                    PMAssemblyID = table.Column<int>(nullable: false),
                    PMAssemblyName = table.Column<string>(maxLength: 255, nullable: false),
                    ShortText = table.Column<string>(maxLength: 255, nullable: true),
                    Make = table.Column<string>(maxLength: 255, nullable: true),
                    Model = table.Column<string>(maxLength: 255, nullable: true),
                    Rev = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PMAssembly", x => x.PMAssemblyID);
                });

            migrationBuilder.CreateTable(
                name: "PO",
                columns: table => new
                {
                    POID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POName = table.Column<string>(maxLength: 255, nullable: false),
                    POCompany = table.Column<string>(maxLength: 255, nullable: true),
                    PODesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PO", x => x.POID);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityName = table.Column<string>(nullable: true),
                    PriorityDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "RbiSil",
                columns: table => new
                {
                    RbiSilID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RbiSilName = table.Column<string>(maxLength: 255, nullable: false),
                    RbiSilDesc = table.Column<string>(maxLength: 255, nullable: false),
                    RbiSilDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RbiSil", x => x.RbiSilID);
                });

            migrationBuilder.CreateTable(
                name: "Rbm",
                columns: table => new
                {
                    RbmID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RbmName = table.Column<string>(maxLength: 255, nullable: false),
                    RbmDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rbm", x => x.RbmID);
                });

            migrationBuilder.CreateTable(
                name: "Rcm",
                columns: table => new
                {
                    RcmID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RcmName = table.Column<string>(maxLength: 255, nullable: false),
                    RcmDesc = table.Column<string>(maxLength: 255, nullable: false),
                    RcmDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rcm", x => x.RcmID);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryBody",
                columns: table => new
                {
                    RegulatoryBodyID = table.Column<int>(nullable: false),
                    RegulatoryBodyName = table.Column<string>(maxLength: 255, nullable: false),
                    RegulatoryBodyDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryBody", x => x.RegulatoryBodyID);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipType",
                columns: table => new
                {
                    RelationshipTypeID = table.Column<int>(nullable: false),
                    RelationshipTypeName = table.Column<string>(maxLength: 255, nullable: false),
                    RelationshipTypeDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipType", x => x.RelationshipTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SAPExportDetail",
                columns: table => new
                {
                    SAPExportDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutputName = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    SheetName = table.Column<string>(nullable: true),
                    ColumnOrder = table.Column<int>(nullable: false),
                    ColumnHeader_Legible = table.Column<string>(nullable: true),
                    ColumnHeader_SAP = table.Column<string>(nullable: true),
                    PathName = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    limit = table.Column<int>(nullable: false),
                    Mandatory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPExportDetail", x => x.SAPExportDetailId);
                });

            migrationBuilder.CreateTable(
                name: "SAPStatus",
                columns: table => new
                {
                    SAPStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ColourCode = table.Column<string>(nullable: true),
                    FontColourCode = table.Column<string>(nullable: true),
                    ForSAPExport = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPStatus", x => x.SAPStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ScePSReview",
                columns: table => new
                {
                    ScePSReviewID = table.Column<int>(nullable: false),
                    ScePSReviewName = table.Column<string>(maxLength: 255, nullable: false),
                    ScePSReviewDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScePSReview", x => x.ScePSReviewID);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingPeriodUOM",
                columns: table => new
                {
                    SchedulingPeriodUOMId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchedulingPeriodUOMName = table.Column<string>(nullable: false),
                    SchedulingPeriodUOMDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingPeriodUOM", x => x.SchedulingPeriodUOMId);
                });

            migrationBuilder.CreateTable(
                name: "SortField",
                columns: table => new
                {
                    SortFieldId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortFieldName = table.Column<string>(nullable: false),
                    SortFieldDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortField", x => x.SortFieldId);
                });

            migrationBuilder.CreateTable(
                name: "SuperClass",
                columns: table => new
                {
                    SuperclassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperclassName = table.Column<string>(maxLength: 50, nullable: false),
                    Superclassdescription = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperClass", x => x.SuperclassID);
                });

            migrationBuilder.CreateTable(
                name: "SysCond",
                columns: table => new
                {
                    SysCondID = table.Column<int>(nullable: false),
                    SysCondName = table.Column<string>(maxLength: 255, nullable: false),
                    SysCondDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCond", x => x.SysCondID);
                });

            migrationBuilder.CreateTable(
                name: "System",
                columns: table => new
                {
                    SystemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemNum = table.Column<string>(maxLength: 255, nullable: false),
                    SystemName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System", x => x.SystemID);
                });

            migrationBuilder.CreateTable(
                name: "TagView",
                columns: table => new
                {
                    TagViewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagViewName = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagView", x => x.TagViewId);
                });

            migrationBuilder.CreateTable(
                name: "TaskListCat",
                columns: table => new
                {
                    TaskListCatID = table.Column<int>(nullable: false),
                    TaskListCatName = table.Column<string>(maxLength: 255, nullable: false),
                    TaskListCatDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListCat", x => x.TaskListCatID);
                });

            migrationBuilder.CreateTable(
                name: "TaskListGroup",
                columns: table => new
                {
                    TaskListGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskListGroupName = table.Column<string>(maxLength: 255, nullable: false),
                    TaskListGroupDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListGroup", x => x.TaskListGroupID);
                });

            migrationBuilder.CreateTable(
                name: "UC2ViewColumns",
                columns: table => new
                {
                    UC2ViewColumnsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sectionOrder = table.Column<int>(nullable: false),
                    sectionName = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC2ViewColumns", x => x.UC2ViewColumnsId);
                });

            migrationBuilder.CreateTable(
                name: "Vib",
                columns: table => new
                {
                    VibID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VibName = table.Column<string>(maxLength: 255, nullable: false),
                    VibDesc = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vib", x => x.VibID);
                });

            migrationBuilder.CreateTable(
                name: "WBSElement",
                columns: table => new
                {
                    WBSElementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WBSElementName = table.Column<string>(nullable: false),
                    WBSElementDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WBSElement", x => x.WBSElementId);
                });

            migrationBuilder.CreateTable(
                name: "EngDataCode",
                columns: table => new
                {
                    EngDataCodeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngDataCodeName = table.Column<string>(maxLength: 255, nullable: false),
                    EngDataCodeDesc = table.Column<string>(maxLength: 255, nullable: true),
                    EngDataCodeNotes = table.Column<string>(maxLength: 50, nullable: true),
                    HideFromUI = table.Column<bool>(nullable: false),
                    EngDataCodeSAPDesc = table.Column<string>(maxLength: 255, nullable: true),
                    EngDataCodeDDLType = table.Column<string>(maxLength: 255, nullable: true),
                    EngDataClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngDataCode", x => x.EngDataCodeID);
                    table.ForeignKey(
                        name: "FK_EngDataCode_EngDataClass_EngDataClassId",
                        column: x => x.EngDataClassId,
                        principalTable: "EngDataClass",
                        principalColumn: "EngDataClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocType",
                columns: table => new
                {
                    DocTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTypeName = table.Column<string>(maxLength: 255, nullable: false),
                    DocTypeDesc = table.Column<string>(maxLength: 255, nullable: true),
                    DocTypeDiscId = table.Column<int>(nullable: false),
                    EngDiscId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocType", x => x.DocTypeID);
                    table.ForeignKey(
                        name: "FK_DocType_EngDisc_EngDiscId",
                        column: x => x.EngDiscId,
                        principalTable: "EngDisc",
                        principalColumn: "EngDiscID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttributeRequirement",
                columns: table => new
                {
                    EntityAttributeRequirementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityAttributeId = table.Column<int>(nullable: false),
                    EntityAttributeRequirementType = table.Column<string>(nullable: true),
                    EntityAttributeRequirementCondition = table.Column<string>(nullable: true),
                    EntityAttributeRequirementValue = table.Column<string>(nullable: true),
                    EntityAttributeRequirementValueType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttributeRequirement", x => x.EntityAttributeRequirementId);
                    table.ForeignKey(
                        name: "FK_EntityAttributeRequirement_EntityAttribute_EntityAttributeId",
                        column: x => x.EntityAttributeId,
                        principalTable: "EntityAttribute",
                        principalColumn: "EntityAttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Import",
                columns: table => new
                {
                    ImportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportStatus = table.Column<string>(nullable: true),
                    ImportTypeId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedComment = table.Column<string>(nullable: false),
                    Approved = table.Column<DateTime>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import", x => x.ImportId);
                    table.ForeignKey(
                        name: "FK_Import_ImportType_ImportTypeId",
                        column: x => x.ImportTypeId,
                        principalTable: "ImportType",
                        principalColumn: "ImportTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintLoad",
                columns: table => new
                {
                    MaintLoadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintLoadNum = table.Column<string>(maxLength: 255, nullable: false),
                    LoadTemplateID = table.Column<int>(nullable: false),
                    MaintLoadComment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintLoad", x => x.MaintLoadID);
                    table.ForeignKey(
                        name: "FK_MaintLoad_LoadTemplate",
                        column: x => x.LoadTemplateID,
                        principalTable: "LoadTemplate",
                        principalColumn: "LoadTemplateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenancePlantID = table.Column<int>(nullable: false),
                    AreaName = table.Column<string>(maxLength: 50, nullable: false),
                    AreaDisc = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaID);
                    table.ForeignKey(
                        name: "FK_Area_MaintenancePlant_MaintenancePlantID",
                        column: x => x.MaintenancePlantID,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(maxLength: 25, nullable: false),
                    ProjectName = table.Column<string>(maxLength: 255, nullable: true),
                    MaintenancePlantID = table.Column<int>(nullable: false),
                    MaintenanceRootTagId = table.Column<int>(nullable: false),
                    MaintHierarchy_LoadDepth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_Project_MaintenancePlant",
                        column: x => x.MaintenancePlantID,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintObjectTypeXMaintClass",
                columns: table => new
                {
                    MaintObjectTypeId = table.Column<int>(nullable: false),
                    MaintClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintObjectTypeXMaintClass", x => new { x.MaintObjectTypeId, x.MaintClassId });
                    table.ForeignKey(
                        name: "FK_MaintObjectTypeXMaintClass_MaintClass_MaintClassId",
                        column: x => x.MaintClassId,
                        principalTable: "MaintClass",
                        principalColumn: "MaintClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintObjectTypeXMaintClass_MaintObjectType_MaintObjectTypeId",
                        column: x => x.MaintObjectTypeId,
                        principalTable: "MaintObjectType",
                        principalColumn: "MaintObjectTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintQuery",
                columns: table => new
                {
                    MaintQueryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintQueryNoteID = table.Column<int>(nullable: true),
                    MaintQueryNum = table.Column<string>(maxLength: 255, nullable: false),
                    MaintQueryTitle = table.Column<string>(maxLength: 255, nullable: true),
                    MaintQueryRaisedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaintQueryRaisedBy = table.Column<string>(maxLength: 255, nullable: true),
                    MaintQueryLongDesc = table.Column<string>(nullable: true),
                    MaintQueryClosedBy = table.Column<string>(maxLength: 255, nullable: true),
                    MaintQueryClosedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MaintQueryClosingNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintQuery", x => x.MaintQueryID);
                    table.ForeignKey(
                        name: "FK_MaintQuery_MaintQueryNote",
                        column: x => x.MaintQueryNoteID,
                        principalTable: "MaintQueryNote",
                        principalColumn: "MaintQueryNoteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(maxLength: 255, nullable: false),
                    ModelDesc = table.Column<string>(maxLength: 255, nullable: true),
                    ManufacturerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Models_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintArea",
                columns: table => new
                {
                    MaintAreaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantSectionID = table.Column<int>(nullable: true),
                    MaintAreaName = table.Column<string>(maxLength: 4, nullable: false),
                    MaintAreaDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintArea", x => x.MaintAreaId);
                    table.ForeignKey(
                        name: "FK_MaintArea_PlantSection",
                        column: x => x.PlantSectionID,
                        principalTable: "PlantSection",
                        principalColumn: "PlantSectionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipToOperation",
                columns: table => new
                {
                    RelationshipToOperationID = table.Column<int>(nullable: false),
                    RelationshipToOperationName = table.Column<string>(maxLength: 255, nullable: true),
                    RelationshipTypeID = table.Column<int>(nullable: true),
                    RelationshipToOperationNotes = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipToOperation", x => x.RelationshipToOperationID);
                    table.ForeignKey(
                        name: "FK_RelationshipToOperation_RelationshipType",
                        column: x => x.RelationshipTypeID,
                        principalTable: "RelationshipType",
                        principalColumn: "RelationshipTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngClass",
                columns: table => new
                {
                    EngClassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngClassName = table.Column<string>(maxLength: 255, nullable: false),
                    EngClassDesc = table.Column<string>(maxLength: 255, nullable: true),
                    SuperClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngClass", x => x.EngClassID);
                    table.ForeignKey(
                        name: "FK_EngClass_SuperClass_SuperClassID",
                        column: x => x.SuperClassID,
                        principalTable: "SuperClass",
                        principalColumn: "SuperclassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSystem",
                columns: table => new
                {
                    SubSystemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubSystemNum = table.Column<string>(maxLength: 255, nullable: false),
                    SubSystemName = table.Column<string>(maxLength: 255, nullable: true),
                    SystemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSystem", x => x.SubSystemID);
                    table.ForeignKey(
                        name: "FK_SubSystem_System",
                        column: x => x.SystemID,
                        principalTable: "System",
                        principalColumn: "SystemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagViewColumns",
                columns: table => new
                {
                    TagViewColumnsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagViewId = table.Column<int>(nullable: false),
                    TagViewOrder = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true),
                    ColumnWidth = table.Column<int>(nullable: false),
                    starField = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagViewColumns", x => x.TagViewColumnsId);
                    table.ForeignKey(
                        name: "FK_TagViewColumns_TagView_TagViewId",
                        column: x => x.TagViewId,
                        principalTable: "TagView",
                        principalColumn: "TagViewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UC2ViewColumnsUser",
                columns: table => new
                {
                    UC2ViewColumnsUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UC2ViewColumnsId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC2ViewColumnsUser", x => x.UC2ViewColumnsUserId);
                    table.ForeignKey(
                        name: "FK_UC2ViewColumnsUser_UC2ViewColumns_UC2ViewColumnsId",
                        column: x => x.UC2ViewColumnsId,
                        principalTable: "UC2ViewColumns",
                        principalColumn: "UC2ViewColumnsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EngDataCodeDropDown",
                columns: table => new
                {
                    EngDataCodeDropDownId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngDataCodeId = table.Column<int>(nullable: false),
                    EngDataCodeDropDownValue = table.Column<string>(maxLength: 255, nullable: true),
                    EngDataCodeDropDownDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngDataCodeDropDown", x => x.EngDataCodeDropDownId);
                    table.ForeignKey(
                        name: "FK_EngDataCodeDropDown_EngDataCode_EngDataCodeId",
                        column: x => x.EngDataCodeId,
                        principalTable: "EngDataCode",
                        principalColumn: "EngDataCodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyListxEngDataCode",
                columns: table => new
                {
                    KeyListxEngDataCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyListId = table.Column<int>(nullable: false),
                    EngDataCode = table.Column<int>(nullable: false),
                    ColumnNumber = table.Column<int>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    EngDataCodesEngDataCodeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyListxEngDataCode", x => x.KeyListxEngDataCodeId);
                    table.ForeignKey(
                        name: "FK_KeyListxEngDataCode_EngDataCode_EngDataCodesEngDataCodeId",
                        column: x => x.EngDataCodesEngDataCodeId,
                        principalTable: "EngDataCode",
                        principalColumn: "EngDataCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KeyListxEngDataCode_KeyList_KeyListId",
                        column: x => x.KeyListId,
                        principalTable: "KeyList",
                        principalColumn: "KeyListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintClassXEngDataCode",
                columns: table => new
                {
                    MaintClassId = table.Column<int>(nullable: false),
                    EngDataCodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintClassXEngDataCode", x => new { x.MaintClassId, x.EngDataCodeId });
                    table.ForeignKey(
                        name: "FK_MaintClassXEngDataCode_EngDataCode_EngDataCodeId",
                        column: x => x.EngDataCodeId,
                        principalTable: "EngDataCode",
                        principalColumn: "EngDataCodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintClassXEngDataCode_MaintClass_MaintClassId",
                        column: x => x.MaintClassId,
                        principalTable: "MaintClass",
                        principalColumn: "MaintClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doc",
                columns: table => new
                {
                    DocID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocNum = table.Column<string>(maxLength: 25, nullable: false),
                    DocAlias = table.Column<string>(maxLength: 25, nullable: true),
                    DocTitle = table.Column<string>(maxLength: 255, nullable: true),
                    DocTypeID = table.Column<int>(nullable: true),
                    DocLink = table.Column<string>(maxLength: 255, nullable: true),
                    DocComments = table.Column<string>(maxLength: 255, nullable: true),
                    DocSource = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doc", x => x.DocID);
                    table.ForeignKey(
                        name: "FK_Doc_DocType",
                        column: x => x.DocTypeID,
                        principalTable: "DocType",
                        principalColumn: "DocTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImportExtract",
                columns: table => new
                {
                    ImportExtractId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityPseudoPK = table.Column<string>(nullable: true),
                    EntityPseudoPK2 = table.Column<string>(nullable: true),
                    EntityPseudoFKName = table.Column<string>(nullable: true),
                    EntityPseudoFKValue = table.Column<string>(nullable: true),
                    EntityPseudoFK2Name = table.Column<string>(nullable: true),
                    EntityPseudoFK2Value = table.Column<string>(nullable: true),
                    AttributeName = table.Column<string>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportExtract", x => x.ImportExtractId);
                    table.ForeignKey(
                        name: "FK_ImportExtract_Import_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Import",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportTransform",
                columns: table => new
                {
                    ImportTransformId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportId = table.Column<int>(nullable: false),
                    LoadType = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    EntityPK = table.Column<int>(nullable: false),
                    EntityPseudoPK = table.Column<string>(nullable: true),
                    EntityPK2 = table.Column<int>(nullable: false),
                    AttributeName = table.Column<string>(nullable: true),
                    AttributeNameOrg = table.Column<string>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true),
                    AttributeValueTxt = table.Column<string>(nullable: true),
                    AttributeValueOldTxt = table.Column<string>(nullable: true),
                    AttributeValueOld = table.Column<string>(nullable: true),
                    AttributeValueType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportTransform", x => x.ImportTransformId);
                    table.ForeignKey(
                        name: "FK_ImportTransform_Import_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Import",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: false),
                    LocationName = table.Column<string>(maxLength: 50, nullable: false),
                    LocationDesc = table.Column<string>(maxLength: 255, nullable: false),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Elevation = table.Column<int>(nullable: true),
                    ParentLocationID = table.Column<int>(nullable: true),
                    LocationType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Location_Area",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Location_ParentLocationID",
                        column: x => x.ParentLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommZone",
                columns: table => new
                {
                    CommZoneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(nullable: false),
                    CommZoneName = table.Column<string>(maxLength: 255, nullable: false),
                    CommZoneDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommZone", x => x.CommZoneID);
                    table.ForeignKey(
                        name: "FK_CommZone_Project",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SP",
                columns: table => new
                {
                    SPID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(nullable: false),
                    SPnum = table.Column<string>(maxLength: 50, nullable: false),
                    SPdesc = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP", x => x.SPID);
                    table.ForeignKey(
                        name: "FK_SP_Project",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    EquipTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipTypeDesc = table.Column<string>(nullable: true),
                    ModelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.EquipTypeID);
                    table.ForeignKey(
                        name: "FK_EquipmentTypes_Models_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Models",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintLocation",
                columns: table => new
                {
                    MaintLocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintAreaId = table.Column<int>(nullable: true),
                    MaintLocationName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintLocationDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintLocation", x => x.MaintLocationID);
                    table.ForeignKey(
                        name: "FK_MaintLocation_MaintArea",
                        column: x => x.MaintAreaId,
                        principalTable: "MaintArea",
                        principalColumn: "MaintAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngClassRequiredDocs",
                columns: table => new
                {
                    EngClassRequiredDocsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngClassId = table.Column<int>(nullable: false),
                    DocTypeId = table.Column<int>(nullable: false),
                    BCC = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngClassRequiredDocs", x => x.EngClassRequiredDocsId);
                    table.ForeignKey(
                        name: "FK_EngClassRequiredDocs_DocType_DocTypeId",
                        column: x => x.DocTypeId,
                        principalTable: "DocType",
                        principalColumn: "DocTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngClassRequiredDocs_EngClass_EngClassId",
                        column: x => x.EngClassId,
                        principalTable: "EngClass",
                        principalColumn: "EngClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EngDataClassxEngDataCode",
                columns: table => new
                {
                    EngDataClassxEngDataCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngClassId = table.Column<int>(nullable: false),
                    EngDataCodeId = table.Column<int>(nullable: false),
                    BccCodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngDataClassxEngDataCode", x => x.EngDataClassxEngDataCodeId);
                    table.ForeignKey(
                        name: "FK_EngDataClassxEngDataCode_BccCode_BccCodeId",
                        column: x => x.BccCodeId,
                        principalTable: "BccCode",
                        principalColumn: "BccCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngDataClassxEngDataCode_EngClass_EngClassId",
                        column: x => x.EngClassId,
                        principalTable: "EngClass",
                        principalColumn: "EngClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EngDataClassxEngDataCode_EngDataCode_EngDataCodeId",
                        column: x => x.EngDataCodeId,
                        principalTable: "EngDataCode",
                        principalColumn: "EngDataCodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyListxEngClass",
                columns: table => new
                {
                    KeyListxEngClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyListId = table.Column<int>(nullable: false),
                    EngClassID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyListxEngClass", x => x.KeyListxEngClassId);
                    table.ForeignKey(
                        name: "FK_KeyListxEngClass_EngClass_EngClassID",
                        column: x => x.EngClassID,
                        principalTable: "EngClass",
                        principalColumn: "EngClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyListxEngClass_KeyList_KeyListId",
                        column: x => x.KeyListId,
                        principalTable: "KeyList",
                        principalColumn: "KeyListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagViewColumnsUser",
                columns: table => new
                {
                    TagViewColumnsUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagViewColumnsId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    ColumnWidth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagViewColumnsUser", x => x.TagViewColumnsUserId);
                    table.ForeignKey(
                        name: "FK_TagViewColumnsUser_TagViewColumns_TagViewColumnsId",
                        column: x => x.TagViewColumnsId,
                        principalTable: "TagViewColumns",
                        principalColumn: "TagViewColumnsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintStrategy",
                columns: table => new
                {
                    MaintStrategyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocID = table.Column<int>(nullable: false),
                    MaintStrategyName = table.Column<string>(maxLength: 255, nullable: false),
                    MaintStrategyDesc = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintStrategy", x => x.MaintStrategyID);
                    table.ForeignKey(
                        name: "FK_MaintStrategy_Doc",
                        column: x => x.DocID,
                        principalTable: "Doc",
                        principalColumn: "DocID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportError",
                columns: table => new
                {
                    ImportErrorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportId = table.Column<int>(nullable: false),
                    ImportExtractId = table.Column<int>(nullable: true),
                    ImportTransformId = table.Column<int>(nullable: true),
                    ErrorVector = table.Column<string>(nullable: true),
                    ErrorDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportError", x => x.ImportErrorId);
                    table.ForeignKey(
                        name: "FK_ImportError_ImportExtract_ImportExtractId",
                        column: x => x.ImportExtractId,
                        principalTable: "ImportExtract",
                        principalColumn: "ImportExtractId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportError_Import_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Import",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportError_ImportTransform_ImportTransformId",
                        column: x => x.ImportTransformId,
                        principalTable: "ImportTransform",
                        principalColumn: "ImportTransformId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommSubSystem",
                columns: table => new
                {
                    CommSubSystemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemID = table.Column<int>(nullable: false),
                    CommSubSystemNo = table.Column<string>(maxLength: 255, nullable: false),
                    CommSubSystemName = table.Column<string>(maxLength: 255, nullable: true),
                    SPID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommSubSystem", x => x.CommSubSystemID);
                    table.ForeignKey(
                        name: "FK_CommSubSystem_SP",
                        column: x => x.SPID,
                        principalTable: "SP",
                        principalColumn: "SPID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommSubSystem_System",
                        column: x => x.SystemID,
                        principalTable: "System",
                        principalColumn: "SystemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BOM",
                columns: table => new
                {
                    BOMID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipTypeID = table.Column<int>(nullable: false),
                    ItemCatalogID = table.Column<int>(nullable: false),
                    ItemQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOM", x => x.BOMID);
                    table.ForeignKey(
                        name: "FK_BOM_EquipmentTypes_EquipTypeID",
                        column: x => x.EquipTypeID,
                        principalTable: "EquipmentTypes",
                        principalColumn: "EquipTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOM_ItemCatalog_ItemCatalogID",
                        column: x => x.ItemCatalogID,
                        principalTable: "ItemCatalog",
                        principalColumn: "ItemCatalogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintPlan",
                columns: table => new
                {
                    MaintPlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintPlanName = table.Column<string>(maxLength: 255, nullable: false),
                    ShortText = table.Column<string>(maxLength: 255, nullable: false),
                    MaintStrategyID = table.Column<int>(nullable: true),
                    MaintSortProcessID = table.Column<int>(nullable: false),
                    Sort = table.Column<string>(maxLength: 255, nullable: true),
                    CycleModFactor = table.Column<double>(maxLength: 255, nullable: true),
                    StartDate = table.Column<string>(maxLength: 255, nullable: true),
                    MeasPointID = table.Column<int>(nullable: true),
                    ChangeStatus = table.Column<string>(maxLength: 255, nullable: true),
                    StartingInstructions = table.Column<string>(maxLength: 255, nullable: true),
                    CallHorizon = table.Column<string>(maxLength: 255, nullable: true),
                    SchedulingPeriodValue = table.Column<int>(maxLength: 255, nullable: true),
                    SchedulingPeriodUomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintPlan", x => x.MaintPlanID);
                    table.ForeignKey(
                        name: "FK_MaintPlan_MaintSortProcess",
                        column: x => x.MaintSortProcessID,
                        principalTable: "MaintSortProcess",
                        principalColumn: "MaintSortProcessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintPlan_MaintStrategy",
                        column: x => x.MaintStrategyID,
                        principalTable: "MaintStrategy",
                        principalColumn: "MaintStrategyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintPlan_MeasPoint",
                        column: x => x.MeasPointID,
                        principalTable: "MeasPoint",
                        principalColumn: "MeasPointID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskListHeader",
                columns: table => new
                {
                    TaskListHeaderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskListGroupID = table.Column<int>(nullable: false),
                    Counter = table.Column<int>(nullable: false),
                    TaskListShortText = table.Column<string>(maxLength: 255, nullable: false),
                    MaintWorkCentreID = table.Column<int>(nullable: true),
                    MaintenancePlantID = table.Column<int>(nullable: true),
                    SysCondID = table.Column<int>(nullable: true),
                    MaintStrategyID = table.Column<int>(nullable: true),
                    MaintPackageID = table.Column<int>(nullable: true),
                    PMAssemblyID = table.Column<int>(nullable: true),
                    TasklistCatID = table.Column<int>(nullable: true),
                    PerformanceStandardID = table.Column<int>(nullable: true),
                    PerfStdAppDel = table.Column<string>(maxLength: 255, nullable: true),
                    ScePsReviewID = table.Column<int>(nullable: true),
                    RegulatoryBodyID = table.Column<int>(nullable: true),
                    RegBodyAppDel = table.Column<string>(maxLength: 255, nullable: true),
                    ChangeRequired = table.Column<string>(maxLength: 255, nullable: true),
                    TaskListClassID = table.Column<int>(nullable: true),
                    MaintPlannerGroupId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListHeader", x => x.TaskListHeaderId);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_MaintPackage",
                        column: x => x.MaintPackageID,
                        principalTable: "MaintPackage",
                        principalColumn: "MaintPackageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_MaintPlannerGroup_MaintPlannerGroupId",
                        column: x => x.MaintPlannerGroupId,
                        principalTable: "MaintPlannerGroup",
                        principalColumn: "MaintPlannerGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_MaintStrategy",
                        column: x => x.MaintStrategyID,
                        principalTable: "MaintStrategy",
                        principalColumn: "MaintStrategyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_MaintWorkCentre",
                        column: x => x.MaintWorkCentreID,
                        principalTable: "MaintWorkCentre",
                        principalColumn: "MaintWorkCentreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_MaintenancePlant",
                        column: x => x.MaintenancePlantID,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_PerformanceStandard",
                        column: x => x.PerformanceStandardID,
                        principalTable: "PerformanceStandard",
                        principalColumn: "PerformanceStandardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_PMAssembly",
                        column: x => x.PMAssemblyID,
                        principalTable: "PMAssembly",
                        principalColumn: "PMAssemblyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_RegulatoryBody",
                        column: x => x.RegulatoryBodyID,
                        principalTable: "RegulatoryBody",
                        principalColumn: "RegulatoryBodyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_SAPStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SAPStatus",
                        principalColumn: "SAPStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_SysCond_SysCondID",
                        column: x => x.SysCondID,
                        principalTable: "SysCond",
                        principalColumn: "SysCondID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_TaskListGroup_TaskListGroupID",
                        column: x => x.TaskListGroupID,
                        principalTable: "TaskListGroup",
                        principalColumn: "TaskListGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskListHeader_TaskListCat",
                        column: x => x.TasklistCatID,
                        principalTable: "TaskListCat",
                        principalColumn: "TaskListCatID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagNumber = table.Column<string>(maxLength: 50, nullable: false),
                    TagService = table.Column<string>(maxLength: 255, nullable: true),
                    TagFLOC = table.Column<string>(maxLength: 50, nullable: true),
                    SubSystemID = table.Column<int>(nullable: true),
                    EngClassID = table.Column<int>(nullable: true),
                    EngParentID = table.Column<int>(nullable: true),
                    MaintParentID = table.Column<int>(nullable: true),
                    EngDiscId = table.Column<int>(nullable: true),
                    MaintLocationID = table.Column<int>(nullable: true),
                    LocationID = table.Column<int>(nullable: true),
                    MaintTypeID = table.Column<int>(nullable: true),
                    MaintStatusID = table.Column<int>(nullable: true),
                    EngStatusID = table.Column<int>(nullable: true),
                    MaintWorkCentreID = table.Column<int>(nullable: true),
                    MaintEdcCodeId = table.Column<int>(nullable: true),
                    MaintStructureIndicatorID = table.Column<int>(nullable: true),
                    CommissioningSubsystemID = table.Column<int>(nullable: true),
                    CommClassID = table.Column<int>(nullable: true),
                    CommZoneID = table.Column<int>(nullable: true),
                    MaintPlannerGroupID = table.Column<int>(nullable: true),
                    MaintenanceplanID = table.Column<int>(nullable: true),
                    MaintCriticalityID = table.Column<int>(nullable: true),
                    PerformanceStandardID = table.Column<int>(nullable: true),
                    KeyDocID = table.Column<int>(nullable: true),
                    PoID = table.Column<int>(nullable: true),
                    TagSource = table.Column<string>(maxLength: 255, nullable: true),
                    TagDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    RTF = table.Column<bool>(nullable: false),
                    TagFlocDesc = table.Column<string>(maxLength: 100, nullable: true),
                    FlocTypeId = table.Column<int>(nullable: true),
                    TagMaintQuery = table.Column<bool>(nullable: false, defaultValue: false),
                    TagComment = table.Column<string>(nullable: true),
                    MaintenancePlantId = table.Column<int>(nullable: true),
                    ModelID = table.Column<int>(nullable: true),
                    ModelDescription = table.Column<string>(nullable: true),
                    VibID = table.Column<int>(nullable: true),
                    Tagnoneng = table.Column<bool>(nullable: false, defaultValue: false),
                    TagVendorTag = table.Column<string>(maxLength: 50, nullable: true),
                    MaintObjectTypeID = table.Column<int>(nullable: true),
                    RbiSilID = table.Column<int>(nullable: true),
                    IpfID = table.Column<int>(nullable: true),
                    RcmID = table.Column<int>(nullable: true),
                    TagRawNumber = table.Column<string>(maxLength: 128, nullable: true),
                    TagRawDesc = table.Column<string>(maxLength: 255, nullable: true),
                    MaintScePsReviewTeamID = table.Column<int>(nullable: true),
                    MaintScePsJustification = table.Column<string>(maxLength: 50, nullable: true),
                    TagMaintCritComments = table.Column<string>(maxLength: 50, nullable: true),
                    RbmID = table.Column<int>(nullable: true),
                    ManufacturerID = table.Column<int>(nullable: true),
                    ExMethodID = table.Column<int>(nullable: true),
                    TagRbmMethod = table.Column<string>(maxLength: 50, nullable: true),
                    TagVib = table.Column<string>(maxLength: 5, nullable: true),
                    TagSrcKeyList = table.Column<string>(maxLength: 40, nullable: true),
                    TagBomReq = table.Column<string>(maxLength: 4, nullable: true),
                    TagSpNo = table.Column<string>(maxLength: 10, nullable: true),
                    MaintSortProcessID = table.Column<int>(nullable: true),
                    TagCharacteristic = table.Column<string>(maxLength: 255, nullable: true),
                    TagCharValue = table.Column<string>(maxLength: 255, nullable: true),
                    TagCharDesc = table.Column<string>(maxLength: 255, nullable: true),
                    SAPStatusId = table.Column<int>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    MEXEquipList = table.Column<string>(nullable: true),
                    MEXParentEquip = table.Column<string>(nullable: true),
                    SupFunctLoc = table.Column<string>(nullable: true),
                    SortFieldId = table.Column<int>(nullable: true),
                    PlannerPlantdId = table.Column<int>(nullable: true),
                    ComnpanyCodeId = table.Column<int>(nullable: true),
                    WBSElementId = table.Column<int>(nullable: true),
                    EquipTypeID = table.Column<int>(nullable: true),
                    PbsId = table.Column<int>(nullable: true),
                    EnvZoneId = table.Column<int>(nullable: true),
                    PlannerPlantId = table.Column<int>(nullable: true),
                    CompanyCodeId = table.Column<int>(nullable: true),
                    EngParentID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                    table.ForeignKey(
                        name: "FK_Tag_CommClass",
                        column: x => x.CommClassID,
                        principalTable: "CommClass",
                        principalColumn: "CommClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_CommZone",
                        column: x => x.CommZoneID,
                        principalTable: "CommZone",
                        principalColumn: "CommZoneID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_CommSubSystem",
                        column: x => x.CommissioningSubsystemID,
                        principalTable: "CommSubSystem",
                        principalColumn: "CommSubSystemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_CompanyCode_CompanyCodeId",
                        column: x => x.CompanyCodeId,
                        principalTable: "CompanyCode",
                        principalColumn: "CompanyCodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EngClass",
                        column: x => x.EngClassID,
                        principalTable: "EngClass",
                        principalColumn: "EngClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EngDisc",
                        column: x => x.EngDiscId,
                        principalTable: "EngDisc",
                        principalColumn: "EngDiscID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EngParent",
                        column: x => x.EngParentID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EngStatus",
                        column: x => x.EngStatusID,
                        principalTable: "EngStatus",
                        principalColumn: "EngStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EnvZone_EnvZoneId",
                        column: x => x.EnvZoneId,
                        principalTable: "EnvZone",
                        principalColumn: "EnvZoneID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_EquipmentTypes_EquipTypeID",
                        column: x => x.EquipTypeID,
                        principalTable: "EquipmentTypes",
                        principalColumn: "EquipTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_ExMethod",
                        column: x => x.ExMethodID,
                        principalTable: "ExMethod",
                        principalColumn: "ExMethodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_FlocType_FlocTypeId",
                        column: x => x.FlocTypeId,
                        principalTable: "FlocType",
                        principalColumn: "FlocTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Ipf",
                        column: x => x.IpfID,
                        principalTable: "Ipf",
                        principalColumn: "IpfID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Doc",
                        column: x => x.KeyDocID,
                        principalTable: "Doc",
                        principalColumn: "DocID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintCriticality",
                        column: x => x.MaintCriticalityID,
                        principalTable: "MaintCriticality",
                        principalColumn: "MaintCriticalityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintEdcCode",
                        column: x => x.MaintEdcCodeId,
                        principalTable: "MaintEdcCode",
                        principalColumn: "MaintEdcCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintLocation",
                        column: x => x.MaintLocationID,
                        principalTable: "MaintLocation",
                        principalColumn: "MaintLocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintObjectType",
                        column: x => x.MaintObjectTypeID,
                        principalTable: "MaintObjectType",
                        principalColumn: "MaintObjectTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintParent",
                        column: x => x.MaintParentID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintPlannerGroup",
                        column: x => x.MaintPlannerGroupID,
                        principalTable: "MaintPlannerGroup",
                        principalColumn: "MaintPlannerGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintScePsReviewTeam",
                        column: x => x.MaintScePsReviewTeamID,
                        principalTable: "MaintScePsReviewTeam",
                        principalColumn: "MaintScePsReviewTeamID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintSortProcess",
                        column: x => x.MaintSortProcessID,
                        principalTable: "MaintSortProcess",
                        principalColumn: "MaintSortProcessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintStatus_MaintStatusID",
                        column: x => x.MaintStatusID,
                        principalTable: "MaintStatus",
                        principalColumn: "MaintStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintStructIndicator",
                        column: x => x.MaintStructureIndicatorID,
                        principalTable: "MaintStructureIndicator",
                        principalColumn: "MaintStructureIndicatorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintType",
                        column: x => x.MaintTypeID,
                        principalTable: "MaintType",
                        principalColumn: "MaintTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintWorkCentre",
                        column: x => x.MaintWorkCentreID,
                        principalTable: "MaintWorkCentre",
                        principalColumn: "MaintWorkCentreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintenancePlant_MaintenancePlantId",
                        column: x => x.MaintenancePlantId,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_MaintPlan",
                        column: x => x.MaintenanceplanID,
                        principalTable: "MaintPlan",
                        principalColumn: "MaintPlanID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Manufacturer",
                        column: x => x.ManufacturerID,
                        principalTable: "Manufacturer",
                        principalColumn: "ManufacturerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Model",
                        column: x => x.ModelID,
                        principalTable: "Models",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Pbs_PbsId",
                        column: x => x.PbsId,
                        principalTable: "Pbs",
                        principalColumn: "PbsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_PerformanceStandard",
                        column: x => x.PerformanceStandardID,
                        principalTable: "PerformanceStandard",
                        principalColumn: "PerformanceStandardID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_PlannerPlant_PlannerPlantId",
                        column: x => x.PlannerPlantId,
                        principalTable: "PlannerPlant",
                        principalColumn: "PlannerPlantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_RbiSil",
                        column: x => x.RbiSilID,
                        principalTable: "RbiSil",
                        principalColumn: "RbiSilID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Rbm",
                        column: x => x.RbmID,
                        principalTable: "Rbm",
                        principalColumn: "RbmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Rcm",
                        column: x => x.RcmID,
                        principalTable: "Rcm",
                        principalColumn: "RcmID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_SAPStatus_SAPStatusId",
                        column: x => x.SAPStatusId,
                        principalTable: "SAPStatus",
                        principalColumn: "SAPStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_SortField_SortFieldId",
                        column: x => x.SortFieldId,
                        principalTable: "SortField",
                        principalColumn: "SortFieldId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_SubSystem",
                        column: x => x.SubSystemID,
                        principalTable: "SubSystem",
                        principalColumn: "SubSystemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_Vib",
                        column: x => x.VibID,
                        principalTable: "Vib",
                        principalColumn: "VibID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_WBSElement_WBSElementId",
                        column: x => x.WBSElementId,
                        principalTable: "WBSElement",
                        principalColumn: "WBSElementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskListOperations",
                columns: table => new
                {
                    TaskListOPerationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskListHeaderID = table.Column<int>(nullable: true),
                    OperationNum = table.Column<int>(nullable: false),
                    SubOperationNum = table.Column<int>(nullable: false, defaultValue: 0),
                    OperationID = table.Column<int>(nullable: true),
                    OperationDescription = table.Column<string>(nullable: true),
                    MaintWorkCentreID = table.Column<int>(nullable: true),
                    MaintenancePlantID = table.Column<int>(nullable: true),
                    ControlKeyID = table.Column<int>(nullable: true),
                    SysCondID = table.Column<int>(nullable: true),
                    RelationshiptoOperationID = table.Column<int>(nullable: true),
                    OperationShortText = table.Column<string>(maxLength: 255, nullable: true),
                    OperationLongText = table.Column<string>(maxLength: 255, nullable: true),
                    WorkHrs = table.Column<string>(maxLength: 255, nullable: true),
                    CapNo = table.Column<int>(nullable: true),
                    MaintPackageID = table.Column<int>(nullable: true),
                    DocID = table.Column<int>(nullable: true),
                    TI = table.Column<bool>(nullable: false, defaultValue: false),
                    Offsite = table.Column<bool>(nullable: false, defaultValue: false),
                    FixedOperQty = table.Column<int>(nullable: true),
                    ChangeRequired = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListOperations", x => x.TaskListOPerationID);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_ControlKey",
                        column: x => x.ControlKeyID,
                        principalTable: "ControlKey",
                        principalColumn: "ControlKeyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_Doc",
                        column: x => x.DocID,
                        principalTable: "Doc",
                        principalColumn: "DocID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_MaintPackage",
                        column: x => x.MaintPackageID,
                        principalTable: "MaintPackage",
                        principalColumn: "MaintPackageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_MaintWorkCentre",
                        column: x => x.MaintWorkCentreID,
                        principalTable: "MaintWorkCentre",
                        principalColumn: "MaintWorkCentreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_MaintenancePlant",
                        column: x => x.MaintenancePlantID,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_Operation",
                        column: x => x.OperationID,
                        principalTable: "Operation",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_RelationshipToOperation",
                        column: x => x.RelationshiptoOperationID,
                        principalTable: "RelationshipToOperation",
                        principalColumn: "RelationshipToOperationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_SysCond",
                        column: x => x.SysCondID,
                        principalTable: "SysCond",
                        principalColumn: "SysCondID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListOperations_TaskListHeader_TaskListHeaderID",
                        column: x => x.TaskListHeaderID,
                        principalTable: "TaskListHeader",
                        principalColumn: "TaskListHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskListXScePSReview",
                columns: table => new
                {
                    ScePSReviewID = table.Column<int>(nullable: false),
                    TaskListHeaderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskListXScePSReview", x => new { x.ScePSReviewID, x.TaskListHeaderID });
                    table.ForeignKey(
                        name: "FK_TaskListXScePSReview_ScePSReview",
                        column: x => x.ScePSReviewID,
                        principalTable: "ScePSReview",
                        principalColumn: "ScePSReviewID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskListXScePSReview_TaskListHeader",
                        column: x => x.TaskListHeaderID,
                        principalTable: "TaskListHeader",
                        principalColumn: "TaskListHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlocXMaintLoad",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false),
                    MaintLoadID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlocXMaintLoad", x => new { x.TagID, x.MaintLoadID });
                    table.ForeignKey(
                        name: "FK_FlocXMaintLoad_MaintLoad",
                        column: x => x.MaintLoadID,
                        principalTable: "MaintLoad",
                        principalColumn: "MaintLoadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlocXMaintLoad_Floc",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlocXMaintQuery",
                columns: table => new
                {
                    FlocID = table.Column<int>(nullable: false),
                    MaintQueryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlocXMaintQuery", x => new { x.FlocID, x.MaintQueryID });
                    table.ForeignKey(
                        name: "FK_FlocXMaintQuery_Floc",
                        column: x => x.FlocID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlocXMaintQuery_MaintQuery",
                        column: x => x.MaintQueryID,
                        principalTable: "MaintQuery",
                        principalColumn: "MaintQueryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlocXPMAssembly",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false),
                    PMAssemblyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlocXPMAssembly", x => new { x.TagID, x.PMAssemblyID });
                    table.ForeignKey(
                        name: "FK_FlocXPMAssembly_PMAssembly",
                        column: x => x.PMAssemblyID,
                        principalTable: "PMAssembly",
                        principalColumn: "PMAssemblyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlocXPMAssembly_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintItem",
                columns: table => new
                {
                    MaintItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintPlanID = table.Column<int>(maxLength: 255, nullable: false),
                    MaintItemNum = table.Column<string>(maxLength: 255, nullable: false),
                    MaintItemShortText = table.Column<string>(maxLength: 255, nullable: true),
                    fMaintItemHeaderFloc = table.Column<string>(maxLength: 255, nullable: true),
                    HeaderFlocId = table.Column<int>(nullable: true),
                    MaintItemHeaderEquipID = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemObjectListFloc = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemObjectListEquip = table.Column<string>(maxLength: 255, nullable: true),
                    MaintWorkCentreId = table.Column<int>(nullable: true),
                    MaintItemMainWorkCentre = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemMainWorkCentrePlant = table.Column<string>(maxLength: 255, nullable: true),
                    MaintenancePlantId = table.Column<int>(nullable: true),
                    MaintItemOrderType = table.Column<string>(maxLength: 255, nullable: true),
                    MaintPlannerGroupID = table.Column<int>(maxLength: 255, nullable: true),
                    MaintItemActivityTypeID = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemRevNo = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemUserStatus = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemSystemCondition_Old = table.Column<string>(nullable: true),
                    SysCondId = table.Column<int>(nullable: true),
                    MaintItemConsequenceCategory = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemConsequence = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemLikelihood = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemProposedPriority = table.Column<string>(maxLength: 255, nullable: true),
                    PriorityId = table.Column<int>(nullable: true),
                    MaintItemProposedTi = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemLongText = table.Column<string>(maxLength: 255, nullable: true),
                    MaintItemTasklistExecutionFactor = table.Column<string>(maxLength: 255, nullable: true),
                    TaskListExecutionFactor = table.Column<double>(nullable: true),
                    MaintItemDoNotRelImmed = table.Column<string>(maxLength: 255, nullable: true),
                    bDoNotRelImmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintItem", x => x.MaintItemID);
                    table.ForeignKey(
                        name: "FK_MaintItem_Tag_HeaderFlocId",
                        column: x => x.HeaderFlocId,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintItem_MaintPlan_MaintPlanID",
                        column: x => x.MaintPlanID,
                        principalTable: "MaintPlan",
                        principalColumn: "MaintPlanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintItem_MaintPlannerGroup_MaintPlannerGroupID",
                        column: x => x.MaintPlannerGroupID,
                        principalTable: "MaintPlannerGroup",
                        principalColumn: "MaintPlannerGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintItem_MaintWorkCentre_MaintWorkCentreId",
                        column: x => x.MaintWorkCentreId,
                        principalTable: "MaintWorkCentre",
                        principalColumn: "MaintWorkCentreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintItem_MaintenancePlant_MaintenancePlantId",
                        column: x => x.MaintenancePlantId,
                        principalTable: "MaintenancePlant",
                        principalColumn: "MaintenancePlantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintItem_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "PriorityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintItem_SysCond_SysCondId",
                        column: x => x.SysCondId,
                        principalTable: "SysCond",
                        principalColumn: "SysCondID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagEngData",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false),
                    EngDataCodeID = table.Column<int>(nullable: false),
                    EngDatavalue = table.Column<string>(maxLength: 255, nullable: false),
                    EngDatasource = table.Column<string>(maxLength: 50, nullable: true),
                    EngDataComment = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEngData", x => new { x.TagID, x.EngDataCodeID });
                    table.ForeignKey(
                        name: "FK_tagengdata_engdatacode",
                        column: x => x.EngDataCodeID,
                        principalTable: "EngDataCode",
                        principalColumn: "EngDataCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tagengdata_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagPO",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false),
                    POID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagPO", x => new { x.TagID, x.POID });
                    table.ForeignKey(
                        name: "FK_TagPO_PO",
                        column: x => x.POID,
                        principalTable: "PO",
                        principalColumn: "POID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagPO_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagXDoc",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false),
                    DocID = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    xComment = table.Column<string>(maxLength: 255, nullable: true),
                    PrimaryDoc = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagXDoc", x => new { x.TagID, x.DocID });
                    table.ForeignKey(
                        name: "FK_TagXDoc_Doc",
                        column: x => x.DocID,
                        principalTable: "Doc",
                        principalColumn: "DocID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagXDoc_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlocXMaintItem",
                columns: table => new
                {
                    FlocID = table.Column<int>(nullable: false),
                    MaintItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlocXMaintItem", x => new { x.FlocID, x.MaintItemID });
                    table.ForeignKey(
                        name: "fk_FlocXMaintItem_Tag",
                        column: x => x.FlocID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_FlocXMaintItem_MaintItem",
                        column: x => x.MaintItemID,
                        principalTable: "MaintItem",
                        principalColumn: "MaintItemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintItemXMaintTaskList",
                columns: table => new
                {
                    MaintItemID = table.Column<int>(nullable: false),
                    TaskListHeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintItemXMaintTaskList", x => new { x.MaintItemID, x.TaskListHeaderId });
                    table.ForeignKey(
                        name: "FK_MaintItemXMaintTaskList_MaintItem_MaintItemID",
                        column: x => x.MaintItemID,
                        principalTable: "MaintItem",
                        principalColumn: "MaintItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintItemXMaintTaskList_TaskListHeader_TaskListHeaderId",
                        column: x => x.TaskListHeaderId,
                        principalTable: "TaskListHeader",
                        principalColumn: "TaskListHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "U_Area",
                table: "Area",
                column: "AreaName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Area_MaintenancePlantID",
                table: "Area",
                column: "MaintenancePlantID");

            migrationBuilder.CreateIndex(
                name: "IX_BOM_EquipTypeID",
                table: "BOM",
                column: "EquipTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BOM_ItemCatalogID",
                table: "BOM",
                column: "ItemCatalogID");

            migrationBuilder.CreateIndex(
                name: "U_Class",
                table: "ClassCharacteristics",
                column: "Class",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_CommClass",
                table: "CommClass",
                column: "CommClassName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_CommSubSystem",
                table: "CommSubSystem",
                column: "CommSubSystemNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommSubSystem_SPID",
                table: "CommSubSystem",
                column: "SPID");

            migrationBuilder.CreateIndex(
                name: "IX_CommSubSystem_SystemID",
                table: "CommSubSystem",
                column: "SystemID");

            migrationBuilder.CreateIndex(
                name: "U_CommZone",
                table: "CommZone",
                column: "CommZoneName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommZone_ProjectID",
                table: "CommZone",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "U_DocNum",
                table: "Doc",
                column: "DocNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doc_DocTypeID",
                table: "Doc",
                column: "DocTypeID");

            migrationBuilder.CreateIndex(
                name: "U_DocType",
                table: "DocType",
                column: "DocTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocType_EngDiscId",
                table: "DocType",
                column: "EngDiscId");

            migrationBuilder.CreateIndex(
                name: "U_EngClass",
                table: "EngClass",
                column: "EngClassName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngClass_SuperClassID",
                table: "EngClass",
                column: "SuperClassID");

            migrationBuilder.CreateIndex(
                name: "IX_EngClassRequiredDocs_DocTypeId",
                table: "EngClassRequiredDocs",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs",
                column: "EngClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngDataClassxEngDataCode_BccCodeId",
                table: "EngDataClassxEngDataCode",
                column: "BccCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngDataClassxEngDataCode_EngClassId",
                table: "EngDataClassxEngDataCode",
                column: "EngClassId");

            migrationBuilder.CreateIndex(
                name: "IX_EngDataClassxEngDataCode_EngDataCodeId",
                table: "EngDataClassxEngDataCode",
                column: "EngDataCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngDataCode_EngDataClassId",
                table: "EngDataCode",
                column: "EngDataClassId");

            migrationBuilder.CreateIndex(
                name: "U_EngDataCodeName",
                table: "EngDataCode",
                column: "EngDataCodeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngDataCodeDropDown_EngDataCodeId",
                table: "EngDataCodeDropDown",
                column: "EngDataCodeId");

            migrationBuilder.CreateIndex(
                name: "U_EngDisc",
                table: "EngDisc",
                column: "EngDiscName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngStatus_EngStatusName",
                table: "EngStatus",
                column: "EngStatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityAttributeRequirement_EntityAttributeId",
                table: "EntityAttributeRequirement",
                column: "EntityAttributeId");

            migrationBuilder.CreateIndex(
                name: "U_EnvZone",
                table: "EnvZone",
                column: "EnvZoneName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_ModelID",
                table: "EquipmentTypes",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "U_ExMethod",
                table: "ExMethod",
                column: "ExMethodName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlocXMaintItem_MaintItemID",
                table: "FlocXMaintItem",
                column: "MaintItemID");

            migrationBuilder.CreateIndex(
                name: "IX_FlocXMaintLoad_MaintLoadID",
                table: "FlocXMaintLoad",
                column: "MaintLoadID");

            migrationBuilder.CreateIndex(
                name: "IX_FlocXMaintQuery_MaintQueryID",
                table: "FlocXMaintQuery",
                column: "MaintQueryID");

            migrationBuilder.CreateIndex(
                name: "IX_FlocXPMAssembly_PMAssemblyID",
                table: "FlocXPMAssembly",
                column: "PMAssemblyID");

            migrationBuilder.CreateIndex(
                name: "IX_Import_ImportTypeId",
                table: "Import",
                column: "ImportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportError_ImportExtractId",
                table: "ImportError",
                column: "ImportExtractId",
                unique: true,
                filter: "[ImportExtractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImportError_ImportId",
                table: "ImportError",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportError_ImportTransformId",
                table: "ImportError",
                column: "ImportTransformId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportExtract_ImportId",
                table: "ImportExtract",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportTransform_ImportId",
                table: "ImportTransform",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "U_Ipf",
                table: "Ipf",
                column: "IpfName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngClass_EngClassID",
                table: "KeyListxEngClass",
                column: "EngClassID");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngClass_KeyListId",
                table: "KeyListxEngClass",
                column: "KeyListId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngDataCode_EngDataCodesEngDataCodeId",
                table: "KeyListxEngDataCode",
                column: "EngDataCodesEngDataCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngDataCode_KeyListId",
                table: "KeyListxEngDataCode",
                column: "KeyListId");

            migrationBuilder.CreateIndex(
                name: "U_LoadTemplate",
                table: "LoadTemplate",
                column: "LoadTemplateName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParentLocationID",
                table: "Location",
                column: "ParentLocationID");

            migrationBuilder.CreateIndex(
                name: "U_Location",
                table: "Location",
                columns: new[] { "AreaId", "LocationName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypes_LocationsLocationTypesId",
                table: "LocationTypes",
                column: "LocationsLocationTypesId");

            migrationBuilder.CreateIndex(
                name: "U_MaintArea",
                table: "MaintArea",
                column: "MaintAreaName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintArea_PlantSectionID",
                table: "MaintArea",
                column: "PlantSectionID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintClassXEngDataCode_EngDataCodeId",
                table: "MaintClassXEngDataCode",
                column: "EngDataCodeId");

            migrationBuilder.CreateIndex(
                name: "U_MaintCriticality",
                table: "MaintCriticality",
                column: "MaintCriticalityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintEdcCode",
                table: "MaintEdcCode",
                column: "MaintEdcCodeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_HeaderFlocId",
                table: "MaintItem",
                column: "HeaderFlocId");

            migrationBuilder.CreateIndex(
                name: "U_MaintItemNum",
                table: "MaintItem",
                column: "MaintItemNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_MaintPlanID",
                table: "MaintItem",
                column: "MaintPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_MaintPlannerGroupID",
                table: "MaintItem",
                column: "MaintPlannerGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_MaintWorkCentreId",
                table: "MaintItem",
                column: "MaintWorkCentreId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_MaintenancePlantId",
                table: "MaintItem",
                column: "MaintenancePlantId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_PriorityId",
                table: "MaintItem",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItem_SysCondId",
                table: "MaintItem",
                column: "SysCondId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintItemXMaintTaskList_TaskListHeaderId",
                table: "MaintItemXMaintTaskList",
                column: "TaskListHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintLoad_LoadTemplateID",
                table: "MaintLoad",
                column: "LoadTemplateID");

            migrationBuilder.CreateIndex(
                name: "U_MaintLoadNum",
                table: "MaintLoad",
                column: "MaintLoadNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintLocation_MaintAreaId",
                table: "MaintLocation",
                column: "MaintAreaId");

            migrationBuilder.CreateIndex(
                name: "U_MaintLocation",
                table: "MaintLocation",
                column: "MaintLocationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_maintobjecttype",
                table: "MaintObjectType",
                column: "MaintObjectTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintObjectTypeXMaintClass_MaintClassId",
                table: "MaintObjectTypeXMaintClass",
                column: "MaintClassId");

            migrationBuilder.CreateIndex(
                name: "U_MaintPlan",
                table: "MaintPlan",
                column: "MaintPlanName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintPlan_MaintSortProcessID",
                table: "MaintPlan",
                column: "MaintSortProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintPlan_MaintStrategyID",
                table: "MaintPlan",
                column: "MaintStrategyID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintPlan_MeasPointID",
                table: "MaintPlan",
                column: "MeasPointID");

            migrationBuilder.CreateIndex(
                name: "U_MaintPlannerGroup",
                table: "MaintPlannerGroup",
                column: "MaintPlannerGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintQuery_MaintQueryNoteID",
                table: "MaintQuery",
                column: "MaintQueryNoteID");

            migrationBuilder.CreateIndex(
                name: "U_MaintQueryNum",
                table: "MaintQuery",
                column: "MaintQueryNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintScePsReviewTeam",
                table: "MaintScePsReviewTeam",
                column: "MaintScePsReviewTeamName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintSortProcess",
                table: "MaintSortProcess",
                column: "MaintSortProcessName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintStrategy_DocID",
                table: "MaintStrategy",
                column: "DocID");

            migrationBuilder.CreateIndex(
                name: "U_MaintStrategy",
                table: "MaintStrategy",
                column: "MaintStrategyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintStructureIndicator",
                table: "MaintStructureIndicator",
                column: "MaintStructureIndicatorName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintType",
                table: "MaintType",
                column: "MaintTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_MaintWorkCentre",
                table: "MaintWorkCentre",
                column: "MaintWorkCentreName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Manufacturer",
                table: "Manufacturer",
                column: "ManufacturerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ManufacturerId",
                table: "Models",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "U_Model",
                table: "Models",
                column: "ModelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_PBS",
                table: "Pbs",
                column: "PbsName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_PerformanceStandard",
                table: "PerformanceStandard",
                column: "PerformanceStandardName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_PlantSection",
                table: "PlantSection",
                column: "PlantSectionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_PMAssembly",
                table: "PMAssembly",
                column: "PMAssemblyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_PO",
                table: "PO",
                column: "POName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_MaintenancePlantID",
                table: "Project",
                column: "MaintenancePlantID");

            migrationBuilder.CreateIndex(
                name: "U_ProjectCode",
                table: "Project",
                column: "ProjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_RbiSil",
                table: "RbiSil",
                column: "RbiSilName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Rbm",
                table: "Rbm",
                column: "RbmName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Rcm",
                table: "Rcm",
                column: "RcmName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_RegulatoryBody",
                table: "RegulatoryBody",
                column: "RegulatoryBodyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelationshipToOperation_RelationshipTypeID",
                table: "RelationshipToOperation",
                column: "RelationshipTypeID");

            migrationBuilder.CreateIndex(
                name: "U_ScePsReviewID",
                table: "ScePSReview",
                column: "ScePSReviewName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SP_ProjectID",
                table: "SP",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "U_SPnum",
                table: "SP",
                column: "SPnum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_SubSystem",
                table: "SubSystem",
                column: "SubSystemNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubSystem_SystemID",
                table: "SubSystem",
                column: "SystemID");

            migrationBuilder.CreateIndex(
                name: "U_SuperClass",
                table: "SuperClass",
                column: "SuperclassID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_SysCond",
                table: "SysCond",
                column: "SysCondName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_System",
                table: "System",
                column: "SystemNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CommClassID",
                table: "Tag",
                column: "CommClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CommZoneID",
                table: "Tag",
                column: "CommZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CommissioningSubsystemID",
                table: "Tag",
                column: "CommissioningSubsystemID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CompanyCodeId",
                table: "Tag",
                column: "CompanyCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EngClassID",
                table: "Tag",
                column: "EngClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EngDiscId",
                table: "Tag",
                column: "EngDiscId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EngParentID",
                table: "Tag",
                column: "EngParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EngStatusID",
                table: "Tag",
                column: "EngStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EnvZoneId",
                table: "Tag",
                column: "EnvZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EquipTypeID",
                table: "Tag",
                column: "EquipTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ExMethodID",
                table: "Tag",
                column: "ExMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_FlocTypeId",
                table: "Tag",
                column: "FlocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_IpfID",
                table: "Tag",
                column: "IpfID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_KeyDocID",
                table: "Tag",
                column: "KeyDocID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_LocationID",
                table: "Tag",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintCriticalityID",
                table: "Tag",
                column: "MaintCriticalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintEdcCodeId",
                table: "Tag",
                column: "MaintEdcCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintLocationID",
                table: "Tag",
                column: "MaintLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintObjectTypeID",
                table: "Tag",
                column: "MaintObjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintParentID",
                table: "Tag",
                column: "MaintParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintPlannerGroupID",
                table: "Tag",
                column: "MaintPlannerGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintScePsReviewTeamID",
                table: "Tag",
                column: "MaintScePsReviewTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintSortProcessID",
                table: "Tag",
                column: "MaintSortProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintStatusID",
                table: "Tag",
                column: "MaintStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintStructureIndicatorID",
                table: "Tag",
                column: "MaintStructureIndicatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintTypeID",
                table: "Tag",
                column: "MaintTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintWorkCentreID",
                table: "Tag",
                column: "MaintWorkCentreID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintenancePlantId",
                table: "Tag",
                column: "MaintenancePlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_MaintenanceplanID",
                table: "Tag",
                column: "MaintenanceplanID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ManufacturerID",
                table: "Tag",
                column: "ManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ModelID",
                table: "Tag",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PbsId",
                table: "Tag",
                column: "PbsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PerformanceStandardID",
                table: "Tag",
                column: "PerformanceStandardID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PlannerPlantId",
                table: "Tag",
                column: "PlannerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_RbiSilID",
                table: "Tag",
                column: "RbiSilID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_RbmID",
                table: "Tag",
                column: "RbmID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_RcmID",
                table: "Tag",
                column: "RcmID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_SAPStatusId",
                table: "Tag",
                column: "SAPStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_SortFieldId",
                table: "Tag",
                column: "SortFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_SubSystemID",
                table: "Tag",
                column: "SubSystemID");

            migrationBuilder.CreateIndex(
                name: "U_NoNull_TagFLOC",
                table: "Tag",
                column: "TagFLOC",
                unique: true,
                filter: "([TagFLOC] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Tag",
                table: "Tag",
                column: "TagNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_VibID",
                table: "Tag",
                column: "VibID");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_WBSElementId",
                table: "Tag",
                column: "WBSElementId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEngData_EngDataCodeID",
                table: "TagEngData",
                column: "EngDataCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_TagPO_POID",
                table: "TagPO",
                column: "POID");

            migrationBuilder.CreateIndex(
                name: "IX_TagViewColumns_TagViewId",
                table: "TagViewColumns",
                column: "TagViewId");

            migrationBuilder.CreateIndex(
                name: "IX_TagViewColumnsUser_TagViewColumnsId",
                table: "TagViewColumnsUser",
                column: "TagViewColumnsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagXDoc_DocID",
                table: "TagXDoc",
                column: "DocID");

            migrationBuilder.CreateIndex(
                name: "U_TaskListCat",
                table: "TaskListCat",
                column: "TaskListCatName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_TaskListGroup",
                table: "TaskListGroup",
                column: "TaskListGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_MaintPackageID",
                table: "TaskListHeader",
                column: "MaintPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_MaintPlannerGroupId",
                table: "TaskListHeader",
                column: "MaintPlannerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_MaintStrategyID",
                table: "TaskListHeader",
                column: "MaintStrategyID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_MaintWorkCentreID",
                table: "TaskListHeader",
                column: "MaintWorkCentreID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_MaintenancePlantID",
                table: "TaskListHeader",
                column: "MaintenancePlantID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_PerformanceStandardID",
                table: "TaskListHeader",
                column: "PerformanceStandardID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_PMAssemblyID",
                table: "TaskListHeader",
                column: "PMAssemblyID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_RegulatoryBodyID",
                table: "TaskListHeader",
                column: "RegulatoryBodyID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_StatusId",
                table: "TaskListHeader",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_SysCondID",
                table: "TaskListHeader",
                column: "SysCondID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_TaskListGroupID",
                table: "TaskListHeader",
                column: "TaskListGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListHeader_TasklistCatID",
                table: "TaskListHeader",
                column: "TasklistCatID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_ControlKeyID",
                table: "TaskListOperations",
                column: "ControlKeyID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_DocID",
                table: "TaskListOperations",
                column: "DocID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_MaintPackageID",
                table: "TaskListOperations",
                column: "MaintPackageID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_MaintWorkCentreID",
                table: "TaskListOperations",
                column: "MaintWorkCentreID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_MaintenancePlantID",
                table: "TaskListOperations",
                column: "MaintenancePlantID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_OperationID",
                table: "TaskListOperations",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_RelationshiptoOperationID",
                table: "TaskListOperations",
                column: "RelationshiptoOperationID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_SysCondID",
                table: "TaskListOperations",
                column: "SysCondID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListOperations_TaskListHeaderID",
                table: "TaskListOperations",
                column: "TaskListHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskListXScePSReview_TaskListHeaderID",
                table: "TaskListXScePSReview",
                column: "TaskListHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_UC2ViewColumnsUser_UC2ViewColumnsId",
                table: "UC2ViewColumnsUser",
                column: "UC2ViewColumnsId");

            migrationBuilder.CreateIndex(
                name: "U_Vib",
                table: "Vib",
                column: "VibName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOM");

            migrationBuilder.DropTable(
                name: "ClassCharacteristics");

            migrationBuilder.DropTable(
                name: "ClassCharsTaskListHeader");

            migrationBuilder.DropTable(
                name: "ColumnSets");

            migrationBuilder.DropTable(
                name: "EngClassRequiredDocs");

            migrationBuilder.DropTable(
                name: "EngDataClassxEngDataCode");

            migrationBuilder.DropTable(
                name: "EngDataCodeDropDown");

            migrationBuilder.DropTable(
                name: "EntityAttributeRequirement");

            migrationBuilder.DropTable(
                name: "FlocXMaintItem");

            migrationBuilder.DropTable(
                name: "FlocXMaintLoad");

            migrationBuilder.DropTable(
                name: "FlocXMaintQuery");

            migrationBuilder.DropTable(
                name: "FlocXPMAssembly");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "ImportAttributeType");

            migrationBuilder.DropTable(
                name: "ImportError");

            migrationBuilder.DropTable(
                name: "KeyListxEngClass");

            migrationBuilder.DropTable(
                name: "KeyListxEngDataCode");

            migrationBuilder.DropTable(
                name: "LocationTypes");

            migrationBuilder.DropTable(
                name: "MaintClassXEngDataCode");

            migrationBuilder.DropTable(
                name: "MaintItemXMaintTaskList");

            migrationBuilder.DropTable(
                name: "MaintObjectTypeXMaintClass");

            migrationBuilder.DropTable(
                name: "SAPExportDetail");

            migrationBuilder.DropTable(
                name: "SchedulingPeriodUOM");

            migrationBuilder.DropTable(
                name: "TagEngData");

            migrationBuilder.DropTable(
                name: "TagPO");

            migrationBuilder.DropTable(
                name: "TagViewColumnsUser");

            migrationBuilder.DropTable(
                name: "TagXDoc");

            migrationBuilder.DropTable(
                name: "TaskListOperations");

            migrationBuilder.DropTable(
                name: "TaskListXScePSReview");

            migrationBuilder.DropTable(
                name: "UC2ViewColumnsUser");

            migrationBuilder.DropTable(
                name: "ItemCatalog");

            migrationBuilder.DropTable(
                name: "BccCode");

            migrationBuilder.DropTable(
                name: "EntityAttribute");

            migrationBuilder.DropTable(
                name: "MaintLoad");

            migrationBuilder.DropTable(
                name: "MaintQuery");

            migrationBuilder.DropTable(
                name: "ImportExtract");

            migrationBuilder.DropTable(
                name: "ImportTransform");

            migrationBuilder.DropTable(
                name: "KeyList");

            migrationBuilder.DropTable(
                name: "MaintItem");

            migrationBuilder.DropTable(
                name: "MaintClass");

            migrationBuilder.DropTable(
                name: "EngDataCode");

            migrationBuilder.DropTable(
                name: "PO");

            migrationBuilder.DropTable(
                name: "TagViewColumns");

            migrationBuilder.DropTable(
                name: "ControlKey");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "RelationshipToOperation");

            migrationBuilder.DropTable(
                name: "ScePSReview");

            migrationBuilder.DropTable(
                name: "TaskListHeader");

            migrationBuilder.DropTable(
                name: "UC2ViewColumns");

            migrationBuilder.DropTable(
                name: "LoadTemplate");

            migrationBuilder.DropTable(
                name: "MaintQueryNote");

            migrationBuilder.DropTable(
                name: "Import");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "EngDataClass");

            migrationBuilder.DropTable(
                name: "TagView");

            migrationBuilder.DropTable(
                name: "RelationshipType");

            migrationBuilder.DropTable(
                name: "MaintPackage");

            migrationBuilder.DropTable(
                name: "PMAssembly");

            migrationBuilder.DropTable(
                name: "RegulatoryBody");

            migrationBuilder.DropTable(
                name: "SysCond");

            migrationBuilder.DropTable(
                name: "TaskListGroup");

            migrationBuilder.DropTable(
                name: "TaskListCat");

            migrationBuilder.DropTable(
                name: "ImportType");

            migrationBuilder.DropTable(
                name: "CommClass");

            migrationBuilder.DropTable(
                name: "CommZone");

            migrationBuilder.DropTable(
                name: "CommSubSystem");

            migrationBuilder.DropTable(
                name: "CompanyCode");

            migrationBuilder.DropTable(
                name: "EngClass");

            migrationBuilder.DropTable(
                name: "EngStatus");

            migrationBuilder.DropTable(
                name: "EnvZone");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "ExMethod");

            migrationBuilder.DropTable(
                name: "FlocType");

            migrationBuilder.DropTable(
                name: "Ipf");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "MaintCriticality");

            migrationBuilder.DropTable(
                name: "MaintEdcCode");

            migrationBuilder.DropTable(
                name: "MaintLocation");

            migrationBuilder.DropTable(
                name: "MaintObjectType");

            migrationBuilder.DropTable(
                name: "MaintPlannerGroup");

            migrationBuilder.DropTable(
                name: "MaintScePsReviewTeam");

            migrationBuilder.DropTable(
                name: "MaintStatus");

            migrationBuilder.DropTable(
                name: "MaintStructureIndicator");

            migrationBuilder.DropTable(
                name: "MaintType");

            migrationBuilder.DropTable(
                name: "MaintWorkCentre");

            migrationBuilder.DropTable(
                name: "MaintPlan");

            migrationBuilder.DropTable(
                name: "Pbs");

            migrationBuilder.DropTable(
                name: "PerformanceStandard");

            migrationBuilder.DropTable(
                name: "PlannerPlant");

            migrationBuilder.DropTable(
                name: "RbiSil");

            migrationBuilder.DropTable(
                name: "Rbm");

            migrationBuilder.DropTable(
                name: "Rcm");

            migrationBuilder.DropTable(
                name: "SAPStatus");

            migrationBuilder.DropTable(
                name: "SortField");

            migrationBuilder.DropTable(
                name: "SubSystem");

            migrationBuilder.DropTable(
                name: "Vib");

            migrationBuilder.DropTable(
                name: "WBSElement");

            migrationBuilder.DropTable(
                name: "SP");

            migrationBuilder.DropTable(
                name: "SuperClass");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "MaintArea");

            migrationBuilder.DropTable(
                name: "MaintSortProcess");

            migrationBuilder.DropTable(
                name: "MaintStrategy");

            migrationBuilder.DropTable(
                name: "MeasPoint");

            migrationBuilder.DropTable(
                name: "System");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "PlantSection");

            migrationBuilder.DropTable(
                name: "Doc");

            migrationBuilder.DropTable(
                name: "MaintenancePlant");

            migrationBuilder.DropTable(
                name: "DocType");

            migrationBuilder.DropTable(
                name: "EngDisc");
        }
    }
}
