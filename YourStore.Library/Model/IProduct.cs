namespace YourStore.Library.Model
{
    public interface IProduct
    {
        float Cost { get; set; }
        int ID { get; set; }
        string Name { get; set; }

        void Refill();
        bool Restockable();
    }
}