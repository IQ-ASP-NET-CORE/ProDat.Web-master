using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Data;
using ProDat.Web2.Models;

namespace ProDat.Web2.ViewModels
{
    public class TaskListHeaderFormUC3
    {
        public int height { get; set; }
        public int width { get; set; }
        public int? parent { get; set; }

        public virtual TaskListHeader TaskListHeader { get; set; }

    }
}
