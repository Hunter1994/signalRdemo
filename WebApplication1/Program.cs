using WebApplication1.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7193");
                          policy.WithMethods("GET", "POST");
                          policy.AllowCredentials();
                      });
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
app.UseDefaultFiles();

app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/hub");

app.Run();
