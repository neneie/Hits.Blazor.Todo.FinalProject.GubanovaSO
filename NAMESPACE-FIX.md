# 🔧 ИСПРАВЛЕНИЕ NAMESPACE'ОВ

**ВАЖНО:** Твой проект называется `Hits.Blazor.Todo.FinalProject.GubanovaSO`, а не `EducationPlatform`!

Везде нужно заменить:
- ❌ `EducationPlatform` 
- ✅ `Hits.Blazor.Todo.FinalProject.GubanovaSO`

---

## 📝 Файл 1: Program.cs (ЗАМЕНИ ПОЛНОСТЬЮ)

```csharp
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services;

var builder = WebApplicationBuilder.CreateBuilder(args);

// 1️⃣ Подключение строки БД
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2️⃣ Добавление DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3️⃣ Добавление Identity
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

// 4️⃣ Добавление Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 5️⃣ Добавление сервисов
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

// 6️⃣ Добавление аутентификации
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<IdentityRevalidatingAuthenticationStateProvider>());

var app = builder.Build();

// 7️⃣ Выполнение миграций и создание ролей
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

// 8️⃣ Настройка middleware
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

// 9️⃣ Маршруты
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

---

## 📝 Файл 2: Components/Account/IdentityRevalidatingAuthenticationStateProvider.cs

```csharp
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Account
{
    public sealed class IdentityRevalidatingAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly NavigationManager _navigationManager;

        public IdentityRevalidatingAuthenticationStateProvider(
            IServiceProvider serviceProvider,
            NavigationManager navigationManager)
        {
            _serviceProvider = serviceProvider;
            _navigationManager = navigationManager;
        }

        protected override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return await Task.FromResult(new AuthenticationState(anonymous));
        }
    }
}
```

---

## 📝 Файл 3: _Imports.razor (ОБНОВИ)

```razor
@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Authorization
@using Microsoft.JSInterop

@* Blazor компоненты *@
@using Microsoft.AspNetCore.Authentication.OpenIdConnect
@using Microsoft.AspNetCore.Authentication.Cookies

@* Hits.Blazor - Модели *@
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models

@* Hits.Blazor - БД и контексты *@
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data

@* Hits.Blazor - Сервисы *@
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services

@* Компоненты проекта *@
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Pages
@using Hits.Blazor.Todo.FinalProject.GubanovaSO.Components.Shared
```

---

## ✅ Порядок действий:

1. **Замени Program.cs** полностью на Файл 1
2. **Обнови** `Components/Account/IdentityRevalidatingAuthenticationStateProvider.cs` на Файл 2
3. **Обнови** `_Imports.razor` на Файл 3
4. **Проверь ВСЕ классы** в папке `Models/` - везде должен быть namespace `Hits.Blazor.Todo.FinalProject.GubanovaSO.Models`
5. **Проверь ВСЕ классы** в папке `Data/` и `Data/Services/` - везде `Hits.Blazor.Todo.FinalProject.GubanovaSO.Data`

---

## 🧹 Очистка и Rebuild:

```powershell
dotnet clean
dotnet build
```

---

## 🎯 Если всё скомпилировалось:

```powershell
Add-Migration InitialCreate
Update-Database
```

---

**Готово! 🎉 Теперь namespace'ы будут правильные!**
