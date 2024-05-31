using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachineAssign.Models
{
    public class EmpModel
    {
        public int Id  { get; set; }
        [Required(ErrorMessage = "Please enter your Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your First Name")]
        [StringLength(50, ErrorMessage = "First Name must not exceed 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your Last Name")]
        [StringLength(50, ErrorMessage = "Last Name must not exceed 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your Mobile Number")]
        [StringLength(10, ErrorMessage = "Mobile number must not exceed 10 digits")]
        public string MobileNumber { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter your Date of Birth")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter your Date of joining")]
        public string joinDate { get; set; }
        [Required(ErrorMessage = "Please select a file.")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only JPG and PNG files are allowed.")]
        [MaxFileSize(200 * 1024, ErrorMessage = "Maximum allowed file size is 200 KB.")]
        public IFormFile profilepicture { get; set; }
        public string profilepicpath { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your Pan Number")]
        [RegularExpression("^[A-Z0-9]*$",
         ErrorMessage = "Only uppercase Characters are allowed.")]
        [StringLength(10, ErrorMessage = "String length must not exceed 10 characters.")]
        public string Pan_no { get; set; }
        [Required(ErrorMessage = "Please enter your Passport Number")]
        [RegularExpression(@"[A-Z0-9]*$",
         ErrorMessage = "Only uppercase Characters are allowed.")]
        public string Passport_no { get; set; }
        [Required(ErrorMessage = "Please select your Gender")]
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var file = value as IFormFile;
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Only JPG and PNG files are allowed.";
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var file = value as IFormFile;
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize / 1024} KB.";
        }
    }
}
