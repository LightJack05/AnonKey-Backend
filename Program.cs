var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Create a User in Backend
app.MapPost("/user", () =>
{
  return "Creates a user"; // A user id should be returned
})
.WithName("CreateUser")
.WithOpenApi();

// Get the User Inforamtion by Id
app.MapGet("/user{id}", (int id) =>
{
  return "Resturn the user information";
})
.WithName("GetUserInfo")
.WithOpenApi();

// Update the user's information by id
app.MapPut("/user{id}", (int id) =>
{
  return "Update the user's information";
})
.WithName("UpdateUsersInfo")
.WithOpenApi();


// Delete a user by id
app.MapDelete("/user{id}", (int id) =>
{
  return "Delete a user by id";
})
.WithName("UserDelete")
.WithOpenApi();

// Adds new key to the database
app.MapPost("/{user_id}/keys", (int user_id) =>
{
  return "Adds new key to the database"; // A key's id should be returned
})
.WithName("AddNewKey")
.WithOpenApi();

// Returns all user's keys
app.MapGet("/{user_id}/keys", (int user_id) =>
{
  return "Returns all users's keys";
})
.WithName("GetKeys")
.WithOpenApi();

// Get a specific user's key
app.MapGet("/{user_id}/keys/{key_id}", (int user_id, int key_id) =>
{
  return "Get a specific user's key";
})
.WithName("GetKey")
.WithOpenApi();

// Update a specific user's key
app.MapPut("/{user_id}/keys/{key_id}", (int user_id, int key_id) =>
{
  return "Update a specific user's key";
})
.WithName("UpdateKey")
.WithOpenApi();


// Delete a specific user's key
app.MapDelete("/{user_id}/keys/{key_id}", (int user_id, int key_id) =>
{
  return "Delete a specific user's key";
})
.WithName("DeleteKey")
.WithOpenApi();

app.Run();
