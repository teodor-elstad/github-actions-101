var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var application = builder.Build();

if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI();
}

application
    .MapGet("/games", GameState.Create)
    .WithTags("Games");

application
    .MapGet("/games/{gameStateId}", (Guid stateId) =>
        GameState.Create().WithId(stateId))
    .WithTags("Games");

application
    .MapGet("/games/{gameStateId}/open/{cardId}", (Guid stateId, Guid cardId) =>
        GameState.Create().WithId(stateId).OpenCard(cardId))
    .WithTags("Games");

application
    .MapGet("/ping", () => "pong")
    .WithTags("Diagnostics");

application.Run();

record GameState(Guid Id, Card[] Cards)
{
    public static GameState Create() =>
        new GameState(
            Id: Guid.NewGuid(),
            Cards: new Card[]
            {
                new Card(
                    Id: Guid.NewGuid(),
                    Position: new Position(Row: 1, Column: 1),
                    Open: new Uri("/games/90eb119a-3010-4424-b7d4-923a9b82851b/open/c8f957a6-32c8-40bd-bfd9-dd8f7d9ee6e3"),
                    Display: new Uri("/images/closed.jpeg"))
            });

    public GameState WithId(Guid id) =>
        this with { Id = id };

    public GameState OpenCard(Guid id) =>
        this with { Cards =
            this.Cards
                .Select(card => card with { Id = id, Display = new Uri("/images/open.jpeg") })
                .ToArray() };
}

record Card(Guid Id, Position Position, Uri Open, Uri Display);

record Position(uint Row, uint Column);