using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

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
    public static async Task SetInSqlServerAsync<T>(this IDistributedCache distributed,string key ,T value)
    {
        await distributed.SetAsync(key,JsonSerializer.SerializeToUtf8Bytes(value));
    }
    public static async Task<T?> GetFromSqlServerAsync<T>(this IDistributedCache distributed,string key)
    {
        var data = await distributed.GetAsync(key);
        if(data == null) return default;
        return JsonSerializer.Deserialize<T>(data); 
    }
}
