﻿using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan.Persistence
{
    public interface IAddOfferToPlanCommandPersistence
    {
        Task<bool> OfferExist(long offerId);
        Task<long> AddOffer(long planId, Offer offer);
    }
}