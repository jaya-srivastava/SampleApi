using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPractice.Helpers;
using Sample.Models.DTO;

namespace iPractice.ViewModel
{    
    //used for regitering for all users
    public class RegisterSchoolModel
    {
        [Remote("IsUserNameExists", "Membership", ErrorMessage = "User Name Already Exists")]
        [Required(ErrorMessage = "User name is Required")]
        [RegularExpression(@"^\d*[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "{0} should contain only alphanumeric characters.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 15 characters long.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public int UserId { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 200 characters long.")]
        //[Required(ErrorMessage = "School Name is Required")]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "School Address")]
        public string SchoolAddress { get; set; }

        public string RoleType { get; set; }

        public string ErrorMessage { get; set; }

        //[Remote("IsSuffixExists", "Membership", ErrorMessage = "Suffix already assigned")]
        //[Required(ErrorMessage = "Suffix is Required")]
        //[StringLength(10, MinimumLength = 4, ErrorMessage = "{0} must be between 4 to 10 characters long.")]
        //[RegularExpression(@"^\d*[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "{0} should only contains characters or characters and numbers.")]
        //[Display(Name = "Suffix")]
        public string Suffix { get; set; }

        //don't add validation as these are not passed while registration
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")] 
        //[Required(ErrorMessage = "First Name is Required")]
        //[Display(Name = "First Name")]
        
        public string FirstName { get; set; }
        //don't add validation as these are not passed while registration
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between 2 to 50 characters long.")]
        //[Required(ErrorMessage = "Last Name is Required")]
        //[Display(Name = "Last Name")]        
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Phone is Required")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "{0} must be between 10 to 15 characters long.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid 10-digit phone number with or without hyphen")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "{0} must be between 6 to 15 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Remote("IsEmailExists", "Membership", ErrorMessage = "Email Address Already Exists")]
        [Required(ErrorMessage = "Email address is Required")]
        //if you made any changes in regex here also make change in EmailAttribute class in emailExtension.cs in helper file
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.+-]+\.(?:[a-zA-Z]{2}|COM|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu|academy|biz|college|education|int)$", ErrorMessage = "Invalid Email Address")]
        [Email]  //emailAttribue fro emailExtensions in helper file /// doesn't work on client side 
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Email and confirmation email do not match.")]
        [Display(Name = "Confirm Email address")]
        public string ConfirmEmail { get; set; }

        public bool TermsOfMemeberShip { get; set; }

        public string ReferredBy { get; set; }

        public string ReferredByOther { get; set; }
    }


    //used to register child account only.
    public class RegisterStudentModel
    {
        [Remote("IsStudentUserNameExists", "Membership", ErrorMessage = "User Name Already Exists")]
        [Required(ErrorMessage = "User name is Required")]
        [RegularExpression(@"^\d*[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "{0} should only contains characters or characters and numbers.")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 15 characters long.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }


        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [RegularExpression(@"[a-zA-z]*$", ErrorMessage = "{0} should contain only letters.")]
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between 2 to 50 characters long.")]
        [RegularExpression(@"[a-zA-z'-]*$", ErrorMessage = "{0} should contain only letters, hyphens, and apostrophes.")]
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "{0} must be between 6 to 15 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Remote("IsEmailExists", "Membership", ErrorMessage = "Email ID already exists.")]
        [Required(ErrorMessage = "Email address is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.+-]+\.(?:[a-zA-Z]{2}|COM|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu|academy|biz|college|education|int)$", ErrorMessage = "Invalid Email Address")]
        [Email]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Range(-5, 100, ErrorMessage = "* Please select grade")]
        public int? GradeId { get; set; }

        public bool TermsOfMemeberShip { get; set; }

        [Required(ErrorMessage = "Security Code is Required")]
        public string SecurityCode { get; set; }

        public SelectList Grades { get; set; }
    }

    public class TeacherProfileVM
    {
        public int UserId { get; set; }

        public string EmailId { get; set; }

        public string UserName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        [RegularExpression(@"[a-zA-Z]*$", ErrorMessage = "{0} should contain only letters.")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between 2 to 50 characters long.")]
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"[\sa-zA-z'-]*$", ErrorMessage = "{0} should contain only letters, hyphens, and apostrophes.")]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "{0} must be between 10 to 15 characters long.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid 10-digit phone number with or without hyphen")]
        public string Phone { get; set; }

        public DateTime? DOB { get; set; }


        [Required(ErrorMessage = "* Gender is required")]
        public string Gender { get; set; } // M or F    

        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 200 characters long.")]
        [Required(ErrorMessage = "School Name is Required")]
        [RegularExpression(@"[\sa-zA-Z]*$", ErrorMessage = "{0} should contain only letters.")]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 500 characters long.")]
        [Display(Name = "School Address")]
        public string SchoolAddress { get; set; }

        //[StringLength(500, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 500 characters long.")]
        //[Required(ErrorMessage = "Class Description is Required")]
        //[Display(Name = "Class Description")]
        public string ClassDesc { get; set; }

        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [RegularExpression(@"[\sa-zA-z]*$", ErrorMessage = "{0} should contain only letters.")]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [RegularExpression(@"[\sa-zA-z]*$", ErrorMessage = "{0} should contain only letters.")]
        public string State { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [RegularExpression(@"[\sa-zA-z]*$", ErrorMessage = "{0} should contain only letters.")]
        [Required(ErrorMessage = "* Country is required")]
        public string Country { get; set; }

        [RegularExpression(@"[0-9]*$", ErrorMessage = "{0} should contain only numbers.")]
        [Required(ErrorMessage = "* Zipcode is required")]
        public string Zipcode { get; set; }

        public string AddressType { get; set; }

        public bool? IsAdminActive { get; set; }

        public string SecurityCode { get; set; }

        public string SchoolSignInUrl { get; set; }

        public bool? IsActive { get; set; }


    }

    public class StudentModel
    {
        public int UserId { get; set; }

        public string EmailId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 to 50 characters long.")]
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        [RegularExpression(@"[a-zA-Z]*$", ErrorMessage = "{0} should contain only letters.")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between 2 to 50 characters long.")]
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"[\sa-zA-z'-]*$", ErrorMessage = "{0} should contain only letters, hyphens, and apostrophes.")]
        public string LastName { get; set; }

        public GradeDto Grade { get; set; }

        public string EncodedUserName { get; set; }
    }

    public class StudentLoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string SchoolSuffix { get; set; }
    }
}