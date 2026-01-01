using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UdemyCarBook.Application.Features.Mediator.Queries.StatisticsQueries;
using UdemyCarBook.Application.Features.Mediator.Results.StatisticsResults;
using UdemyCarBook.Application.Interfaces.StatisticsInterfaces;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class GetCarCountByFuelElectiricQueryHandler : IRequestHandler<GetCarCountByFuelElectiricQuery, GetCarCountByFuelElectiricQueryResult>
    {
        private readonly IstatisticsRepository _repository;

        public GetCarCountByFuelElectiricQueryHandler(IstatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCarCountByFuelElectiricQueryResult> Handle(GetCarCountByFuelElectiricQuery request, CancellationToken cancellationToken)
        {
            var value = _repository.GetCarCountByFuelElectiric();
            return new GetCarCountByFuelElectiricQueryResult
            {
                CarCountByFuelElectiric = value
            };
        }
    }
}
