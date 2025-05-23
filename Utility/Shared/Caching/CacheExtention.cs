using Microsoft.Extensions.Caching.Memory;

namespace Shared.Caching;
public static class CacheExtention
{
    public static void SetInMemory<T>(this IMemoryCache memoryCache , string key,T value,TimeSpan time,long size)
    {
        memoryCache.Set(key, value,new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = time,
            Size = size
        });
    }
    public static async Task<T?> GetFromMemoryAsync<T>(this IMemoryCache memoryCache, string key)
    {
        if (memoryCache.TryGetValue(key, out T model)) return model;
        else return default;
    }
}
