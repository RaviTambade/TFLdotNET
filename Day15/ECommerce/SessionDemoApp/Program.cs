using Core.Repositories;
using Core.Repositories.Interfaces;
using Core.Services;
using Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//register Distributed Memory for stroing session
      
builder.Services.AddTransient<IFruitRepository, FruitRepository>();
builder.Services.AddTransient<IFlowerRepository, FlowerRepository>();
builder.Services.AddTransient<IFlowerService, FlowerService>();
builder.Services.AddTransient<IFruitService, FruitService>();
builder.Services.AddTransient<IFinancialsService, FinancialsService>();

//services.AddMemoryCache();  /// keeping session data  in proc
builder.Services.AddDistributedMemoryCache();
            //setting session state enviornment at starup level
builder.Services.AddSession(options=>{
                options.Cookie.Name = ".Transflower.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
 app.UseSession(); //--------------------------set session middleware
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
