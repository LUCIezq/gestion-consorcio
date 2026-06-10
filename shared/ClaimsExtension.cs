using System.Security.Claims;

namespace PracticaParcial.shared
{
    public static class ClaimsExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            return Guid.Parse(claim!.Value);
        }
    }
}