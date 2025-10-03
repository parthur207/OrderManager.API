using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using OrderManager.Infrastructure.Persistence;
using System.Diagnostics;

namespace OrderManager.Infrastructure.Repository.Commands
{
    public class OccurrencesOrderCommandsRepository : IOccurrenceOrderCommandsRepository
    {
        private readonly DbContextOrderManager _dbContextOM;
        private readonly ICheckTimeOccurrenceOrderRespository _checkTimeOccurrenceOrderRespository;
        public OccurrencesOrderCommandsRepository(DbContextOrderManager dbContextOM, ICheckTimeOccurrenceOrderRespository checkTimeOccurrenceOrderRespository)
        {
            _dbContextOM = dbContextOM;
            _checkTimeOccurrenceOrderRespository = checkTimeOccurrenceOrderRespository;
        }

        public async Task<SimpleResponseModel> CreateOccurrenceToOrderRepository(OccurrenceEntity occurrenceEntity)
        {
            var Response = new SimpleResponseModel();

            try
            {
                var ResponseCheckTime = await _checkTimeOccurrenceOrderRespository
                    .CheckTimeRepository(occurrenceEntity.OrderNumber.Value, occurrenceEntity.TypeOccurrence);

                if (ResponseCheckTime.Status == ResponseStatusEnum.Error)
                    return ResponseCheckTime;

                int OrderNumberFK = occurrenceEntity.OrderNumber.Value;

                var order = await _dbContextOM.OrderEntity
                    .Include(x => x.Occurrences)
                    .FirstOrDefaultAsync(x => x.OrderNumber.Value == OrderNumberFK);

                if (order == null)
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = $"Não existe um pedido com o número informado: {occurrenceEntity.OrderNumber}";
                    return Response;
                }

                if (order.IndDelivered)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = $"Erro. O pedido '{occurrenceEntity.OrderNumber}' já foi entregue, não é possível adicionar novas ocorrências.";
                    return Response;
                }

                if (!order.Occurrences.Any())
                {
                    if (occurrenceEntity.TypeOccurrence == ETypeOccurrenceEnum.EntregueComSucesso)
                    {
                        Response.Status = ResponseStatusEnum.Error;
                        Response.Message = $"Erro. A primeira ocorrência do pedido não pode ser do tipo 'Entregue com sucesso'.";
                        return Response;
                    }

                    order.AddOccurrenceToOrder(occurrenceEntity);
                    // Não precisa chamar Update(order) se order está trackeado
                    await _dbContextOM.SaveChangesAsync();
                }
                else
                {
                    // já possui ocorrências
                    if (occurrenceEntity.TypeOccurrence == ETypeOccurrenceEnum.EntregueComSucesso)
                    {
                        order.SetOrderStatusToDelivered();
                    }

                    occurrenceEntity.SetOccurrenceToFinishing();

                    order.AddOccurrenceToOrder(occurrenceEntity);
                    // order está trackeado, ao salvar, o EF vai inserir a ocorrência nova
                    await _dbContextOM.SaveChangesAsync();
                }

                Response.Status = ResponseStatusEnum.Success;
                Response.Message = $"Uma nova ocorrência foi inclusa ao pedido! Número: {occurrenceEntity.OrderNumber} | Tipo: {occurrenceEntity.TypeOccurrence} | Horário: {occurrenceEntity.TimeOccurrence}";

            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
            }

            return Response;
        }

        public async Task<SimpleResponseModel> DeleteOccurrenceByOrderNumberRepository(OrderNumberVO orderNumber, int OccurrenceId)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                int orderNumberValue = orderNumber.Value;
                var Order = await _dbContextOM.OrderEntity.Include(x => x.Occurrences)
                    .FirstOrDefaultAsync(x => x.OrderNumber.Value == orderNumberValue);

                if (Order is null)//verifica se o pedido existe
                {
                    Response.Message = "Não foi encontrado nenhum pedido com o número informado: " + orderNumber.Value;
                    Response.Status = ResponseStatusEnum.NotFound;
                    return Response;
                }

                if (Order.IndDelivered is true)//verifica se o pedido já foi entregue
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = "O pedido informado já foi entregue.";
                    return Response;
                }

                var Occurrence = Order.Occurrences.FirstOrDefault(x => x.Id == OccurrenceId);
                
                if (Occurrence is null)//Verifica se a ocorrência passada existe no pedido informado
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = $"Não foi encontrada nenhuma ocorrência vinculada ao pedido n° '{Order.OrderNumber.Value}' com o ID informado: {OccurrenceId}";
                    return Response;
                }

                Order.DeleteOccurrenceFromOrder(Occurrence);
                _dbContextOM.OrderEntity.Update(Order);
                await _dbContextOM.SaveChangesAsync();

                Response.Status = ResponseStatusEnum.Success;
                Response.Message = $"A ocorrência com ID '{OccurrenceId}' foi removida do pedido n° '{Order.OrderNumber.Value}' com sucesso.";
            }
            catch(Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message="Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }
    }
}
