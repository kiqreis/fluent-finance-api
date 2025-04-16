namespace FluentFinance.Core.Responses;

public class PagedResponse<T> : Response<T>
{
  public int CurrentPage { get; set; }
  public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
  public int PageSize { get; set; }
  public int TotalCount { get; set; }

  public PagedResponse(T? data, int code = Configuration.DefaultStatusCode, string message = null!) : base(data, code,
    message)
  {
  }

  public PagedResponse(T? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize) :
    base(data)
  {
    TotalCount = totalCount;
    CurrentPage = currentPage;
    PageSize = pageSize;
  }
}