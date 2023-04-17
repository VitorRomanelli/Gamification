﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Core.Entities
{
    public class StandardUser : User
    {
        public Guid SectorId { get; set; }
        public Sector? Sector { get; set; }
    }
}
