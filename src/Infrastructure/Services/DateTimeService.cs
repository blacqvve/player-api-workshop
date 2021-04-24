using Player_API.Application.Common.Interfaces;
using System;

namespace Player_API.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
