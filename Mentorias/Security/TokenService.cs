
using Mentorias.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    public static string GenerateToken(object user, string jwtKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.ASCII.GetBytes(jwtKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user is Students ? ((Students)user).StudentId.ToString() : ((Teachers)user).TeacherId.ToString()),
            new Claim(ClaimTypes.Name, user is Students ? ((Students)user).Name : ((Teachers)user).Name),
            new Claim(ClaimTypes.Email, user is Students ? ((Students)user).Email : ((Teachers)user).Email)
        };

        if (user is Students)
        {
            claims.Add(new Claim("UserType", "Student"));
        }
        else if (user is Teachers)
        {
            claims.Add(new Claim("UserType", "Teacher"));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
