using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Application.Interfaces.IServices.ICommandsAdm;
using OrderManager.Application.RepositoryInterface.Commands;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using OrderManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Services.CommandsAdm
{
    public class OccurrencesOrderAdmService : IOccurrenceOrderCommandsAdmInterface
    {
        private readonly IOccurrenceMapperInterface _occurrenceMapper;
        private readonly IOccurrenceOrderCommandsRepository _occurrenceOrderCommandsRepository;
        public OccurrencesOrderAdmService(IOccurrenceMapperInterface occurrenceMapper, 
            IOccurrenceOrderCommandsRepository occurrenceOrderCommandsRepository)
        {
            _occurrenceMapper = occurrenceMapper;
            _occurrenceOrderCommandsRepository = occurrenceOrderCommandsRepository;
        }

        public async Task<SimpleResponseModel> CreateOccurrenceToOrder(CreateOccurrenceToOrderModel occurrenceModel)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                if(occurrenceModel is null)
                {
                    Response.Status= ResponseStatusEnum.Error;
                    Response.Message = "O modelo de ocorrência não pode ser nulo.";
                    return Response;
                }

                var OccurrenceEntityConverted = _occurrenceMapper.CreateOccurrencesModelToEntity(occurrenceModel);

                if (OccurrenceEntityConverted.Status.Equals(ResponseStatusEnum.Error) ||
                    OccurrenceEntityConverted.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status= OccurrenceEntityConverted.Status;
                    Response.Message = OccurrenceEntityConverted.Message;
                    return Response;
                }

                var ResponseRespository= await _occurrenceOrderCommandsRepository.CreateOccurrenceToOrderRepository(OccurrenceEntityConverted.Content);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error)
                    || Response.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;

                    return Response;
                }

                Response.Status = ResponseRespository.Status;
                Response.Message = ResponseRespository.Message;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: "+ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }

        public async Task<SimpleResponseModel> DeleteOccurrenceByOrderNumber(DeleteOccurrenceOrderModel Model)
        {
            SimpleResponseModel Response = new SimpleResponseModel();
            try
            {
                if(Model is null)
                {
                    Response.Message = "O modelo de exclusão de ocorrência não pode ser nulo.";
                    Response.Status= ResponseStatusEnum.Error;
                    return Response;
                }

                var OccurrenceEntity= _occurrenceMapper.DeleteOccurrencesModelToEntity(Model);


                if (OccurrenceEntity.Status.Equals(ResponseStatusEnum.Error) ||
                    OccurrenceEntity.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    if (Response.Status.Equals(ResponseStatusEnum.CriticalError))
                        Response.Message = "Ocorreu um erro inesperado.";
                    else
                        Response.Message = OccurrenceEntity.Message; 
                }
                var ResponseRespository= await _occurrenceOrderCommandsRepository.DeleteOccurrenceByOrderNumberRepository(
                    new OrderNumberVO(OccurrenceEntity.Content.OrderNumber), OccurrenceEntity.Content.Id);

                if (ResponseRespository.Status.Equals(ResponseStatusEnum.Error) ||
                 ResponseRespository.Status.Equals(ResponseStatusEnum.CriticalError))
                {
                    Response.Status = ResponseRespository.Status;
                    Response.Message = ResponseRespository.Message;
                    return Response;
                }
                Response.Status= ResponseRespository.Status;
                Response.Message = ResponseRespository.Message;
            }
            catch (Exception ex)
            {
                Response.Status= ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: "+ex.Message;
            }
            return Response;
        }
    }
}
