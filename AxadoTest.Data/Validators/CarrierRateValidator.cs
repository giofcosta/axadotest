﻿using AxadoTest.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxadoTest.Data.Validators
{
    public class CarrierRateValidator : AbstractValidator<CarrierRateViewModel>, ICarrierRateValidator
    {
        public CarrierRateValidator()
        {

        }
    }
}
