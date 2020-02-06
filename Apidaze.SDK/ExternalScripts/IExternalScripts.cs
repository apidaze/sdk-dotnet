using System;
using System.Collections.Generic;

namespace Apidaze.SDK.ExternalScripts
{
    public interface IExternalScripts
    {
        List<ExternalScript> GetExternalScripts();

        ExternalScript GetExternalScript(long id);

        ExternalScript UpdateExternalScript(long id, string name, Uri url);

        ExternalScript UpdateExternalScriptUrl(long id, Uri url);
    }
}