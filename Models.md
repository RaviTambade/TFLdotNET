
# MVC:Model

A model is a class with .cs (for C#) as an extension having both properties and methods. Models are used to set or get the data. If your application does not have data, then there is no need for a model. If your application has data, then you need a model.

The Model in an MVC application represents the state of the application and any business logic or operations it should perform. Business logic should be encapsulated in the model, along with any implementation logic for persisting the state of the application. Strongly-typed views typically use ViewModel types designed to contain the data to display on that view. The controller creates and populates these ViewModel instances from the model.

```
namespace FirstCoreMVCWebApplication.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Branch { get; set; }
        public string? Section { get; set; }
        public string? Gender { get; set; }
    }
}

```