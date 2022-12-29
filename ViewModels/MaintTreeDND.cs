using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class MaintTreeDND
    {
        ICollection<TreeNode> MaintTree { get; set; }

        ICollection<TreeNode> EngTree { get; set; }
    }
}
