خلاصه

این پروژه یک ساختار آماده (scaffolding) برای ساخت رابط‌های Blazor است که به‌صورت مجموعه پکیج‌های مجزا طراحی شده — یعنی هر لایه/بخش داخل پروژه یک پکیج مستقل است (مثلاً Despro.Blazor.Form, Despro.Blazor.Layout, Despro.Blazor.Modal, Despro.Blazor.Message, Despro.Blazor.Table, Despro.Blazor.Display, Despro.Blazor.Base). این پکیج‌ها هم برای Blazor Server و هم برای Blazor WebAssembly قابل استفاده و آمادهٔ انتشار به‌صورت NuGet هستند.

نصب و شروع سریع

نصب پکیج‌ها:

# نصب هر پکیج جدا
```C#
dotnet add package Despro.Blazor.Form
dotnet add package Despro.Blazor.Layout
dotnet add package Despro.Blazor.Modal
dotnet add package Despro.Blazor.Message
dotnet add package Despro.Blazor.Table
dotnet add package Despro.Blazor.Display
```
ثبت سرویس‌ها (Dependency Injection)

در Program.cs یا Startup.cs پروژهٔ Blazor خود، سرویس‌‌های مورد نیاز هر پکیج را رجیستر کنید. نمونهٔ کلی:

```C#
builder.Services.AddDesproBlazorForm();
builder.Services.AddDesproBlazorModal();
builder.Services.AddDesproBlazorMessage();
builder.Services.AddDesproBlazorTable();
builder.Services.AddDesproBlazorDisplay();
``` 

خلاصهٔ پکیج‌ها
```
Despro.Blazor.Base — کلاس‌ها و کمکی‌های پایه (BaseColor enum, ClassBuilder, DatePersian, EnumHelper و ...). این پکیج ابزارهایی فراهم می‌کند که در همهٔ پکیج‌های دیگر مورد استفاده‌اند (مثلاً enum رنگ‌ها). 

Despro.Blazor.Layout — عناصر UI عمومی (Button, Card, Dropdown, Avatar, Progress, Navbar و ...).

Despro.Blazor.Form — کنترل‌های فرم مثل Autocomplete, Datepicker, Select, Typeahead, ValueInput, Checkbox و... .

Despro.Blazor.Display — نمایش محتوا (ImageViewer, PdfViewer), Status indicators و TreeView.

Despro.Blazor.Message — پیام‌رسانی/Toast و Alert (ToastService / IToastService، کانتینر توست).

Despro.Blazor.Modal — سرویس نمایش مدال/آفلاین (ModalService / IModalService، Offcanvas) و کامپوننت‌های modal.

Despro.Blazor.Table — جدول پیشرفته با paging/grouping/sorting/editing و زیرساختی برای data factory و filter service. 
```
مثال‌ها و توضیحات برای هر پکیج

# 1) Despro.Blazor.Base

چی انجام می‌دهد: ابزارها و enumها/کلاس‌های کمکی (مثلاً BaseColor برای رنگ‌های استاندارد، ClassBuilder برای ساخت رشته کلاس‌های CSS، DatePersian برای تبدیل تاریخ‌ها و ...) — کاربرد عمومی در همهٔ دیگر پکیج‌ها. (فایل BaseGenerals/BaseColor.cs موجود است.) 

مثال ۱ — استفاده از Enum رنگ:

```C#
using Despro.Blazor.Base.BaseGenerals;

BaseColor btnColor = BaseColor.Blue;
```

مثال ۲ — ClassBuilder برای ساخت کلاس‌های CSS داینامیک:
```C#
var cb = new ClassBuilder("btn");
if (isPrimary) cb.Add("btn-primary");
if (isDisabled) cb.Add("disabled");
string css = cb.Build(); // "btn btn-primary" ...
```

# 2) Despro.Blazor.Layout

چی انجام می‌دهد: کامپوننت‌های ظاهری پایه مثل Button, Card, Dropdown, Avatar و غیره — برای سریع ساختن صفحات با استایل منسجم.

مثال ۱ — دکمه ساده:
```HTML
@using Despro.Blazor.Layout.Components.Buttons

<Button OnClick="@OnSave" Color="BaseColor.Blue">ذخیره</Button>

@code {
  void OnSave() { /* عملیات ذخیره */ }
}
```
توضیح: Color می‌تواند از BaseColor استفاده کند. دکمه‌ها معمولاً پارامترهایی مثل Size, Icon, Disabled, Loading دارند.

مثال ۲ — کارت با هدر و بدنه:
```Html
@using Despro.Blazor.Layout.Components.Cards

<Card>
  <CardHeader>اطلاعات کاربر</CardHeader>
  <CardBody>
    محتوا اینجا قرار می‌گیرد.
  </CardBody>
</Card>
```

# 3) Despro.Blazor.Form

چی انجام می‌دهد: کنترل‌های فرم پیشرفته (Autocomplete, Datepicker, Select, Typeahead، ورودی‌های شماره و متن و غیره). این پکیج برای فرم‌دهی قابل استفاده و قابل bind شدن است. 

مثال ۱ — Autocomplete ساده (local data):
```Html
@using Despro.Blazor.Form.Components.AutoComplete

<Autocomplete TItem="string"
              Items="cities"
              @bind-Value="selectedCity"
              Placeholder="نام شهر را وارد کنید" />

@code {
  List<string> cities = new() { "Tehran", "Mashhad", "Isfahan" };
  string selectedCity;
}
```

