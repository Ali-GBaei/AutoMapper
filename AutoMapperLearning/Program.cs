var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure AutoMapper
// AddAutoMapper scans assemblies for Profile classes and registers them
// It also registers IMapper as a service for dependency injection
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AutoMapperDemo}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
