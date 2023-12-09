using RazorClassLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddScoped<ApiService>();
//builder.Services.AddHttpClient<ApiService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddHttpClient("API TCC", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://localhost:7094/");
//    // using Microsoft.Net.Http.Headers;
//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.Accept, "application/json");
//    httpClient.DefaultRequestHeaders.Add(
//        HeaderNames.UserAgent, "HttpRequestsSample");
//})
//.ConfigurePrimaryHttpMessageHandler(() =>
//    new HttpClientHandler
//    {
//        AllowAutoRedirect = true,
//        UseDefaultCredentials = true
//    })
//.SetHandlerLifetime(TimeSpan.FromMinutes(5));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapDefaultControllerRoute();

app.Run();
