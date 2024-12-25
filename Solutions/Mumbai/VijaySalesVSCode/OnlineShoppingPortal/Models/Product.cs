using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingPortal.Models
{
    //Model class
    //Model is a class that represents the data of the application and
    //the business rules that govern access to and updates of this data.
    public class Product
    {
        //list of data annotations in c#
        //1. Required : Makes the property as required
        //2. Range : Validates the property value within a specified range
        //3. MinLength : Validates the minimum length of a string property
        //4. MaxLength : Validates the maximum length of a string property
        //5. RegularExpression : Validates the property value against a regular expression pattern
        //6. Compare : Validates two properties in a model
        //7. CreditCard : Validates a credit card number
        //8. EmailAddress : Validates an email address
        //9. Phone : Validates a phone number
        //10. Url : Validates a URL

        public int Id { get; set; }

        [Required]
        public string  Title { get; set; }
        public string  Description { get; set; }

        [Range(50, 10000)]
        public double Price { get; set; }

        public string ImageUrl { get; set; }
        
        [Range(10,6000)]
        public int Stock { get; set; }

    }
}
