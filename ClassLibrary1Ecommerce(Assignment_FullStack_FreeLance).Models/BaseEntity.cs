namespace Ecommerce_Assignment_FullStack_FreeLance_.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
