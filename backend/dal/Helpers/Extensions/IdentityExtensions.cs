using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Pims.Dal.Exceptions;

namespace Pims.Dal.Helpers.Extensions
{
    /// <summary>
    /// IdentityExtensions static class, provides extension methods for user identity.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Get the currently logged in user's ClaimTypes.NameIdentifier.
        /// Return an empty Guid if no user is logged in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            return String.IsNullOrWhiteSpace(value) ? Guid.Empty : new Guid(value);
        }

        /// <summary>
        /// Get the user's primary agency.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int? GetAgency(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue("agency");
            return String.IsNullOrWhiteSpace(value) ? null : (int?)Int32.Parse(value);
        }

        /// <summary>
        /// Get the user's list of agencies they have access to.
        /// Return an empty array if no agencies are found.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetAgencies(this ClaimsPrincipal user)
        {
            var agencies = user?.FindAll("agencies");
            return agencies?.Select(c => Int32.Parse(c.Value));
        }

        /// <summary>
        /// Get the user's display name.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue("name");
            return value;
        }

        /// <summary>
        /// Get the user's first name.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetFirstName(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue(ClaimTypes.GivenName);
            return value;
        }

        /// <summary>
        /// Get the user's last name.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetLastName(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue(ClaimTypes.Surname);
            return value;
        }

        /// <summary>
        /// Get the user's email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var value = user?.FindFirstValue(ClaimTypes.Email);
            return value;
        }

        /// <summary>
        /// Determine if the user any of the specified roles.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns>True if the user has any of the roles.</returns>
        public static bool HasRole(this ClaimsPrincipal user, params string[] role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (role.Length == 0) throw new ArgumentOutOfRangeException(nameof(role));

            return user.Claims.Any(c => c.Type == ClaimTypes.Role && role.Contains(c.Value));
        }

        /// <summary>
        /// Determine if the user all of the specified roles.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns>True if the user has all of the roles.</returns>
        public static bool HasRoles(this ClaimsPrincipal user, params string[] role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (role.Length == 0) throw new ArgumentOutOfRangeException(nameof(role));

            var count = user.Claims.Count(c => c.Type == ClaimTypes.Role && role.Contains(c.Value));

            return count == role.Length;
        }

        /// <summary>
        /// If the user does has not been authenticated throw a NotAuthorizedException.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <exception type="NotAuthorizedException">User does not have the specified 'role'.</exception>
        /// <returns></returns>
        public static ClaimsPrincipal ThrowIfNotAuthorized(this ClaimsPrincipal user, string message = null)
        {
            if (user == null || !user.Identity.IsAuthenticated) throw new NotAuthorizedException(message);
            return user;
        }

        /// <summary>
        /// If the user does not have the specified 'role' throw a NotAuthorizedException.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="message"></param>
        /// <exception type="NotAuthorizedException">User does not have the specified 'role'.</exception>
        /// <returns></returns>
        public static ClaimsPrincipal ThrowIfNotAuthorized(this ClaimsPrincipal user, string role, string message = null)
        {
            if (user == null || !user.HasRole(role)) throw new NotAuthorizedException(message);
            return user;
        }
    }
}
