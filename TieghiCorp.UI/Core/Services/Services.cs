using System.Net.Http.Json;
using TieghiCorp.UI.Core.Api;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Core.Services;

public class Services<TEntity>(HttpClient http) : IServices<TEntity> where TEntity : BaseModel
{
    private readonly HttpClient _http = http;

    public async Task<ApiResult> CreateAsync(
        string url,
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _http.PostAsJsonAsync(url, entity, cancellationToken);

            if (result.IsSuccessStatusCode)
                return new ApiResult(true, "Creation completed successfully.");

            var errorDetails = await result.Content.ReadAsStringAsync(cancellationToken);
            return new ApiResult(false, $"Error: {result.ReasonPhrase}", errorDetails);
        }
        catch (Exception ex)
        {
            return new ApiResult(false, "An error occurred while processing the create request.", ex.Message);
        }
    }

    public async Task<ApiResult> UpdateAsync(
        string url,
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            url += $"/{entity.Id}";

            var result = await _http.PutAsJsonAsync(url, entity, cancellationToken);

            if (result.IsSuccessStatusCode)
                return new ApiResult(true, "Update completed successfully.");

            var errorDetails = await result.Content.ReadAsStringAsync(cancellationToken);

            return new ApiResult(false, $"Error: {result.ReasonPhrase}", errorDetails);
        }
        catch (Exception ex)
        {
            return new ApiResult(false, "An error occurred while processing the update request.", ex.Message);
        }
    }

    public async Task<ApiResult> DeleteAsync(
        string url,
        int id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            url += $"/{id}";

            var result = await _http.DeleteAsync(url, cancellationToken);

            if (result.IsSuccessStatusCode)
                return new ApiResult(true, "Deleted completed successfully.");

            var errorDetails = await result.Content.ReadAsStringAsync(cancellationToken);
            return new ApiResult(false, $"Error: {result.ReasonPhrase}", errorDetails);
        }
        catch (Exception ex)
        {
            return new ApiResult(false, "An error occurred while processing the update request.", ex.Message);
        }
    }

    public async Task<ApiResult<TEntity>> GetByIdAsync(
        string url,
        int id,
        CancellationToken cancellationToken = default)
    {
        url += $"/{id}";

        var result = await _http.GetFromJsonAsync<TEntity>(url, cancellationToken);

        return result is null
            ? new ApiResult<TEntity>(null!, false, $"Entity not found.")
            : new ApiResult<TEntity>(result);
    }

    public async Task<ApiPagedResult<TEntity>> ListAsync(
        string url,
        int page,
        int pageSize,
        string? searchTerm = null,
        string? sortField = null,
        string? sortDirection = null,
        CancellationToken cancellationToken = default)
    {
        url += $"?pageNumber={page}&pageSize={pageSize}";

        if (!string.IsNullOrEmpty(searchTerm))
            url += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";

        if (!string.IsNullOrEmpty(sortField))
            url += $"&sortField={sortField}";

        if (!string.IsNullOrEmpty(sortDirection))
            url += $"&sortDirection={sortDirection}";

        var result = await _http.GetFromJsonAsync<ApiPagedResult<TEntity>>(
            url,
            cancellationToken);

        if (result?.Data is null)
        {
            return new ApiPagedResult<TEntity>
            {
                Data = [],
                TotalCount = 0
            };
        }

        var startNumber = (page - 1) * pageSize + 1;

        var numberedData = result.Data
            .Select((location, index) =>
            {
                location.Nr = startNumber + index;
                return location;
            }).ToList();

        return new ApiPagedResult<TEntity>
        {
            Data = numberedData,
            TotalCount = result.TotalCount
        };
    }
}