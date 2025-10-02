using Microsoft.EntityFrameworkCore;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Infrastructure.Repository.UseCase
{
    public class CheckOrderNumberExistsRepository : ICheckOrderNumberExistsRepository
    {
        private readonly DbContextOrderManager _dbContextOrderManager;
        public CheckOrderNumberExistsRepository(DbContextOrderManager dbContextOrderManager)
        {
            _dbContextOrderManager = dbContextOrderManager;
        }
        public async  Task<SimpleResponseModel> CheckOrderNumberRepository(int GeneratedNumber)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                if (GeneratedNumber>9999)
                {
                    Response.Message = "Erro. O número deve ser entre 1000 e 9999.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var OrderExists = await _dbContextOrderManager.OrderEntity.AnyAsync(x => x.OrderNumber.Value == GeneratedNumber);

                if(OrderExists is true)
                {
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception EX)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + EX.Message;
            }
            return Response;
        }
    }
}
