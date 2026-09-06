using CirkusLuna.ClassLibrary.Repository;
using CirkusLuna.ClassLibrary.Service;

var builder = WebApplication.CreateBuilder(args);

// Path to JSON data folder
string dataPath = Path.Combine(
    builder.Environment.ContentRootPath,
    "Data"
);

// Make sure the Data folder exists
Directory.CreateDirectory(dataPath);

// Register repositories for dependency injection
builder.Services.AddSingleton<IEmployeeRepository>(
    new EmployeeJSONRepository(
        Path.Combine(dataPath, "employees.json")
    )
);

builder.Services.AddSingleton<IArtistRepository>(
    new ArtistJSONRepository(
        Path.Combine(dataPath, "artists.json")
    )
);

builder.Services.AddSingleton<IShowRepository>(
    new ShowJSONRepository(
        Path.Combine(dataPath, "shows.json")
    )
);

builder.Services.AddSingleton<ICustomerRepository>(
    new CustomerJSONRepository(
        Path.Combine(dataPath, "customers.json")
    )
);

builder.Services.AddSingleton<IReservationRepository>(
    new ReservationJSONRepository(
        Path.Combine(dataPath, "reservations.json")
    )
);

builder.Services.AddSingleton<INewsPostRepository>(
    new NewsPostJSONRepository(
        Path.Combine(dataPath, "newsposts.json")
    )
);

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
