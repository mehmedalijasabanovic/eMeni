namespace eMeni.Application.Abstractions;

/// <summary>
/// Represents the currently logged-in user in the system.
/// </summary>
public interface IAppCurrentUser
{
    /// <summary>
    /// User identifier (UserId).
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// User Email. (optional)
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Indicates whether the user is logged in.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Indicates whether the user is an administrator.
    /// </summary>
    bool IsAdmin { get; }

    /// <summary>
    /// Indicates whether the user is a owner of bussines.
    /// </summary>
    bool IsOwner { get; }

    /// <summary>
    /// Indicates whether the user is a regular user.
    /// </summary>
    bool IsUser { get; }
}