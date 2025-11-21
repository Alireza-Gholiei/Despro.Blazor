# Despro.Blazor

Despro.Blazor is a ready-to-use template for rapidly developing Blazor applications that works with both Blazor Server and Blazor WebAssembly.
The project is organized into independent NuGet packages, so you can add only the parts you need.
Each package provides practical components and utilities to simplify UI development and shared logic.

# Key Features

Supports Blazor Server and Blazor WebAssembly.

Modular architecture: each layer is published as an independent NuGet package.

Install only the packages you need via NuGet.

Rich set of ready-made components for building pages and forms quickly.

Easy to extend and customize.

# 📦 Package Structure

```mathematica
Despro.Blazor/
│
├─ Base/          → Core helpers and services (Helper, Extension, ServiceBase)
├─ Display/       → Visual elements for presenting data (labels, icons, viewers, etc.)
├─ Form/          → Form components (Input, Select, Validation, etc.)
├─ Layout/        → Page structure, header, sidebar, cards, and layout utilities
├─ Message/       → Notifications and messages (Toast, Alert, etc.)
├─ Modal/         → Dialog and modal windows
└─ Table/         → Dynamic data tables with advanced features
```

# 🔹 Sample Components by Package

# 1️⃣ Display

Show various types of content such as images, PDFs, status indicators, or hierarchical data.

```razor
<ImageViewer Src="image.jpg" Width="300" />
<PdfViewer FileUrl="doc.pdf" Height="600" />

<Status Value="Active" />
<StatusIndicator Status="Success" />
<TreeView Items="@Categories" />
```

# 2️⃣ Form

Rich input controls and ready-to-use forms with built-in validation.

```razor
<Form OnValidSubmit="HandleSave">
    <FormInputText Label="Name" @bind-Value="Model.Name" />
    <Datepicker @bind-Value="Model.BirthDate" />
    <Select Label="City" Items="@Cities" @bind-Value="Model.City" />
    <Checkbox Label="I accept the terms" @bind-Value="Model.Accepted" />
    <Button Text="Submit" Type="Submit" />
</Form>
```

Other useful controls:

```mathematica
Autocomplete
Typeahead
DragDrop
FormInputNumber
CheckboxTriState
```

# 3️⃣ Layout

Page layout helpers and common UI elements.

```razor
<Row>
    <RowCol Size="6">
        <Button Color="Primary" Text="Save" />
    </RowCol>
    <RowCol Size="6">
        <Badge Text="Beta Version" Color="Warning" />
    </RowCol>
</Row>

<Avatar Src="user.jpg" Size="Large" />
```

# 4️⃣ Modal

Interactive dialog windows.

```razor
<Modal @bind-Visible="ShowModal" Title="User Info">
    <p>User details go here.</p>
</Modal>

<Button Text="Open Dialog" OnClick="@(()=>ShowModal=true)" />
```

# 5️⃣ Table

Powerful data table with sorting, paging, and filtering.

```razor
<DesproTable Items="@UserList" PageSize="10">
    <Column Field="Name" Title="Name" />
    <Column Field="Email" Title="Email" />
</DesproTable>
```

# 🚀 Quick Start

# 1️⃣ Install the desired package

```bash
dotnet add package Despro.Blazor.Form
```

# 2️⃣ Register services in Program.cs

```C#
builder.Services.AddDesproBlazor();
```

# 3️⃣ Use in your Razor files

```razor
@using Despro.Blazor.Form.Components
```

# 🛠 For Developers

The codebase is fully modular: one folder = one NuGet package.
Each package can be built and published independently.
The Base folder contains shared extensions and services used across packages.

# Despro.Blazor

پروژه Despro.Blazor یک ساختار آماده برای توسعه‌ی سریع اپلیکیشن‌های Blazor است که هم در Blazor Server و هم در Blazor WebAssembly قابل استفاده می‌باشد.
این پروژه به صورت پکیج‌های مستقل طراحی شده تا بر اساس نیاز خود، هر بخش را به صورت جداگانه به پروژه اضافه کنید (از طریق NuGet Package).
هر پکیج شامل کامپوننت‌ها و ابزارهای کاربردی است که توسعه رابط کاربری و منطق مشترک را ساده می‌کند.

