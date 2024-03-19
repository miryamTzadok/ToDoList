using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoListContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ToDoListContext>();
builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddSwaggerGen();


var app = builder.Build();
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
app.UseSwaggerUI();

}
app.UseCors("AllowAll");
//קריאות שרת
app.MapGet("/",async() =>{
 
});
//שליפה
app.MapGet("/allitems",async(ToDoListContext db) =>{
 List<Item> list = new List<Item>();
    foreach (var i in db.Items)
    {
        list.Add(i);
    }
    return list;
});
//הוספה
app.MapPost("/newitem/{name}", async(ToDoListContext db,string name) => {
    Item item=new Item(){Name=name,IsComplete=false};
     db.Add(item);
    db.SaveChanges();
});
//עידכון
app.MapPut("/item/{id}/{isComplete}", async (ToDoListContext db,int id,bool isComplete) =>
{
   foreach (var item in db.Items)
        {
            if (item.Id == id)
            {
       item.IsComplete=isComplete;      
                break;
            }
        }
         db.SaveChanges();
});
//מחיקה
app.MapDelete("/de/{id}", async(ToDoListContext db,int id) =>
 { foreach (var item in db.Items)
        {
            if (item.Id == id)
            {
                db.Items.Remove(item);
                break;
            }
        }
        db.SaveChanges();});

app.Run();