using FluentValidation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Security.WebApi.Validation.ValidatorFactory
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container _container;

        public FluentValidatorFactory(Container container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.GetInstance(validatorType) as IValidator;
        }
    }
}
