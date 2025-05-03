using Amazon.S3;
using FluentValidation;
using ArtUnion_API.Data;
using ArtUnion_API.Configs;
using ArtUnion_API.Services.Interfaces;
using ArtUnion_API.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers().AddJsonOptions(options => 
    options.AddStringEnumConverter()
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAmazonS3Service, AmazonS3Service>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<DataContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddJwtBearerAuthentication();
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

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