using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AxadoTest.Web
{
    public class IoCValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return DependencyResolver.Current.GetService(validatorType) as IValidator; 
        }
    }
}
