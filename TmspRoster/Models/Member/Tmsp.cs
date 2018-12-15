using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TmspRoster.Models.Member
{
    public class Tmsp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "TMSP #")]
        public int TmspID { get; set; }

        public string Link { get; set; }
    }
}
