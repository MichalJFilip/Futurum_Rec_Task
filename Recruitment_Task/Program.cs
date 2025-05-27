using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Recruitment_Task;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CampaignContext>(options =>
    options.UseSqlite("Data Source=campaigns.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost5173");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

/*
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CampaignContext>();
    if (!context.Campaigns.Any())
    {
        context.Campaigns.AddRange(
            new Campaign
            {
                Id = 1,
                Name = "Summer Sale",
                KeywordsString = "sale,clothes",
                BidAmount = 1.5m,
                CampaignFund = 1000m,
                Status = Status.On,
                Town = Town.Warsaw,
                Radius = 10
            },
            new Campaign
            {
                Id = 2,
                Name = "Tech Auction",
                KeywordsString = "auction,technologies",
                BidAmount = 2.0m,
                CampaignFund = 500m,
                Status = Status.Off,
                Town = Town.Cracow,
                Radius = 15
            },
            new Campaign
            {
                Id = 3,
                Name = "Perfume Promo",
                KeywordsString = "perfume",
                BidAmount = 1.0m,
                CampaignFund = 300m,
                Status = Status.On,
                Town = Town.Poznań,
                Radius = 5
            }
        );
        context.SaveChanges();
    }
}
*/

app.Run();
