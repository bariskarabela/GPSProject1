using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Core.Extensions
{
   public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email,email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name,name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier,nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role=>claims.Add(new Claim(ClaimTypes.Role,role)));
        }
        public static void AddPhone(this ICollection<Claim> claims, string phone)
        {
            claims.Add(new Claim(ClaimTypes.MobilePhone, phone));
        }
        public static void AddTown(this ICollection<Claim> claims, string town)
        {
            claims.Add(new Claim(ClaimTypes.Country, town));
        }
        public static void AddRank(this ICollection<Claim> claims, string rank)
        {
            claims.Add(new Claim(ClaimTypes.Actor, rank));
        }
    }
}
