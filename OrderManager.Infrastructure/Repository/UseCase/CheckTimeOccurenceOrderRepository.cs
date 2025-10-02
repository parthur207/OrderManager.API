using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.UseCase
{
    public class CheckTimeOccurenceOrderRepository : ICheckTimeOccurrenceOrderRespository
    {

        private readonly DbContextOrderManager _dbContextOM;

        public CheckTimeOccurenceOrderRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }

        public async Task<SimpleResponseModel> CheckTimeRepository(int orderNumber, ETypeOccurrenceEnum typeOccurrence)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                var order = await _dbContextOM.OrderEntity.FirstOrDefaultAsync(x => x.OrderNumber.Value == orderNumber);

                if (order is null)
                {
                    Response.Message = $"Não foi encontrado nenhum pedido com o número informado: {orderNumber}";
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }
                var TimeLastOccurrence= order.Occurrences.FirstOrDefault();

                if (order.Occurrences is null || !order.Occurrences.Any())
                {
                    Response.Status=ResponseStatusEnum.Success;
                    return Response;
                }

                TimeSpan TimeDifference = DateTime.Now - TimeLastOccurrence.TimeOccurrence;

                if (order.Occurrences.Any(x => x.TypeOccurrence.Equals(typeOccurrence)) && TimeDifference.TotalMinutes <= 10)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "Não é possível a inserção de uma mesma ocorrencia em um período de 10 minutos.";
                }

                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Erro ao verificar o tempo de ocorrência do pedido: " + ex.Message;
            }
            return Response;
        }
    }
}
