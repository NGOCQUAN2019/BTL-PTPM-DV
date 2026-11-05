using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Cấu hình nút Authorize trên giao diện Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Chỉ cần dán trực tiếp chuỗi Token của bạn vào đây (hệ thống sẽ tự động thêm chữ Bearer).",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,

        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, // Đổi từ ApiKey sang Http
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddScoped<DAL.Interfaces.IHocVienRepository, DAL.HocVienRepository>();
builder.Services.AddScoped<BLL.Interfaces.IHocVienBusiness, BLL.HocVienBusiness>();

builder.Services.AddScoped<DAL.Interfaces.IGiaoVienRepository, DAL.GiaoVienRepository>();
builder.Services.AddScoped<BLL.Interfaces.IGiaoVienBusiness, BLL.GiaoVienBusiness>();

builder.Services.AddScoped<DAL.Interfaces.IGhiDanhRepository, DAL.GhiDanhRepository>();
builder.Services.AddScoped<BLL.Interfaces.IGhiDanhBusiness, BLL.GhiDanhBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
