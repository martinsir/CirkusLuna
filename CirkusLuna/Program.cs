using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;

var builder = WebApplication.CreateBuilder(args);
// Register repositories for dependency injection
builder.Services.AddSingleton<IEmployeeRepository, EmployeeJSONRepository>();


string artistJsonPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data",
    "artists.json"
);

builder.Services.AddSingleton<IArtistRepository>(
    new ArtistJSONRepository(artistJsonPath)
);


builder.Services.AddSingleton<IShowRepository, ShowJSONRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerJSONRepository>();
builder.Services.AddSingleton<IReservationRepository, ReservationJSONRepository>();
builder.Services.AddSingleton<INewsPostRepository, NewsPostJSONRepository>();

// Services 
builder.Services.AddSingleton<IShowService, ShowService>();
builder.Services.AddSingleton<IReservationService, ReservationService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
