﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Model
{
    public class CarrierRate : BaseEntity
    {
        public virtual Carrier Carrier { get; set; }
        public long CarrierId { get; set; }
        public int Rate { get; set; }
        public string UserId { get; set; }
    }
}
