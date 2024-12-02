using AmazonReviewGenerator.Business;
using AmazonReviewGenerator.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<DataLoader>();
builder.Services.AddSingleton<MarkovChainGenerator>();
builder.Services.AddControllers();

// Add Razor Pages
builder.Services.AddControllersWithViews(); // Add MVC support

var app = builder.Build();

app.UseStaticFiles(); // Enable serving static files (CSS/JS)
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Set up routing

app.Run();
