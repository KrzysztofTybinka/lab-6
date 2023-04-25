using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PdfGenerationService.Configuration;
using PdfGenerationService.Interceptors;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.Interceptors.Add<ExceptionInterceptor>();
});


builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddSingleton(new HtmlToPdfConfiguration());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PdfGenerationService.PdfGenerationService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();