# ویژگی‌های اصلی

پشتیبانی همزمان از Blazor Server و Blazor WebAssembly

ساختار ماژولار: هر لایه به صورت پکیج مستقل منتشر می‌شود.

قابلیت نصب هر پکیج به صورت جداگانه از طریق NuGet.

مجموعه‌ای از کامپوننت‌های آماده برای ساخت سریع صفحات و فرم‌ها.

امکان توسعه و شخصی‌سازی آسان.

# 📦 ساختار پکیج‌ها

```mathematica
Despro.Blazor/
│
├─ Base/          → توابع و سرویس‌های پایه (Helper, Extension, ServiceBase)
├─ Display/       → نمایش داده‌ها و المان‌های تصویری (لیبل، آیکون و ...)
├─ Form/          → کامپوننت‌های فرم (Input، Select، Validation و ...)
├─ Layout/        → ساختار صفحه، هدر، سایدبار، کارت و ...
├─ Message/       → نمایش پیام‌ها (Toast، Alert، Notification)
├─ Modal/         → دیالوگ‌ها و پنجره‌های مودال
└─ Table/         → جدول‌های داینامیک با امکانات کامل
```

# 🔹 نمونه‌ کامپوننت‌های هر پکیج

# 1️⃣ Display

نمایش انواع محتوا مثل تصویر، PDF، وضعیت و ساختار درختی.

```razor
<ImageViewer Src="image.jpg" Width="300" />
<PdfViewer FileUrl="doc.pdf" Height="600" />

<Status Value="Active" />
<StatusIndicator Status="Success" />
<TreeView Items="@Categories" />
```

# 2️⃣ Form

ورودی‌های متنوع و فرم آماده با Validation داخلی.

```razor
<Form OnValidSubmit="HandleSave">
    <FormInputText Label="نام" @bind-Value="Model.Name" />
    <Datepicker @bind-Value="Model.BirthDate" />
    <Select Label="شهر" Items="@Cities" @bind-Value="Model.City" />
    <Checkbox Label="شرایط را می‌پذیرم" @bind-Value="Model.Accepted" />
    <Button Text="ارسال" Type="Submit" />
</Form>
```

کنترل‌های پرکاربرد:

```mathematica
Autocomplete

Typeahead

DragDrop

FormInputNumber

CheckboxTriState
```

# 3️⃣ Layout

چیدمان صفحه و عناصر عمومی.

```razor
<Row>
    <RowCol Size="6">
        <Button Color="Primary" Text="ذخیره" />
    </RowCol>
    <RowCol Size="6">
        <Badge Text="نسخه بتا" Color="Warning" />
    </RowCol>
</Row>

<Avatar Src="user.jpg" Size="Large" />
```

# 4️⃣ Modal

نمایش دیالوگ‌های تعاملی.

```razor
<Modal @bind-Visible="ShowModal" Title="اطلاعات کاربر">
    <p>جزئیات کاربر در اینجا نمایش داده می‌شود.</p>
</Modal>

<Button Text="نمایش پنجره" OnClick="@(()=>ShowModal=true)" />
```

# 5️⃣ Table

جدول داده با امکانات حرفه‌ای.

```razor
<Table Items="@UserList" PageSize="10">
    <Column Field="Name" Title="نام" />
    <Column Field="Email" Title="ایمیل" />
</Table>
```

# 🚀 شروع سریع

# 1️⃣ نصب پکیج دلخواه

```bash
dotnet add package Despro.Blazor.Form
```

# 2️⃣ ثبت سرویس‌ها در Program.cs

```csharp
builder.Services.AddDesproBlazor();
```

# 3️⃣ استفاده در Razor

```razor
@using Despro.Blazor.Form.Components
```

# 🛠 توسعه‌دهندگان

کدها به صورت ماژولار طراحی شده‌اند. هر پوشه = یک پکیج NuGet.

می‌توانید هر پکیج را مستقل Build و Publish کنید.

پوشه‌ی Base شامل Extensionها و سرویس‌های مشترک است.
