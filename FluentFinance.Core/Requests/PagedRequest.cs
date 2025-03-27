using System.ComponentModel;

namespace FluentFinance.Core.Requests;

public class PagedRequest
{
  [DefaultValue(Configuration.DefaultPageNumber)]
  public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
  
  [DefaultValue(Configuration.DefaultPageSize)]
  public int PageSize { get; set; } = Configuration.DefaultPageSize;
}