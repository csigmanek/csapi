using System;
using csAPI.Models;
using Swashbuckle.AspNetCore.Filters;

namespace csAPI.SwaggerExamples
{
    public class FooRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Foo
            {
                Id = new Random().Next(),
                Value = Guid.NewGuid().ToString().Remove(6)
            };
        }
    }
}