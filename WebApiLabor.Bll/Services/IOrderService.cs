using WebApiLabor.Entities;

namespace WebApiLabor.Bll.Services
{
    public interface IOrderService
    {
        Order CreateOrder(int productId, Order order);
    }
}