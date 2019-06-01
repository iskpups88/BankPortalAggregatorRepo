using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IS
{
    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "test",
                    Password = "password",                   
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "test@site"),
                        new Claim(JwtClaimTypes.Name, "Иван"),
                        new Claim(JwtClaimTypes.FamilyName, "Иванов"),
                        new Claim(JwtClaimTypes.MiddleName, "Иванович"),
                    }
                }
            };
        }
    }
}
