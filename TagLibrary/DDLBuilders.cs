using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.TagLibrary
{
    public class DDLBuilders
    {
        public string CustomDDls(SqlConnection conn, string fieldName, bool isAdmin)
        {
            // unusual behaviour: 
            // SubSystemId: *Num
            List<string> numtypes = new List<string>() { "SubSystem", "asdf" };

            // MaintParentId: Tag with Join to get parent Id
            // EngParentId: Tag with Join to get parent Id

            List<customSelectItem> ddlList = new List<customSelectItem>();
            // add blank line to null a value.
            ddlList.Add(new customSelectItem(-1, "-", "-"));

            // All Star Tables are identified by EndsWith 'Id'.
            string entity = fieldName.Substring(0, fieldName.Length - 2);
            string sql = "";
            if (numtypes.Contains(entity)) 
            { 
                sql = "SELECT " + entity + "Id, " + entity + "Num, " + entity + "Num + ' - ' + " + entity + "Name FROM " + entity + " ORDER BY 2";
            }
            else if (entity == "MaintParent") 
            { 
                sql = "SELECT DISTINCT b.TagId, b.TagNumber, b.TagNumber FROM Tag a INNER JOIN Tag b ON a.TagId = b.MaintParentId ORDER BY 2";
            }
            else if (entity == "EngParent")
            {
                sql = "SELECT DISTINCT b.TagId, b.TagNumber, b.TagNumber FROM Tag a INNER JOIN Tag b ON a.TagId = b.EngParentId ORDER BY 2";
            }
            else if (entity == "MaintObjectType")
            {
                sql = "SELECT MaintObjectTypeID, MaintObjectTypeName, MaintObjectTypeName +' - ' + MaintObjectTypeDesc + ' - ' + MaintObjectTypeDescExt as ddl FROM MaintObjectType ORDER BY 2";
            }
            else
            {
                sql = "SELECT " + entity + "Id, " + entity + "Name, " + entity + "Name + ' - ' + " + entity + "Desc FROM " + entity + " ORDER BY 2";
            }
   

            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            var val1 = reader.GetInt32(0);
                            var val2 = reader.GetString(1);
                            var val3 = reader.GetString(2);
                            ddlList.Add(new customSelectItem(val1, val2, val3));
                        } 
                        catch(Exception ex)
                        {
                            Console.WriteLine("error with " + entity + ex);
                        }
                    }
                }
            }
            
            if (isAdmin)
                ddlList.Add(new customSelectItem(-2, "--Add New Item", "--Add New Item"));

            return JsonConvert.SerializeObject(ddlList);
        }

        public List<string> DicHelperBuilder(SqlConnection conn, string fieldName)
        {
            List<string> dataList = new List<string>();
            dataList.Add("");

            // Build SQL Statement
            List<string> numtypes = new List<string>() { "SubSystem", "asdf" };
            string entity = fieldName.Substring(0, fieldName.Length - 2);
            string sql = "";
            if (numtypes.Contains(entity))
            {
                sql = "SELECT " + entity + "Num FROM " + entity + " ORDER BY 1";
            }
            else if (entity == "MaintParent")
            {
                sql = "SELECT b.TagNumber FROM Tag a INNER JOIN Tag b ON a.TagId = b.MaintParentId ORDER BY 1";
            }
            else if (entity == "EngParent")
            {
                sql = "SELECT b.TagNumber FROM Tag a INNER JOIN Tag b ON a.TagId = b.EngParentId ORDER BY 1";
            }
            else if (entity == "MaintObjectType")
            {
                sql = "SELECT MaintObjectTypeName FROM MaintObjectType ORDER BY 1";
            }
            else
            {
                sql = "SELECT " + entity + "Name FROM " + entity + " ORDER BY 1";
            }

            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            dataList.Add(reader.GetString(0));
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("error with " + entity + ex);
                        }
                    }
                }
            }

            return dataList;
        }

    }
}
