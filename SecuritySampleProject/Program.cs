using SecurityMiddlewareRepository._585.Extensions;
using SecuritySampleProject.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddGenericSecurity<SampleSecurityContext, SampleSecurityValidator>(options =>
{
    options.EnableHeaderValidation = true;
    options.RequiredHeaderName = "X-Security-Token";
    // Tarayıcıdan girildiğinde veya belirli yollar için güvenlik doğrulamasını atlamak için hariç tutulacak yolları belirleyebilirsiniz.
    options.ExcludedPaths = ["/", "/Home/Index", "/Home/Error", "/Home/Privacy", "/favicon.ico"];
});

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

app.UseGenericSecurity<SampleSecurityContext>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();