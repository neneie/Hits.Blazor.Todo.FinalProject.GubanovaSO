# 🔧 Решение ошибок при миграции в Blazor + SQL Server

---

## ⚠️ Частые ошибки при `Add-Migration InitialCreate`

### Ошибка 1: Build failed - синтаксические ошибки

**Когда:** После ввода команды `Add-Migration InitialCreate`

**Причина:** Ошибки в коде (синтаксис C#)

**Решение:**
1. Посмотри точное сообщение об ошибке
2. Найди файл с ошибкой (обычно указан в сообщении)
3. Исправь синтаксис

**Распространённые ошибки:**

```csharp
// ❌ НЕПРАВИЛЬНО - забыли точку с запятой
public string Title { get; set }

// ✅ ПРАВИЛЬНО
public string Title { get; set; }
```

```csharp
// ❌ НЕПРАВИЛЬНО - нет using
public class Course
{
    public List<Lesson> Lessons { get; set; }
}

// ✅ ПРАВИЛЬНО
using System.Collections.Generic;

public class Course
{
    public List<Lesson> Lessons { get; set; }
}
```

---

### Ошибка 2: "Cannot find DbSet"

**Сообщение:**
```
The type 'EducationDbContext' has no usable constructors
```

**Причина:** DbContext неправильно сконфигурирован

**Решение:** Проверь `EducationDbContext.cs`:

```csharp
// ✅ ПРАВИЛЬНО
public class EducationDbContext : DbContext
{
    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<TestResult> TestResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
```

---

### Ошибка 3: "No usable constructor found"

**Сообщение:**
```
No usable constructor of 'EducationDbContext' found
```

**Причина:** DbContext требует конструктор, принимающий `DbContextOptions`

**Решение:**

```csharp
// ❌ НЕПРАВИЛЬНО
public class EducationDbContext : DbContext
{
    // Нет конструктора!
}

// ✅ ПРАВИЛЬНО
public class EducationDbContext : DbContext
{
    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options) { }
}
```

---

### Ошибка 4: "Project does not reference Microsoft.EntityFrameworkCore"

**Сообщение:**
```
The type or namespace name 'DbContext' does not exist
```

**Причина:** NuGet пакеты не установлены

**Решение:**

```powershell
# В Package Manager Console
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Затем повтори:
```powershell
Add-Migration InitialCreate
```

---

### Ошибка 5: "The name 'DbSet' does not exist"

**Причина:** Забыл `using`

**Решение:** Добавь в начало файла:

```csharp
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
```

---

### Ошибка 6: Ошибка в модели (Model Class)

**Сообщение:**
```
'Course' does not contain a definition for 'Id'
```

**Причина:** Свойство не определено в классе

**Решение:** Проверь класс:

```csharp
// ❌ НЕПРАВИЛЬНО
public class Course
{
    public string Title { get; set; }
}

// ✅ ПРАВИЛЬНО
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
}
```

---

## 🎯 Чек-лист перед миграцией

### Файловая структура

```
YourProject/
├── Models/
│   ├── Course.cs           ✅ Создан?
│   ├── Lesson.cs           ✅ Создан?
│   ├── Test.cs             ✅ Создан?
│   ├── Question.cs         ✅ Создан?
│   ├── TestResult.cs       ✅ Создан?
│   ├── Enrollment.cs       ✅ Создан?
│   ├── ApplicationUser.cs  ✅ Создан?
│   └── ...
├── Data/
│   ├── ApplicationDbContext.cs    ✅ Создан?
│   └── EducationDbContext.cs      ✅ Создан?
├── Program.cs              ✅ Обновлён?
├── appsettings.json        ✅ Обновлён?
└── YourProject.csproj      ✅ Обновлён?
```

### Проверка Program.cs

```csharp
// ✅ Есть эти строки?

// 1. Подключение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Регистрация DbContext
builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Регистрация Identity
builder.Services.AddIdentityCore<ApplicationUser>(options => ...)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    ...
```

### Проверка appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EducationPlatformDB;Integrated Security=true;Encrypt=false;"
  }
}
```

---

## 🚀 Пошаговое исправление

### Шаг 1: Очистить проект

```powershell
# В Package Manager Console
Remove-Item -Path .\.vs -Recurse -Force
```

```
В Visual Studio:
Solution Explorer → Right Click Solution → Clean Solution
Solution Explorer → Right Click Solution → Rebuild Solution
```

### Шаг 2: Переустановить NuGet

```powershell
Update-Package -Reinstall
```

### Шаг 3: Проверить синтаксис

Открой все файлы Model и проверь:
- ✅ Все свойства имеют `get; set;`
- ✅ Все классы наследуют правильно
- ✅ Нет красных волнистых линий (ошибок)

### Шаг 4: Заново создать миграцию

```powershell
Remove-Migration

Add-Migration InitialCreate
```

### Шаг 5: Если всё ещё не работает

Скопируй **полный текст ошибки** и пришли мне!

---

## 📋 Пример правильной модели

```csharp
using System;
using System.Collections.Generic;

namespace EducationPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int DifficultyLevel { get; set; }
        public int DurationHours { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string InstructorId { get; set; }

        // Навигационные свойства
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public List<Test> Tests { get; set; } = new List<Test>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
```

---

## ✅ Всё исправлено! Теперь:

```powershell
# Финальные команды
Add-Migration InitialCreate
Update-Database
```

---

**Если ошибка остаётся - скопируй полный текст ошибки! 📝**

Покажи:
1. Полное сообщение об ошибке (весь текст)
2. На какой файл указывает ошибка
3. Номер строки в файле

Помогу разобраться! 💪
