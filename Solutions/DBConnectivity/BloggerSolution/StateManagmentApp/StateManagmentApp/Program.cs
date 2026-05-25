var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Steps to create server side session managment

// step 1: configuring Session Mgmt 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>  //--------------------*****
{
    options.Cookie.Name = "transflower.Session";
    options.IdleTimeout= TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//Output Cache configuration
 //builder.Services.AddOutputCache();

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
        builder.Expire(TimeSpan.FromSeconds(5)));

    options.AddPolicy("CacheForTenSeconds", builder =>
        builder.Expire(TimeSpan.FromSeconds(10)));

    options.AddPolicy("CacheFor40Seconds", builder =>
       builder.Expire(TimeSpan.FromSeconds(40)));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}




//Middleware Settings : setting up HTTP Pipeline

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//Step 2: Session Managment
app.UseSession();    //-----------------------------*****

app.UseOutputCache();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();