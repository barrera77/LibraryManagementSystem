using LibraryManagementSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddSingleton<IReaderRepository, ReaderRepository>();
builder.Services.AddSingleton<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddSingleton<IStaffRepository, StaffRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

//Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();