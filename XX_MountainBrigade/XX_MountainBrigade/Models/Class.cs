using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XX_MountainBrigade.Models
{
    public class Company
    {
        [Key]
        public int CoyId { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters.")]
        public string CoyName { get; set; }

        // Navigation Property: One Company has many Personnel
        public ICollection<Personnel> Personnel { get; set; }

        // Navigation Property: One Company has many Regiments
        public ICollection<Regiment> Regiments { get; set; }  // Added this line
    }


    public class Rank
    {
        [Key]
        public int RankId { get; set; }

        [Required(ErrorMessage = "Rank Name is required.")]
        [StringLength(50, ErrorMessage = "Rank Name cannot exceed 50 characters.")]
        public string RankName { get; set; }

        // Navigation Property: One Rank can be assigned to many Personnel
        public ICollection<Personnel> Personnel { get; set; }
    }

    public class Personnel
    {
        [Key]
        public int PersId { get; set; }

        [Required(ErrorMessage = "Personnel Number is required.")]
        [StringLength(10, ErrorMessage = "Personnel Number cannot exceed 10 characters.")]
        public string PersNo { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Permanent Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Type of Personnel is required.")]
        [RegularExpression("^(JCO|OR)$", ErrorMessage = "Type of Personnel must be either 'JCO' or 'OR'.")]
        public string TypeOfPersonnel { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        public int CoyId { get; set; } // Foreign Key for Company

        [Required(ErrorMessage = "Rank is required.")]
        public int RankId { get; set; } // Foreign Key for Rank

        public int? RegimentId { get; set; }  // Foreign Key for Regiment (nullable)

        // Navigation Properties
        [ForeignKey("CoyId")]
        public Company Company { get; set; }

        [ForeignKey("RankId")]
        public Rank Rank { get; set; }

        [ForeignKey("RegimentId")]
        public Regiment Regiment { get; set; } // Nullable, since Personnel may not belong to a Regiment
    }


    public class LoginModel
    {
        [Required(ErrorMessage = "Personnel Number (Username) is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Personnel Number must be between 3 and 50 characters.")]
        [Display(Name = "Personnel Number (Username)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }

    public class Regiment
    {
        [Key]
        public int RegId { get; set; }

        [Required(ErrorMessage = "Regiment Name is required.")]
        [StringLength(100, ErrorMessage = "Regiment Name cannot exceed 100 characters.")]
        public string RegimentName { get; set; }  // Add this property

        // Foreign Key to Company
        [Required(ErrorMessage = "Company is required.")]
        public int CoyId { get; set; }

        // Navigation Property: One Regiment belongs to one Company
        [ForeignKey("CoyId")]
        public Company Company { get; set; }

        // Navigation Property: One Regiment has many Personnel
        public ICollection<Personnel> Personnel { get; set; }
    }



}
