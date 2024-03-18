using System.Text.Json;
using Re_Store.RequestHelpers;

namespace Re_Store.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
    {
        var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData, option));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}