using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services;

var builder = WebApplicationBuilder.CreateBuilder(args);

// 1?? Подключение строки БД
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2?? Добавление DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3?? Добавление Identity
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddRoleManager<RoleManager<IdentityRole>>()
.AddDefaultTokenProviders();

// 4?? Добавление Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 5?? Добавление сервисов
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

// 6?? Добавление аутентификации
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<IdentityRevalidatingAuthenticationStateProvider>());

var app = builder.Build();

// 7?? Выполнение миграций и создание ролей
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EducationDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "Instructor", "Student" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 8?? Настройка middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// 9?? Маршруты
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
