namespace Catalog;

public class Product
{          
    //Data Members
    private int Id;
    private string title;
    private string imageURL;
    private string category;
    private string description;
    private float unitPrice;
    private int balance;
    private string paymentTerm;
    private string delivery;
    
       
    //Costructor Overloading
    public Product(){
    }

    public Product(int productId, string title){
        this.Id = productId;
        this.title = title;
        
    }

    public Product(int productId, string title, string brand, string category){
        this.Id = productId;
        this.title = title;       
        this.category = category;
    }

    public Product(int productId, string title, string brand,
                    string category, float unitPrice, int balance){
        this.Id = productId;
        this.title = title;
        this.category = category;
        this.unitPrice = unitPrice;
        this.balance = balance;
    }

    public Product(int productId, string title, string brand, string category,
                    float unitPrice, 
                    int balance, string description, string imageURL){
        this.Id = productId;
        this.title = title;   
        this.category = category;
        this.unitPrice = unitPrice;
        this.balance = balance;
        this.description = description;
        this.imageURL = imageURL;
    }

    //Properties of Product Entity

    public int ProductId{
        get { return Id; }
        set { Id = value; }
    }
    
    public string Title{
            get { return title; }
            set { title = value; }
    }


    public string Category{
        get { return category; }
        set { category = value; }
    }
        
    public string Description{
        get { return description; }
        set { description = value; }
    }

    
    public string PaymentTerm{
        get { return paymentTerm;}
        set { paymentTerm = value; }
    }

    public string Delivery {
        get{  return delivery;}
        set{ delivery = value; }
    }

    public string ImageURL{
        get  {   return imageURL; }
        set  { imageURL = value; }
    }

    public float UnitPrice{
        get  { return unitPrice; }
        set  { unitPrice = value; }
    }
    
    public int Balance{
        get{  return balance; }
        set{  balance = value;}
    }     
}