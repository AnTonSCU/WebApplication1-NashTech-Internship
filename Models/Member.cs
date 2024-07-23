using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class Member
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        public string PlaceOfBirth { get; set; }

        public bool IsGraduated { get; set; }
    }
}
