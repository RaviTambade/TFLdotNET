namespace OrderProcessing
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public Item(int itemId, string itemName)
        {
            ItemId = itemId;
            ItemName = itemName;
        }
    }
}