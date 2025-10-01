using OrderManager.Application.DTOs;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;

namespace OrderManager.Application.Interfaces.IServices.Adm
{
    public interface IOrderQueriesAdmInterface
    {

        Task<ResponseModel<List<OrderDTO>>?> GetAllOrders();
        Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByUserEmail(UserEmailVO Email);
        Task<ResponseModel<OrderDTO>?> GetOrderByNumber(OrderNumberVO orderNumber);
        Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrence);
    }
}
