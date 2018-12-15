using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TmspRoster.Models.Enums;

namespace TmspRoster.Models.Member
{
    public class Member
    {
        public int MemberID { get; set; }
               
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        public string City { get; set; }

        public EnumLocation Territory { get; set; }

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? JoinDate { get; set; }

        [Display(Name = "Status / Remarks")]
        public EnumStatus Status { get; set; }

        public Tmsp Tmsp { get; set; }

        [Display(Name = "Plate No. / Conduction No.")]
        public string PlateNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => FirstMidName + " " + LastName;

        public string Location => City + ", " + Territory;

    }
}