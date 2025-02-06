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
    app.UseExceptionHandler("/Index/Error");
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
    pattern: "{controller=BMICalculator}/{action=Index}/{id?}") //Controller pekar mot BMICalculaterController för att hitta rätt vy att rendera
    .WithStaticAssets();


app.Run();
