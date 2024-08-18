using Gadget_Gourmet.Models.Entities;
using Gadget_Gourmet.Models.Interface;
using Gadget_Gourmet.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IGeneric<Product>, Generic<Product> >(); 
builder.Services.AddScoped<IGeneric<User>, Generic<User> >();
builder.Services.AddScoped<IGeneric<Category>, Generic<Category> >();

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
