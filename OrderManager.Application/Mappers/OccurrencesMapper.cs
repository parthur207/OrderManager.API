using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces.IMapper;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;
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
        public ResponseModel<List<OccurrenceDTO>>? MapToOccurrenceDTOList(List<OccurrenceEntity> OccurrencesEntity)
        {
            ResponseModel<List<OccurrenceDTO>?> Response = new ResponseModel<List<OccurrenceDTO>>();
            try
            {
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
                    Response.Content.Add(occurrenceDTO);
                }

                Response.Status = ResponseStatusEnum.Success;
            }
            catch (Exception ex)
            {
                Response.Status = ResponseStatusEnum.CriticalError;
                Response.Message = "Ocorreu um erro inesperado: " + ex.Message;
                Debug.Assert(false, Response.Message);
            }
            return Response;
        }
    }
}
