﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanCommandHandler : IRequestHandler<AddFeatureToPlanCommand,AddFeatureToPlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IFeaturesRepository _featuresRepository;

        public AddFeatureToPlanCommandHandler(IMapper mapper,IUnitOfWorkContext unitOfWorkContext,IFeaturesRepository featuresRepository)
        {
            _mapper = mapper;
            _unitOfWorkContext = unitOfWorkContext;
            _featuresRepository = featuresRepository;
        }

        public async Task<AddFeatureToPlanResponse> Handle(AddFeatureToPlanCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork() )
            {
                await unitOfWork.BeginWork();
                try
                {
                    Feature feature = new Feature()
                    {
                        Id = Guid.NewGuid().ToString()
                    };
                    _mapper.Map(request, feature);
                    feature.Plan = new Plan
                    {
                        Id = request.PlanId
                    };
                    await _featuresRepository.AddFeatureToPlan(feature);
                    await unitOfWork.CommitWork();
                    return new AddFeatureToPlanResponse()
                    {
                        Id = feature.Id
                    };
                }
                catch (Exception)
                {
                    await unitOfWork.RollBack();
                    throw;
                }
            }
        }
    }
}