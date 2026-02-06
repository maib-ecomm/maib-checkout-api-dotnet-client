using Maib.Checkout.Api.Connector.Models;
using Maib.Checkout.Api.Connector.Models.Requests;
using Maib.Checkout.Api.Connector.Models.Responses;

namespace Maib.Checkout.Api.Connector;

public interface IMaibCheckoutApiClient
{
    /// <summary>
    /// Refund payment
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OperationResult<RefundPaymentResponse?>> RefundPaymentAsync(RefundPaymentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create new checkout
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OperationResult<CreateCheckoutResponse?>> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate access token
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OperationResult<GenerateTokenResponse?>>  GenerateTokenAsync(GenerateTokenRequest request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get all checkouts by filter
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OperationResult<PagedList<CheckoutDto>>> GetAllCheckoutsAsync(GetAllCheckoutsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get checkout by id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OperationResult<CheckoutDto>> GetCheckoutByIdAsync(GetCheckoutByIdRequest request, CancellationToken cancellationToken = default);
}