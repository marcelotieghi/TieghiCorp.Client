using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Core.Services;

public interface IServices<TEntity> where TEntity : BaseModel
{
    Task<ApiResult> CreateAsync(string url, TEntity entity, CancellationToken cancellationToken = default);

    Task<ApiResult> UpdateAsync(string url, TEntity entity, CancellationToken cancellationToken = default);

    Task<ApiResult> DeleteAsync(string url, int id, CancellationToken cancellationToken = default);

    Task<ApiResult<TEntity>> GetByIdAsync(string url, int id, CancellationToken cancellationToken = default);

    Task<ApiPagedResult<TEntity>> ListAsync(
        string url,
        int page,
        int pageSize,
        string? searchTerm = null,
        string? sortField = null,
        string? sortDirection = null,
        CancellationToken cancellationToken = default);
}