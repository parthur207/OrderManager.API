using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.RepositoryInterface.UseCase
{
    public interface ICheckTimeOccurrenceOrderRespository
    {
        Task<SimpleResponseModel> CheckTimeRepository(int OrderNumber, ETypeOccurrenceEnum TypeOccurrenceEnum);
    }
}
