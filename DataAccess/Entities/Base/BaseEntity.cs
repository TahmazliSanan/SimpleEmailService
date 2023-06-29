namespace DataAccess.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}