using System;
using System.Collections.Generic;
using System.Text;

namespace Apidaze.SDK.Applications
{
    public interface IApplications
    {
        List<Application> GetApplications();

        /**
         * Returns an application details retrieved by application id.
         * @param id id of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        List<Application> GetApplicationsById(long id);

        /**
         * Returns an application details retrieved by api key.
         * @param apiKey api key of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        List<Application> GetApplicationsByApiKey(String apiKey);

        /**
         * Returns an application details retrieved by api key.
         * @param name the name of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        List<Application> GetApplicationsByName(String name);
    }
}
