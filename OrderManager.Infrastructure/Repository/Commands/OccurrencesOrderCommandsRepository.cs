using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.Commands
{
    public class OccurrencesOrderCommandsRepository : IOccurrenceOrderCommandsRepository
    {
        private readonly DbContextOrderManager _dbContextOM;
        public OccurrencesOrderCommandsRepository(DbContextOrderManager dbContextOM)
        {
            _dbContextOM = dbContextOM;
        }

        public async Task<SimpleResponseModel> CreateOccurrenceToOrderRepository(OccurrenceEntity occurrenceEntity)
        {
            SimpleResponseModel Response = new SimpleResponseModel();

            try
            {
                var Order = await _dbContextOM.OrderEntity.Include(x => x.Occurrences)
                    .FirstOrDefaultAsync(x => x.OrderNumber.Value == occurrenceEntity.OrderNumber.Value);

                int orderNumber = occurrenceEntity.OrderNumber.Value;

                if (Order is null)//verificando se o pedido com o numero informado existe
                {
                    Response.Status = ResponseStatusEnum.NotFound;
                    Response.Message = $"Não existe um pedido com o número informado: {orderNumber}";
                    return Response;
                }

                if (Order.IndDelivered is false)//Verificando se o pedido ja foi entregue 
                {
                    if (Order.Occurrences.Any())//se já possui ocorrências
                    {
                        if (occurrenceEntity.TypeOccurrence.Equals(ETypeOccurrenceEnum.EntregueComSucesso))
                            Order.SetOrderStatusToDelivered();
                        
                        occurrenceEntity.SetOccurrenceToFinishing();//Modificando o status da ocorrência para finalizada
                        _dbContextOM.OrderEntity.Update(Order);
                        _dbContextOM.OccurrenceEntity.Update(occurrenceEntity);
                        await _dbContextOM.OrderEntity.AddAsync(Order);
                        await _dbContextOM.SaveChangesAsync();

                        Response.Status = ResponseStatusEnum.Success;
                        Response.Message = $"Uma nova ocorrência foi inclusa ao pedido!\n\nDetalhes:\nNumero do pedido: {orderNumber} | Tipo de ocorrência:{occurrenceEntity.TypeOccurrence} " +
                            $"| Horário de inserção da ocorrência: {occurrenceEntity.TimeOccurrence} | Finalizadora: {occurrenceEntity.IndFinishing}";
                    }
                    else
                    {
                        if (occurrenceEntity.TypeOccurrence.Equals(ETypeOccurrenceEnum.EntregueComSucesso))//A primeira ocorrencia de um pedido não pode ser de 'Entregue'
                        {
                            Response.Status = ResponseStatusEnum.Error;
                            Response.Message = $"Erro. A primeira ocorrência do pedido não pode ser do tipo 'Entregue com sucesso'.";
                        }
                        else
                        {
                            Order.AddOccurrenceToOrder(occurrenceEntity);
                            _dbContextOM.OrderEntity.Update(Order);
                            await _dbContextOM.SaveChangesAsync();

                            Response.Status = ResponseStatusEnum.Success;
                            Response.Message = $"Uma nova ocorrência foi inclusa ao pedido!\n\nDetalhes\nNumero do pedido: {orderNumber} | Tipo de ocorrência:{occurrenceEntity.TypeOccurrence} " +
                                $"| Horário de inserção da ocorrência: {occurrenceEntity.TimeOccurrence} | Finalizadora: {occurrenceEntity.IndFinishing}";
                        }
                    }
                }
                else
                {
                    Response.Status = ResponseStatusEnum.Error;
                    Response.Message = $"Erro. O pedido '{orderNumber}' já foi entregue, não é possível adicionar novas ocorrências.";
                }
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperaod: " + ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }

        public async Task<SimpleResponseModel> DeleteOccurrenceByOrderNumberRepository(OrderNumberVO orderNumber, int OccurrenceId)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                var Order = await _dbContextOM.OrderEntity.Include(x => x.Occurrences)
                    .FirstOrDefaultAsync(x => x.OrderNumber.Value == orderNumber.Value);

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
                    Response.Message = $"Não foi encontrada nenhuma ocorrência vinculada ao pedido n° '{Order.OrderNumber}' com o ID informado: {OccurrenceId}";
                    return Response;
                }

                Order.DeleteOccurrenceFromOrder(Occurrence);
                _dbContextOM.OrderEntity.Update(Order);
                await _dbContextOM.SaveChangesAsync();

                Response.Status = ResponseStatusEnum.Success;
                Response.Message = $"A ocorrência com ID '{OccurrenceId}' foi removida do pedido n° '{Order.OrderNumber}' com sucesso.";
            }
            catch(Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message="Ocorreu um erro inesperado: "+ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }
    }
}
