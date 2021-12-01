using Back;
using Back.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>();

var app = builder.Build();

using (var db = new AppDBContext())
{
    // нужны базовые Создать, Читать, Изменить, Удалить, Список для всех. Для записей полная детализация

    // Master GET, фильтр (date строка в формате 2021-08-29, timeInterval ид)
    // нужен список специалистов (Master), доступные на эту дату и временной промежуток
    // если аргументы не заданы, возвращать всех

    // Record GET фильтр (date строка в формате 2021-08-29)
    // список записей по этой дате, иначе все

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var client1 = new Client() { Name = "Клиент 1", Email = "test1@test.com", HasTelegram = true, Phone = "8(800)555-35-35" };
    var client2 = new Client() { Name = "Клиент 2", Email = "test2@test.com", HasWhatsapp = true, Phone = "7(937)999-99-99" };
    var client3 = new Client() { Name = "Клиент 3", Email = "test3@test.com", IsVIP = true, Phone = "7(937)945-99-99" };
    var client4 = new Client() { Name = "Клиент 4", Email = "test4@test.com", HasWhatsapp = true, Phone = "7(937)777-99-91" };
    db.Clients.Add(client1);
    db.Clients.Add(client2);

    var nails1 = new Service() { Title = "Ноготочки", Cost = 3000 };
    var nails2 = new Service() { Title = "Педикюр", Cost = 3500 };
    db.Services.Add(nails1);
    db.Services.Add(nails2);

    var master1 = new Master() { Name = "Мастер 1", WorkPhone = "7(998)999-88-99" };
    var master2 = new Master() { Name = "Мастер 2", WorkPhone = "7(997)999-77-99" };
    db.Masters.Add(master1);
    db.Masters.Add(master2);

    var before = new TimeInterval() { Title = "с 9:00 до 13:00" };
    var after = new TimeInterval() { Title = "с 14:00 до 18:00" };
    db.TimeIntervals.Add(before);
    db.TimeIntervals.Add(after);

    var today = DateTime.Today;
    db.Records.Add(new Record() { Client = client1, Master = master1, TimeInterval = before, Date = today, Service = nails1 });
    db.Records.Add(new Record() { Client = client2, Master = master1, TimeInterval = after, Date = today, Service = nails1 });
    db.Records.Add(new Record() { Client = client3, Master = master2, TimeInterval = before, Date = today, Service = nails2 });
    db.Records.Add(new Record() { Client = client4, Master = master2, TimeInterval = after, Date = today, Service = nails2 });

    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
