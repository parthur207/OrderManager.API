using OrderManager.Application.DTOs;
using OrderManager.Application.DTOs.AdmDTOs;
using OrderManager.Application.Interfaces.IServices.Adm;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.QueriesAdm
{
    public class OrderQueriesAdmService : IOrderQueriesAdmInterface
    {
        public Task<ResponseModel<List<OrderDTO>>?> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByTypeOccurrence(ETypeOccurrenceEnum occurrence)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<OrderDTO>>?> GetAllOrdersByUserEmail(UserEmailVO Email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<OrderDTO>?> GetOrderByNumber(OrderNumberVO orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
