using OrderManager.Application.DTOs.AdmDTOs;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IServices.Adm
{
    public interface IOrderQueriesAdmInterface
    {

        Task<ResponseModel<List<OrderDTOadm>>?> GetAllOrders();
        Task<ResponseModel<List<OrderDTOadm>>?> GetAllOrdersByUserEmail(UserEmailVO Email);
        Task<ResponseModel<OrderDTOadm>?> GetOrderByNumber(OrderNumberVO orderNumber);
        Task<ResponseModel<List<OrderDTOadm>>?> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrence);
    }
}
