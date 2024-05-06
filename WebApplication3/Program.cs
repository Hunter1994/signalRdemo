using WebApplication3.Hubs;
using Microsoft.AspNetCore.Identity;
using WebApplication3;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DemoDbContext>(options => {
    options.UseSqlite("Data Source=demo.db");
});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//}).AddEntityFrameworkStores<DemoDbContext>().AddDefaultUI();

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<DemoDbContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer();
builder.Services.AddSignalR()
    .AddMessagePackProtocol()
    .AddStackExchangeRedis("192.168.1.59:6379,password=redis01:Hw191119,defaultDatabase=8");
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();
