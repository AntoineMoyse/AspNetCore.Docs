﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace CachingMemorySample.Pages;

#region snippet_ClassConstructor
public class IndexModel : PageModel
{
    private readonly IMemoryCache _memoryCache;

    public IndexModel(IMemoryCache memoryCache) =>
        _memoryCache = memoryCache;

    // ...
    #endregion

    public DateTime CurrentDateTime { get; set; }

    public DateTime CacheCurrentDateTime { get; set; }

    #region snippet_OnGet
    public void OnGet()
    {
        CurrentDateTime = DateTime.Now;

        if (!_memoryCache.TryGetValue(CacheKeys.Entry, out DateTime cacheValue))
        {
            cacheValue = CurrentDateTime;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            _memoryCache.Set(CacheKeys.Entry, cacheValue, cacheEntryOptions);
        }

        CacheCurrentDateTime = cacheValue;
    }
    #endregion
}
