using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.pl.ViewModels
{
    public class EmoployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is requaired")]
        [MaxLength(50, ErrorMessage = "MaxLength is 50")]
        [MinLength(5, ErrorMessage = "MinLength is 5")]
        public string Name { get; set; }
        [Range(0, 50, ErrorMessage = "age must be in range from 22 to 35")]
        public int? Age { get; set; }
        [RegularExpression("123", ErrorMessage = "Adress must be like 123-street -city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }//fk 
                                              //FK Optinal => ondelete:restric 
        [InverseProperty("Employees")]                     //FK Requaried => ondelete:Cascade 

        public Department Department { get; set; }
    }
}
