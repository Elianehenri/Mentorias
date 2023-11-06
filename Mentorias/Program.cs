using Mentorias.Config;
using Mentorias.Data;
using Mentorias.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//conectar banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MentorShipContext>(options => options.UseSqlServer(connectionString));

//na hora de inicializar a aplicacao, ela esta pegando a configuracao do jwt la no appsettings.json e colocando na classe JWTKey
var jwtsettings = builder.Configuration.GetRequiredSection("JWT").Get<JWTKey>();

//configurar injecao de dependencia
//repositorio
RepositoryIoc.RegisterServices(builder.Services);
//services
ServicoIoc.RegisterServices(builder.Services);


//acessando a chave de seguranca
var secretKey = Encoding.ASCII.GetBytes(jwtsettings.SecretKey);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(authentication =>
{
    authentication.RequireHttpsMetadata = false;
    authentication.SaveToken = true;
    authentication.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
   

app.MapControllers();

app.Run();
