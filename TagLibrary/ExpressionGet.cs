using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.TagLibrary
{
    // KEEP THIS
    // Simplifies use of reflection with Models. 
    
    public class ExpressionGet
    {

        public PropertyInfo getChildProperty(String name, Object obj)
        {
            PropertyInfo info=null;

            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                info = type.GetProperty(part);
                
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return info;
        }

        public object getChildPropertyValue(String name, Object obj)
        {

            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                var info = type.GetProperty(part);

                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public string GetPropertyValue(Type type, IEnumerable<Object> x)
        {

            return null;
        }

    }
}
