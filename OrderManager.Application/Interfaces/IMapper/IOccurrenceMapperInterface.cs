using OrderManager.Application.DTOs;
using OrderManager.Domain.Entities;
using OrderManager.Domain.Models;
using OrderManager.Domain.Models.ReponsePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.Interfaces.IMapper
{
    public interface IOccurrenceMapperInterface
    {
        ResponseModel<List<OccurrenceDTO>>? OccurrencesEntityListToDTOList(List<OccurrenceEntity> OccurrencesEntity);

        ResponseModel<OccurrenceEntity>? DeleteOccurrencesModelToEntity(DeleteOccurrenceOrderModel Model);

        ResponseModel<OccurrenceEntity>? CreateOccurrencesModelToEntity(CreateOccurrenceToOrderModel OccurrencesModel);
    }
}
