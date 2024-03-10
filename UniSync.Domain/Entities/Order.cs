using UniSync.Domain.Common;

namespace UniSync.Domain.Entities
{
    public class Order : AuditableEntity
    {
        private Order(int totalOrder, DateTime orderPlaced, Guid userId)
        {
            OrderId = Guid.NewGuid();
            TotalOrder = totalOrder;
            OrderPlaced = orderPlaced;
            UserId = userId;
        }

        public static Result<Order> Create(int totalOrder, DateTime orderPlaced, Guid userId)
        {
            if (totalOrder <= 0)
            {
                return Result<Order>.Failure("Total Order must be greater than zero.");
            }
            if (orderPlaced == default)
            {
                return Result<Order>.Failure("Order Placed is required.");
            }
            if (userId == default)
            {
                return Result<Order>.Failure("User id should not be default");
            }
            return Result<Order>.Success(new Order(totalOrder, orderPlaced, userId));
        }
        public Guid OrderId { get; private set; }
        public Guid UserId { get; private set; }
        public int TotalOrder { get; private set; }
        public DateTime OrderPlaced { get; private set; }
        public bool OrderPaid { get; private set; }

        public void MarkAsPaid()
        {
            OrderPaid = true;
        }
    }
}
