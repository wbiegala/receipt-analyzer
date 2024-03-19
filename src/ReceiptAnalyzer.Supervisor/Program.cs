using BS.ReceiptAnalyzer.Core;
using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Supervisor.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiKeyAuth(builder.Configuration);

builder.Services.AddCore(builder.Configuration);
builder.Services.AddDbContext<ReceiptAnalyzerDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ReceiptAnalyzerDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseApiKeyAuth();

app.MapControllers();

app.Run();
