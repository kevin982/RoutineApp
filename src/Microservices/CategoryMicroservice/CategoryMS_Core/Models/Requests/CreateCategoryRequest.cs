using System.ComponentModel.DataAnnotations;

namespace CategoryMS_Core.Models.Requests
{
    public class CreateCategoryRequest
    {
        [Required(ErrorMessage = "Please provide the category name"), MinLength(3), MaxLength(30)]
        public string CategoryName { get; set; }
    }
}
