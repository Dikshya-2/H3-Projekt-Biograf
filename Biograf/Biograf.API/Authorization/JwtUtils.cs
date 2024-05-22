using Biograf.API.Help;
using Biograf.Repo.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biograf.API.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);

    }
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<JwtUtils> _logger; // Add ILogger
        public JwtUtils(IOptions<AppSettings> appSettings, ILogger<JwtUtils> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _logger.LogInformation("JwtUtils constructor called."); // Log information

        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            JwtSecurityTokenHandler tokenHandler = new();
            // Convert the secret key into a byte array. secret key from the app settings is converted to a byte array.
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            // Define the token's properties, such as the user's ID and expiration time (7 days)
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            // Create the token
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            // Return the token as a string
            return tokenHandler.WriteToken(token);

        }
        //checks if a given token is valid and extracts the user ID from it:
        public int? ValidateJwtToken(string token)
        {
            if (token == null)
            {
                return null;// If no token is provided
            }

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch (Exception ex)
            {
                // return null if validation fails
                return null;
            }

        }

    }
}
