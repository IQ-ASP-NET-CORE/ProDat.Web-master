using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintScePsReviewTeam
    {
        public MaintScePsReviewTeam()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintScePsReviewTeamId { get; set; }
        public string MaintScePsReviewTeamName { get; set; }
        public string MaintScePsReviewTeamDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
