using System;
using System.Collections.Generic;
using System.Text;

namespace Apidaze.SDK.Base
{
    internal interface IBaseApiClient
    {
       TResponse Create<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new();
    }
}