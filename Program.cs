var builder = WebApplication.CreateBuilder(args);

//Stöd för MVC-mönster
builder.Services.AddControllersWithViews();

//Stöd för sessionshantering
builder.Services.AddSession(); 

var app = builder.Build();

 app.UseStaticFiles();  //Aktiverar statiska filer

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//Aktiverar sessionshantering
app.UseSession();

//Aktiverar Routing
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BMICalculatorController}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
