using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SjonnieLoper.Core
{
    public class EmployeeRegisterCode
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(4)]
        public string CodeP1 { get; set; }
        [Required, StringLength(4)]
        public string CodeP2 { get; set; }
        [Required, StringLength(4)]
        public string CodeP3 { get; set; }
    }
}
