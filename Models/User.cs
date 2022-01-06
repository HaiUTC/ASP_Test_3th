using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNet.Models{
    public class User{
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set;}
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set;}
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set;}
        [Required]
        public string Gender { get; set;}
        
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set;}
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set;}

        public DateTime CreatedAt { get; set;}
    }
}