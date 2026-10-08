var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DAL.Interfaces.IHocVienRepository, DAL.HocVienRepository>();
builder.Services.AddScoped<BLL.Interfaces.IHocVienBusiness, BLL.HocVienBusiness>();

builder.Services.AddScoped<DAL.Interfaces.IGiaoVienRepository, DAL.GiaoVienRepository>();
builder.Services.AddScoped<BLL.Interfaces.IGiaoVienBusiness, BLL.GiaoVienBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
