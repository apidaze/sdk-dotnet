using System;
using System.Collections.Generic;

namespace Apidaze.SDK.CdrHttpHandlers
{
    public interface ICdrHttpHandlers
    {
        List<CdrHttpHandler> GetCdrHttpHandlers();

        CdrHttpHandler CreateCdrHttpHandler(string name, Uri url);

        CdrHttpHandler UpdateCdrHttpHandler(long id, string name, Uri url);

        CdrHttpHandler UpdateCdrHttpHandlerName(long id, string name);

        CdrHttpHandler UpdateCdrHttpHandlerUrl(long id, Uri url);

        void DeleteCdrHttpHandler(long id);
    }
}