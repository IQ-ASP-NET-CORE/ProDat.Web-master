using ProDat.Web2.Models;
using System.Linq;
namespace ProDat.Web2.Data
{
    public class DBInitializer
    {
        public static void Initialize(TagContext context)
        {
            // create Database if not exist.
            context.Database.EnsureCreated();


            // Create Data if not exist.
            if (context.Tag.Any())
            {
                return; //already populated
            }

            //engdisc
            var engDisc = new EngDisc[]
            {
                new EngDisc{EngDiscName="E", EngDiscDesc="Electrical" },
                new EngDisc{EngDiscName="M", EngDiscDesc="Mechanical" },
                new EngDisc{EngDiscName="C", EngDiscDesc="Civil" }
            };
            foreach (EngDisc x in engDisc)
            {
                context.EngDisc.Add(x);
            }
            context.SaveChanges();
            
            //PlantSection
            var plantSection = new PlantSection[]
            {
                new PlantSection{ PlantSectionName="T1", PlantSectionDesc="Train 1" },
                new PlantSection{ PlantSectionName="T2", PlantSectionDesc="Train 2" }
            };
            foreach (PlantSection x in plantSection)
            {
                context.PlantSection.Add(x);
            }
            context.SaveChanges();
            
            //maintarea
            var maintArea = new MaintArea[]
            {
                new MaintArea{MaintAreaName="SE", MaintAreaDesc="Site Engineering", PlantSectionId=plantSection.Single(i => i.PlantSectionName == "T1").PlantSectionId },
                new MaintArea{MaintAreaName="PM", MaintAreaDesc="Project Mainenance", PlantSectionId=plantSection.Single(i => i.PlantSectionName == "T1").PlantSectionId },
                new MaintArea{MaintAreaName="FO", MaintAreaDesc="Facilities Office", PlantSectionId=plantSection.Single(i => i.PlantSectionName == "T2").PlantSectionId }
            };
            foreach (MaintArea x in maintArea)
            {
                context.MaintArea.Add(x);
            }
            context.SaveChanges();
            
            //maintcriticality
            var maintCriticality = new MaintCriticality[]
            {
                new MaintCriticality{MaintCriticalityName="SE", MaintCriticalityDesc="Site Engineering" },
                new MaintCriticality{MaintCriticalityName="SE", MaintCriticalityDesc="Site Engineering" },
                new MaintCriticality{MaintCriticalityName="SE", MaintCriticalityDesc="Site Engineering" }
            };
            foreach (MaintCriticality x in maintCriticality)
            {
                context.MaintCriticality.Add(x);
            }
            context.SaveChanges();

            //maintLocation
            var maintLocation = new MaintLocation[]
           {
                new MaintLocation{MaintLocationName="01", MaintLocationDesc="Deck 1", MaintAreaId=maintArea.Single(i => i.MaintAreaName == "SE").MaintAreaId},
                new MaintLocation{MaintLocationName="02", MaintLocationDesc="Deck 2", MaintAreaId=maintArea.Single(i => i.MaintAreaName == "SE").MaintAreaId},
                new MaintLocation{MaintLocationName="03", MaintLocationDesc="Port A", MaintAreaId=maintArea.Single(i => i.MaintAreaName == "PM").MaintAreaId},
                new MaintLocation{MaintLocationName="04", MaintLocationDesc="Office", MaintAreaId=maintArea.Single(i => i.MaintAreaName == "FO").MaintAreaId}
           };
            foreach (MaintLocation x in maintLocation)
            {
                context.MaintLocation.Add(x);
            }
            context.SaveChanges();

            //maintObjectType
            var maintObjectType = new MaintObjectType[]
           {
                new MaintObjectType{MaintObjectTypeName="widgetA", MaintObjectTypeDesc="A maintenance type A", MaintObjectTypeDescExt="another type of maintenance", StdNounModifier="EE"},
                new MaintObjectType{MaintObjectTypeName="widgetC", MaintObjectTypeDesc="A maintenance type C", MaintObjectTypeDescExt="WidgetC", StdNounModifier="EC"},
                new MaintObjectType{MaintObjectTypeName="asdfa", MaintObjectTypeDesc="asdfasd", MaintObjectTypeDescExt="WidgetGG", StdNounModifier="GEC"}
           };

            foreach (MaintObjectType x in maintObjectType)
            {
                context.MaintObjectType.Add(x);
            }
            context.SaveChanges();

            //System
            var system = new Models.Systems[]
           {
                new Models.Systems{SystemNum="A01"},
                new Models.Systems{SystemNum="A02"},
                new Models.Systems{SystemNum="B01"}
           };
            foreach (Models.Systems x in system)
            {
                context.System.Add(x);
            }
            context.SaveChanges();

            //SubSystem
            var subSystem = new Models.SubSystem[]
           {
                new SubSystem{SubSystemNum="A01.01", SystemsId=system.Single(i => i.SystemNum == "A01").SystemsId},
                new SubSystem{SubSystemNum="A01.02", SystemsId=system.Single(i => i.SystemNum == "A01").SystemsId},
                new SubSystem{SubSystemNum="A01.99", SystemsId=system.Single(i => i.SystemNum == "A01").SystemsId},
                new SubSystem{SubSystemNum="A02.01", SystemsId=system.Single(i => i.SystemNum == "A02").SystemsId},
                new SubSystem{SubSystemNum="A02.02", SystemsId=system.Single(i => i.SystemNum == "A02").SystemsId},
                new SubSystem{SubSystemNum="A02.99", SystemsId=system.Single(i => i.SystemNum == "A02").SystemsId},
                new SubSystem{SubSystemNum="B01.01", SystemsId=system.Single(i => i.SystemNum == "B01").SystemsId},
                new SubSystem{SubSystemNum="B01.99", SystemsId=system.Single(i => i.SystemNum == "B01").SystemsId}
           };
            foreach (SubSystem x in subSystem)
            {
                context.SubSystem.Add(x);
            }
            context.SaveChanges();


            //Tags
            var tags = new Tag[]
          {
                new Tag{TagNumber="1ABC-123", TagFloc="1F-ABC-123", EngDiscId=engDisc.Single(i => i.EngDiscName == "E").EngDiscId},
                new Tag{TagNumber="1ABC-124", TagFloc="1F-ABC-124", EngDiscId=engDisc.Single(i => i.EngDiscName == "E").EngDiscId}
          };
            foreach (Tag x in tags)
            {
                context.Tag.Add(x);
            }
            context.SaveChanges();
        }
    }
}