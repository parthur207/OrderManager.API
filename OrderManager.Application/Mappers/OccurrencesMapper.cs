using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Domain.Entities;
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

namespace OrderManager.Application.Mappers
{
    public class OccurrencesMapper : IOccurrenceMapperInterface
    {
        //List<Entity> para List<DTO>
        public ResponseModel<List<OccurrenceDTO>>? OccurrencesEntityListToDTOList(List<OccurrenceEntity> OccurrencesEntity)
        {
            ResponseModel<List<OccurrenceDTO>?> Response = new ResponseModel<List<OccurrenceDTO>>();
            try
            {
                List<OccurrenceDTO>? ListDTO = new List<OccurrenceDTO>();
                if (OccurrencesEntity is null)
                {
                    Response.Message = "Ocorrencias Nulas.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                foreach (var item in OccurrencesEntity)
                {
                    var occurrenceDTO = new OccurrenceDTO
                    (
                       item.TypeOccurrence,
                       item.TimeOccurrence,
                       item.IndFinishing
                    );
                    ListDTO.Add(occurrenceDTO);
                }
                Response.Content = ListDTO;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ResponseModel<OccurrenceEntity>? DeleteOccurrencesModelToEntity(DeleteOccurrenceOrderModel Model)
        {
            ResponseModel<OccurrenceEntity? > Response = new ResponseModel<OccurrenceEntity?>();
            try
            {
                if (Model is null)
                {
                    Response.Message = "Erro. Modelo de ocorrencia é nulo.";
                    Response.Status= ResponseStatusEnum.Error;
                    return Response;
                }
                var OccurrenceEntityConverted = new OccurrenceEntity
                    (
                       new OrderNumberVO(Model.OrderNumber),
                        Model.OccurrenceId
                    );

                Response.Content= OccurrenceEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = ex.Message;
            }
            return Response;
        }

        //Model para Entity
        public ResponseModel<OccurrenceEntity>? CreateOccurrencesModelToEntity(CreateOccurrenceToOrderModel OccurrencesModel)
        {
            ResponseModel<OccurrenceEntity>? Response = new ResponseModel<OccurrenceEntity>();
            try
            {
                if (OccurrencesModel is null)
                {
                    Response.Message = "Erro. Modelo de ocorrencias nulo.";
                    Response.Status = ResponseStatusEnum.Error;
                    return Response;
                }

                var OccurrenceEntityConverted = new OccurrenceEntity
                    (
                       new OrderNumberVO(OccurrencesModel.OrderNumber),
                        OccurrencesModel.ETypeOccurrence
                    );

                Response.Content = OccurrenceEntityConverted;
                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.Error;
                Response.Message = ex.Message;
            }
            return Response;
        }

        
    }
}
