using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Player : IHasCreateDate,IHasActiveState
    {

        public Guid PlayerId { get; set; }
        public DateTime LastLogin { get; set; }
        public virtual List<Player> Friends { get; private set; } = new List<Player>();
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
    }
}