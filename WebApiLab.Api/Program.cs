using Hellang.Middleware.ProblemDetails;

using Microsoft.EntityFrameworkCore;

using WebApiLab.Bll.Dtos;
using WebApiLab.Bll.Exceptions;
using WebApiLab.Bll.Interfaces;
using WebApiLab.Bll.Services;
using WebApiLab.Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//.AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddAutoMapper(typeof(WebApiProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) => false;
    options.Map<EntityNotFoundException>(
        (ctx, ex) =>
        {
            var pd = StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound);
            pd.Title = ex.Message;
            return pd;
        }
    );
});

var app = builder.Build();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
