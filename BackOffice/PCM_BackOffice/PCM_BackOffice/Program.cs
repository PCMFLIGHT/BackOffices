
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllersWithViews();

// Add distributed memory cache for session
builder.Services.AddDistributedMemoryCache();
// Configure session options
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(25);//You can set Time
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "home-index","{id}", defaults: new { controller = "Home", action = "home-index"});
app.MapControllerRoute(name: "Add-User-Details", "{id}", defaults: new { controller = "UserDetails", action = "Add-User-Details" });
app.MapControllerRoute(
    name: "UserDetails",
    pattern: "{controller=UserDetails}/{action=Index}/{id?}");

app.Run();
