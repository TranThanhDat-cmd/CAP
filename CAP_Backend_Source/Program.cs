using CAP_Backend_Source.Common;
using CAP_Backend_Source.Models;
using CAP_Backend_Source.Modules.Account.Services;
using CAP_Backend_Source.Modules.Category.Services;
using CAP_Backend_Source.Modules.Faculty.Services;
using CAP_Backend_Source.Modules.Role.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json.Serialization;
using CAP_Backend_Source.Modules.FileStorage.Service;
using CAP_Backend_Source.Modules.Programs.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddDbContext<MyDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(new { message = context.ModelState?.FirstOrDefault(x => x.Value.ValidationState is ModelValidationState.Invalid).Value?.Errors[0].ErrorMessage }));



builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryResposity>();
builder.Services.AddScoped<IRoleService, RoleResposity>();
builder.Services.AddScoped<IFacultyService, FacultyResposity>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
