# 📚 TechBookOnline

TechBookOnline เป็นเว็บแอปพลิเคชันสำหรับแสดงรายการหนังสือด้าน IT/Database ที่ผู้ใช้สามารถดูรายละเอียดหนังสือ กดถูกใจ และจัดเก็บหนังสือโปรดได้  
พัฒนาโดยใช้ **ASP.NET Core MVC + Entity Framework Core**

---

## ✨ คุณสมบัติ (Features)
- 🔐 ระบบ Login / Register
- 📖 แสดงรายการหนังสือพร้อมรูปภาพและรายละเอียด
- ❤️ ระบบกด "ถูกใจ" และบันทึกหนังสือโปรด
- 🔍 ดูรายละเอียดหนังสือ เช่น ชื่อผู้แต่ง, ราคา
- 👤 จัดการโปรไฟล์ผู้ใช้
- 🎨 UI สะอาดตา ใช้ Bootstrap 5

---

## ⚙️ การติดตั้งและรันโปรเจกต์

### 1. Clone โปรเจกต์
```bash
git clone https://github.com/yourusername/TechBookOnline.git
cd TechBookOnline

2. ติดตั้ง Dependencies
dotnet restore

3. ตั้งค่า Database
แก้ไข connection string ใน appsettings.json
จากนั้นรัน migration และ update database:
dotnet ef database update

4. รันโปรเจกต์
dotnet run

🛠️ เทคโนโลยีที่ใช้

ASP.NET Core MVC
Entity Framework Core
Bootstrap 5
C# .NET 8
Microsoft Access
