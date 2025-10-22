using fundamentals.git.http.rest.api.infra;
using fundamentals.git.http.rest.api.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEntryDiaryData, EntryDiaryData>();
builder.Services.AddSingleton<IEntryDiaryService, EntryDiaryService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

