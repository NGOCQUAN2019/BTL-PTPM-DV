using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Nạp file cấu hình ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. Thêm dịch vụ Ocelot
builder.Services.AddOcelot(builder.Configuration);

// 3. Mở CORS tại Gateway (Front-end sẽ không bị lỗi nữa)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

// Khởi chạy Ocelot
await app.UseOcelot();

app.Run();