namespace Catalog;
using System.ComponentModel.DataAnnotations;
//POCO  : Plain Old CLR Object
[Serializable]    //attribute  :Metadata
public class Product
{
    public int Id{get;set;}

    [Required]
    public string Title{get;set;}
    public string Description{get;set;}
    public double UnitPrice{get;set;}

    public Product(){
        this.Id=12;
        this.Title="Gerbera";
        this.Description="Wedding Flower";
        this.UnitPrice=12;
    }
    public Product(int id, string title, string description, double price){
        this.Id=id;
        this.Title=title;
        this.Description=description;
        this.UnitPrice=price;
    }
}
