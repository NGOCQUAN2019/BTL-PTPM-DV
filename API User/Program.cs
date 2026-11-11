using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

builder.Services.AddScoped<DAL.Interfaces.IKhoaHocRepository, DAL.KhoaHocRepository>();
builder.Services.AddScoped<BLL.Interfaces.IKhoaHocBusiness, BLL.KhoaHocBusiness>();

builder.Services.AddScoped<DAL.Interfaces.IHoaDonRepository, DAL.HoaDonRepository>();
builder.Services.AddScoped<BLL.Interfaces.IHoaDonBusiness, BLL.HoaDonBusiness>();

builder.Services.AddScoped<DAL.Interfaces.IDiemSoRepository, DAL.DiemSoRepository>();
builder.Services.AddScoped<BLL.Interfaces.IDiemSoBusiness, BLL.DiemSoBusiness>();

builder.Services.AddScoped<DAL.Interfaces.ILopHocRepository, DAL.LopHocRepository>();
builder.Services.AddScoped<BLL.Interfaces.ILopHocBusiness, BLL.LopHocBusiness>();

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
