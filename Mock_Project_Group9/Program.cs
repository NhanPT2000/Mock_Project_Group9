using Microsoft.EntityFrameworkCore;
using Mock_Project_Group9.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Configure Json File
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

//Connect to SQL server
builder.Services.AddDbContext<WebDBContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("WebDb")));

//
builder.Services.AddScoped(typeof(WebDBContext));


//Register Sessions
builder.Services.AddDistributedMemoryCache();           
builder.Services.AddSession(cfg => {                    
    cfg.Cookie.Name = "TranQuyNhon";             
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);   
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
