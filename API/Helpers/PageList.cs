using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PageList<T> : List<T>
{



    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

    public PageList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        AddRange(items);
    }


    public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PageList<T>(items, count, pageNumber, pageSize);
    }

    internal static async Task<PageList<LikeDto>> CreateAsync(Task<List<LikeDto>> likedUsers, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}