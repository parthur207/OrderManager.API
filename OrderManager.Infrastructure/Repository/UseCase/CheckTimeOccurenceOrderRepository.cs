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
            var Response = new SimpleResponseModel();

            try
            {
                var lastOccurrence = await _dbContextOM.OccurrenceEntity
                    .Where(oc => oc.OrderNumber.Value == orderNumber) 
                    .OrderByDescending(oc => oc.TimeOccurrence)
                    .FirstOrDefaultAsync();

                if (lastOccurrence == null)
                {
                    Response.Status = ResponseStatusEnum.Success;
                    return Response;
                }

                TimeSpan diff = DateTime.Now - lastOccurrence.TimeOccurrence;

                if (lastOccurrence.TypeOccurrence == typeOccurrence && diff.TotalMinutes <= 10)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "Não é possível a inserção de uma mesma ocorrência em um período de 10 minutos.";
                    return Response;
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
