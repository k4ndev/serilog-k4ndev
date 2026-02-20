# K4ndev.Serilog

[![NuGet](https://img.shields.io/badge/NuGet-K4ndev.Serilog-blue?style=flat)](https://www.nuget.org/packages/K4ndev.Serilog)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat)](https://dotnet.microsoft.com)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat)](LICENSE)

**ASP.NET Core (.NET 10) üçün hazır Serilog konfiqurasiyası** — Client IP, User ID, TraceId/Span, Thread məlumatları və çox gözəl konsol çıxışı ilə.

---

## ✨ Xüsusiyyətlər

- Avtomatik **Client IP** (X-Forwarded-For + fallback)
- **User ID** (`ClaimTypes.NameIdentifier`)
- **ThreadId** + **ThreadName**
- **Span** + **TraceId** (OpenTelemetry uyğun)
- Yüngül və sürətli custom middleware
- `appsettings.json` tam dəstək
- Oxunaqlı və peşəkar konsol formatı
- Sıfır konfiqurasiya ilə işləyir

---

## 🚀 Tez quraşdırma

### 1. Paketi əlavə edin

```bash
dotnet add package K4ndev.Serilog
2. Program.cs
C#var builder = WebApplication.CreateBuilder(args);

// Serilog-u aktiv edin
builder.Host.UseK4ndevSerilog();

var app = builder.Build();

// UserId və ClientIp üçün middleware
app.UseK4ndevLoggingMiddleware();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
3. appsettings.json (tam nümunə)
JSON{
 "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore.Hosting": "Warning",
        "Microsoft.Hosting": "Information"
      }
    },
    "Filter": [
      {
        "Name": "ByExcluding",
        "Args": {
          "expression": "RequestPath like '/metrics%'"
        }
      }
    ],
    "Using": [
      "Serilog.Sinks.Console",
      "Serilog.Expressions"
    ],
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{TraceId}-{SpanId}] [{MachineName}] [User:{UserId}] [IP:{ClientIp}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithClientIp",
      "WithThreadId",
      "WithThreadName",
      "WithSpan"
    ]
  }
}

📘 English Version
Ready-to-use Serilog setup for ASP.NET Core (.NET 10) with Client IP, User ID, TraceId/Span, Thread info and beautiful console output.
Features

Automatic Client IP detection (X-Forwarded-For support)
User ID from Claims
ThreadId + ThreadName
Span + TraceId (OpenTelemetry compatible)
Lightweight custom middleware
Full appsettings.json support
Clean and professional log format

Quick Start
Bashdotnet add package K4ndev.Serilog
C#builder.Host.UseK4ndevSerilog();
app.UseK4ndevLoggingMiddleware();

📦 Paket məlumatları

PackageId: K4ndev.Serilog
Target Framework: net10.0
License: MIT
Repository: https://github.com/k4ndev/serilog-k4ndev


👨‍💻 Müəllif
Kamran Mirzayev (k4ndev)
Baku, Azerbaijan 🇦🇿

⭐ Repo xoşunuza gəldisə ulduz verin!
Suallarınız və ya təklifləriniz varsa Issue açın.