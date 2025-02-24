using TieghiCorp.UI.Core.Models;

namespace TieghiCorp.UI.Core.Api;

public static class ApiRoutes
{
    private static readonly Dictionary<Type, string> ModelUrls = new()
    {
        { typeof(Location), "v1/locations" },
        { typeof(Department), "v1/departments" },
        { typeof(Personnel), "v1/personnel" }
    };

    public static string GetUrl<TModel>()
    {
        if (ModelUrls.TryGetValue(typeof(TModel), out var modelUrl))
        {
            return modelUrl;
        }
        else
        {
            throw new ArgumentException($"No URL found for model type: {typeof(TModel).Name}");
        }
    }
}