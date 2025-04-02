﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("TargetUser")]
        public int TargetUserID { get; set; }

        [Required(ErrorMessage = "Please enter a rating.")]
        public int Rating { get; set; }
        public required string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
