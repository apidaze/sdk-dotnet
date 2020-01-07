namespace APIdaze.SDK.Base
{
    internal interface IBaseApiClient
    {
        TResponse FindAll<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new();

        TResponse FindById<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new();

        TResponse Update<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new();

        TResponse Delete<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new();
    }
}