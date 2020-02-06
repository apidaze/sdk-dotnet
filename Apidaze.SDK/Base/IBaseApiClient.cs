using System.Collections.Generic;

namespace Apidaze.SDK.Base
{
    internal interface IBaseApiClient
    {
        T Create<T>(Dictionary<string, string> requestParams) where T : new();

        IEnumerable<T> FindAll<T>();

        IEnumerable<T> FindByParameter<T>(string name, string value) where T : new();

        T FindById<T>(string id) where T : new();

        T Update<T>(string id, Dictionary<string, string> requestParams) where T : new();

        void Delete(string id);
    }
}