توضیح: این مثال حالت محلی (client-side) را نشان می‌دهد — کامپوننت معمولاً فهرست ورودی می‌گیرد و با @bind-Value مقدار انتخاب شده را نگه می‌دارد.

مثال ۲ — Autocomplete با provider async (سرور):
```Html
<Autocomplete TItem="City"
              ItemsProvider="LoadCities"
              @bind-Value="selectedCity"
              Placeholder="جستجو..." />

@code {
  Task<IEnumerable<City>> LoadCities(string filter, int count)
  {
    // درخواست به سرور یا فیلتر داخل حافظه
  }
  City selectedCity;
}
```

مثال ۳ — Datepicker (مبنایی):
```Html
<Datepicker @bind-Value="birthDate" Format="yyyy/MM/dd" />

@code {
DateTime? birthDate;
}
```

(پارامترهای دقیق مثل Format, MinDate, MaxDate معمول است.)

# 4) Despro.Blazor.Display

چی انجام می‌دهد: نمایش انواع محتوا — ImageViewer, PdfViewer, Status (آیکون وضعیت)، TreeView برای نمایش ساختار درختی. 

مثال ۱ — نمایش تصویر:
```Html
@using Despro.Blazor.Display.Components.ContentViewer

<ImageViewer Src="https://.../image.jpg" Alt="تصویر محصول" Width="400" />
```

مثال ۲ — نمایش PDF:
```Html
<PdfViewer Src="/files/manual.pdf" Height="700" />
```

مثال ۳ — TreeView ساده:
```Html
<TreeView Items="nodes" CheckboxMode="CheckboxMode.Multiple" OnItemDropped="OnDrop" />

@code {
  List<TreeNode> nodes = /* ساخت لیست نُدها */;
  void OnDrop(ItemDropped args) { /* مدیریت درگ/دراپ */ }
}
```
# 5) Despro.Blazor.Message (Toast / Alert)

چی انجام می‌دهد: سرویس نمایش پیام‌های موقت (Toast) و Alert‌ها. در repo فایل‌هایی مثل ToastService.cs, IToastService.cs, ToastContainer.razor وجود دارد. 

مثال استفاده از سرویس توست (برنامه‌ای):
```C#
@inject IToastService ToastService

void Save()
{
  // ذخیره انجام شد
  ToastService.ShowSuccess("ذخیره با موفقیت انجام شد");
  // یا
  ToastService.ShowError("خطا در ذخیره");
}
```

توضیح: معمولاً توست‌ها دارای گزینه‌هایی مثل مدت زمان نمایش، تایپ (success/error/warning) و متن هستند.

# 6) Despro.Blazor.Modal

چی انجام می‌دهد: سرویس و کامپوننت‌های مدال/دیالوگ (ModalService, IModalService, ModalContainer, DialogModal و Offcanvas). 

مثال ۱ — باز کردن یک دیالوگ برنامه‌ای (نمونهٔ مرسوم):
```C#
@inject IModalService ModalService

async Task OpenConfirm()
{
  var result = await ModalService.ShowDialogAsync("تأیید", contentParameters: new { Message = "آیا ادامه می‌دهید؟" });
  if (result.IsConfirmed) { /* کار را ادامه بده */ }
}
```

مثال ۲ — استفاده از کامپوننت modal در razor:
```Html
<DialogModal @ref="myDialog" Title="جزئیات" Size="ModalSize.Large">
  <p>محتوای دلخواه</p>
</DialogModal>

@code {
  DialogModal myDialog;
  void Open() => myDialog.Show();
}
```

(نام متدها/پراپرتی‌ها احتمالی‌اند؛ برای نام دقیق به ModalService.cs و DialogModal.razor.cs مراجعه کن.) 

# 7) Despro.Blazor.Table

چی انجام می‌دهد: جدول پیشرفته با قابلیت‌های grouping, sorting, paging, details row, header tools, row actions — دارای زیرساختی مانند TableFilterService, IDataFactory, IColumn, و کامپوننت‌های Table.razor, TableRow.razor, Pager.razor و غیره. 

مثال ۱ — جدول ساده با دادهٔ محلی:
```Html
@using Despro.Blazor.Table.Components.Table

<Table TItem="User" Items="users" Pageable="true" Sortable="true">
  <Column Field="@(u => u.Id)" Title="ID" />
  <Column Field="@(u => u.Name)" Title="نام" />
  <Column Field="@(u => u.Email)" Title="ایمیل" />
</Table>

@code {
  List<User> users = await UserService.GetAll();
}
```

مثال ۲ — استفاده از IDataFactory برای سرور-ساید paging:
```C#
// Table config: provide a data factory that returns paged TableResult<T>
```

(این پکیج ساختارهای کمکی برای فیلترینگ و factory دارد؛ برای جزئیات متدها به TableRepository/Service/TableFilterService.cs و TheGridDataFactory.cs مراجعه کنید.) 

Contributing

ساختار پروژه پکیج‌بیس است — برای اضافه کردن یک کامپوننت جدید بهتر است پکیج مرتبط را باز کرده و فایل‌های .razor و .razor.cs را مطابق الگوی موجود بسازید.
