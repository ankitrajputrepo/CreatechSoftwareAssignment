using System.ComponentModel.DataAnnotations;

namespace XXMountainBrigade.Models
{
    public class Company
    {

       
        public int CoyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CoyName { get; set; }
        public ICollection<Personnel> Personnel { get; set; }
    }

    public class Rank
    {

        public int RankId { get; set; }

        [Required]
        [StringLength(100)]
        public string RanName { get; set; }
        public ICollection<Personnel> Personnel { get; set; }
    }


    public class Personnel
    {
        public int PersId { get; set; }

        public int CoyId { get; set; }  // Foreign key to Company
        public int RankId { get; set; } // Foreign key to Rank

        [Required]
        [StringLength(50)] // Limiting length of PersNo
        public string PersNo { get; set; }

        [Required]
        [StringLength(100)]  // Limiting length of FirstName
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]  // Limiting length of LastName
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(250)]  // Limiting length of PermanentAddress
        public string PermanentAddress { get; set; }

        // Navigation properties
        public Company Company { get; set; }
        public Rank Rank { get; set; }
    }


    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]  // Password field type
        public string Password { get; set; }
    }


}
