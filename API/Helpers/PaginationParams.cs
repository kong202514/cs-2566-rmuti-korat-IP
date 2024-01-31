using System;

namespace API.Helpers;

public class PaginationParams
{
    private const int MaxPageSize = 50; //ป้องกัน user กำหนด pagesize เยอะเกินไป เช่น ล้านล้าน
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}
