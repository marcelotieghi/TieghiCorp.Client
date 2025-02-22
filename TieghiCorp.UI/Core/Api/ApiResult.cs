namespace TieghiCorp.UI.Core.Api;

public record ApiResult(
    bool Success,
    string? Message = null,
    string? Error = null);

public record ApiResult<TData>(
    TData Data,
    bool Success = true,
    string? Message = null,
    string? Error = null) : ApiResult(Success, Message, Error);