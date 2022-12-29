using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagViewColumnsUser
    {
        public int TagViewColumnsUserId { get; set; }

        public int TagViewColumnsId {get; set;}

        public string UserName { get; set; }

        public int ColumnWidth { get; set; }

    }
}
