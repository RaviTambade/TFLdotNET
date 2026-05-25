namespace StateManagmentApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<string> Items { get; set; }
        public Cart() {
            this.Items = new List<string>();
        }
    }
}
