using System.Text.Json.Serialization;

namespace FluentFinance.Core.Responses;

public class Response<T>
{
  private readonly int _code;

  public T? Data { get; set; }
  
  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string Message { get; set; } = string.Empty;

  [JsonConstructor]
  public Response()
  {
    _code = Configuration.DefaultStatusCode;
  }

  public Response(T data, int code = Configuration.DefaultStatusCode, string message = null!)
  {
    Data = data;
    Message = message;
    _code = code;
  }

  [JsonIgnore] public bool IsSuccess => _code is >= 200 and <= 299;
}