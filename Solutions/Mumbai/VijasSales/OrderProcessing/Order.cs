namespace OrderProcessing;

public class Order
{
    public int OrderId { get; set; }
    public List<Item> Items{get; set;}
    public Order(int orderId)
    {
        OrderId = orderId;
        Items = new List<Item>();
    }
    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
    public void PrintOrder()
    {
        Console.WriteLine($"Order Id: {OrderId}");
        foreach (var item in Items)
        {
            Console.WriteLine($"Item Id: {item.ItemId}, Item Name: {item.ItemName}");
        }
    }
}
