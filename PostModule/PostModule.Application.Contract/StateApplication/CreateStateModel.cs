using Shared.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.StateApplication
{
    public class CreateStateModel
    {
        [Display(Name = "نام استان")]
        [Required(ErrorMessage = ValidationMessages.RequiredMessage)]
        [MaxLength(250,ErrorMessage = ValidationMessages.MaxLengthMessage)]
        public string Title { get; set; }
    }
}
