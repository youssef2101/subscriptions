﻿using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class TimeLine
    {
        public abstract bool IsValid(DateTime date);
        public DateTimeRange DateTimeRange { get; set; }
    }
}