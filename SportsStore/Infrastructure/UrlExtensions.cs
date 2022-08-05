using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        //Генерирует URL, по которому бразер будет возвращаться после обновления корзины, принимая во внимание строку запроса, если она есть
        public static string PathAndQuery(this HttpRequest request) => request.QueryString.HasValue ? $"{request.QueryString}" : request.Path.ToString();
    }
}
