# ลิขสิทธิ์ © 2025 supicha39mew — สงวนลิขสิทธิ์

# เอกสารโปรเจค (ภาษาไทย)

สรุปสั้น ๆ

โปรเจคนี้เป็นแอปพลิเคชัน .NET (C#) แบบเว็บซึ่งมีไฟล์โค้ดหลักในรากโปรเจคและหน้า Razor Pages ที่อยู่ในโฟลเดอร์ `Pages` มีคลาสตัวอย่างคือ `Calculator.cs` พร้อมชุดทดสอบใน `Tests/CalculatorTests` (ใช้ `dotnet test`) โดยโปรเจคถูกคอมไพล์เป็นไบนารีไว้ในโฟลเดอร์ `bin/` และไฟล์ build ชั่วคราวใน `obj/`.

**วัตถุประสงค์**
- ให้ตัวอย่างโครงงาน ASP.NET (Razor Pages) ขนาดเล็กที่แยกชั้น logic (เช่น `Calculator`) และมีชุดทดสอบเพื่อแสดงการทำงานของ `dotnet test`.

**เทคโนโลยีหลัก**
- .NET 8 (SDK)
- C#
- Razor Pages (`cshtml`)
- `dotnet` CLI

**โครงสร้างไฟล์สำคัญ**
- `Program.cs` : จุดเริ่มต้นของแอพ (entry point)
- `Web.csproj` : โปรเจคไฟล์ของแอพเว็บ
- `Calculator.cs` : คลาสตัวอย่าง (logic) ที่มีการทดสอบ
- `Pages/Index.cshtml` และ `Pages/Index.cshtml.cs` : หน้า Razor Page/handler
- `Tests/CalculatorTests/` : โปรเจคทดสอบ (unit tests)
- `bin/` : ผลลัพธ์การ build (executables, dlls)
- `obj/` : ไฟล์ชั่วคราวของการ build

**รายละเอียดส่วนประกอบ**
- `Program.cs` — กำหนด WebHost, routing และการตั้งค่า middleware เบื้องต้น
- `Web.csproj` — ระบุ dependency, SDK และ target framework (ดูว่าตั้งเป็น `net8.0`)
- `Calculator.cs` — ตัวอย่าง business logic (ใช้โดย `Index` page และทดสอบ)
- `Pages/Index.cshtml` — UI แบบ Razor Page สำหรับหน้าแรกของเว็บ
- `Tests/CalculatorTests/CalculatorTests.cs` — ชุดทดสอบ unit สำหรับ `Calculator`

การติดตั้งที่ต้องการ (Prerequisites)
- ติดตั้ง .NET SDK เวอร์ชัน 8.x: ตรวจสอบด้วยคำสั่ง `dotnet --info` ให้ขึ้นว่าเป็น `net8.0` หรือ SDK 8
- Bash shell (ตัวอย่างคำสั่งด้านล่างใช้ bash)

คำสั่งใช้งานพื้นฐาน

- Restore dependencies:
```bash
dotnet restore
```

- Build โปรเจค (ทั้ง solution หรือเฉพาะ Web project):
```bash
# build ทั้ง solution
dotnet build

# หรือ build เฉพาะโปรเจคเว็บ
dotnet build Web.csproj -c Debug
```

- รันทดสอบ (unit tests):
```bash
# รันทุก tests ใน repository
dotnet test

# หรือรันเฉพาะโปรเจคทดสอบ
dotnet test Tests/CalculatorTests/CalculatorTests.csproj -c Debug
```

- รันแอพเว็บ (จากรากโปรเจค):
```bash
# รันโปรเจคผ่าน dotnet (จะใช้ Web.csproj เป็นโปรเจคเป้าหมาย)
dotnet run --project Web.csproj -c Debug

# หรือถ้าต้องการรันไฟล์ที่คอมไพล์แล้ว (เช่น บนเครื่องที่ build แล้ว)
# บน Linux เข้าไปที่โฟลเดอร์ bin และรันไบนารีโดยตรง (ถ้ามี executable)
./bin/Debug/net8.0/Web
```

- สร้างเวอร์ชันสำหรับปล่อย (publish):
```bash
dotnet publish Web.csproj -c Release -o ./publish
# หลัง publish จะได้ไฟล์ไว้ใน ./publish สามารถนำขึ้นเซิร์ฟเวอร์หรือเป็น Docker image base ได้
```

การทดสอบแบบ manual
- หลัง `dotnet run` แอพจะบอก URL ที่ฟัง (เช่น `http://localhost:5xxx`) ให้เปิดเบราว์เซอร์ไปยัง URL นั้นเพื่อตรวจสอบ `Pages/Index.cshtml` และฟังก์ชัน Calculator

คำแนะนำเพิ่มเติม / Best practices
- หากต้องการ CI: ใช้ GitHub Actions หรือ Azure Pipelines เพื่อรัน `dotnet restore`, `dotnet build`, `dotnet test` และ `dotnet publish` บนแต่ละ push/PR
- หากจะทำเป็น container: สร้าง `Dockerfile` ที่ใช้ `mcr.microsoft.com/dotnet/aspnet:8.0` (runtime) และ `mcr.microsoft.com/dotnet/sdk:8.0` (build stage)

ตัวอย่าง Dockerfile (สั้น ๆ):
```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet publish Web.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "Web.dll"]
```

การขยายโปรเจค / แนวทางต่อ
- แยก logic ออกจาก UI ให้มากขึ้น (เช่นย้าย `Calculator` เข้าเป็น service ใน `src` ถ้ามี)
- เพิ่ม integration tests สำหรับ UI หรือ API
- ตั้งค่า logging, configuration (เช่น `appsettings.json`) สำหรับ environment ต่าง ๆ

บันทึก (Notes)
- เอกสารนี้เก็บคำศัพท์ทางเทคนิคไว้เป็นภาษาเดิม เช่น `src`, `bin`, `cshtml` ตามคำขอ

ถ้าต้องการ ผมสามารถ:
- เพิ่ม `README` เวอร์ชันภาษาอังกฤษ
- สร้างตัวอย่าง `Dockerfile` แบบเต็ม และไฟล์ `docker-compose` เพื่อรันทดสอบ
- เขียน GitHub Actions workflow สำหรับ CI (build + test + publish)

บอกผมได้ว่าต้องการให้ผมเพิ่มอะไรต่อไหม (เช่น commit, push, หรือสร้าง workflow)