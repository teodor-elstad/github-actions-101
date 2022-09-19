namespace Notes.Api.Test;

using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Controllers;
using Notes.Api.Database;
using Notes.Api.Models;
using Xunit;

public class Controllers : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly NotesDb _database;
    private readonly NotesController _notesController;
    private readonly PingController _pingController;

    public Controllers()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var databaseOptions = new DbContextOptionsBuilder<NotesDb>()
            .UseSqlite(_connection)
            .Options;

        _database = new NotesDb(databaseOptions);
        _database.SeedData();

        _notesController = new NotesController(_database);
        _pingController = new PingController();
    }

    [Fact]
    public void ShouldReturnPong() => _pingController.GetPing().Should().Be("PONG");

    [Fact]
    public void ShouldGetNotes() => _notesController.GetNotes().Should().NotBeEmpty();

    [Fact]
    public void ShouldCreateNotes() =>
        _notesController
            .CreateNote(new CreateNote
            {
                Content = "Husk å kjøpe melk"
            })
            .Result.As<CreatedAtRouteResult>()
            .Value.As<Note>()
            .Content.Should().Be("Husk å kjøpe melk");

    [Fact]
    public void ShouldGetNote() =>
        _notesController
            .GetNote(1)
            .Result.As<OkObjectResult>()
            .Value.As<Note>()
            .Id.Should().Be(1);

    [Fact]
    public void ShouldNotGetNote() =>
        _notesController
            .GetNote(-1)
            .Result.Should().BeOfType<NotFoundObjectResult>();

    [Fact]
    public void ShouldUpdateNote() =>
        _notesController
            .UpdateNote(1, new UpdateNote
            {
                Content = "Helt nytt innhold!"
            })
            .Result.As<OkObjectResult>()
            .Value.As<Note>()
            .Content.Should().Be("Helt nytt innhold!");

    [Fact]
    public void ShouldNotUpdateNote() =>
        _notesController
            .UpdateNote(-1, new UpdateNote
            {
                Content = "Helt nytt innhold!"
            })
            .Result.Should().BeOfType<NotFoundObjectResult>();

    [Fact]
    public void ShouldDeleteNote() =>
        _notesController
            .DeleteNote(1)
            .Result.Should().BeOfType<OkResult>();

    [Fact]
    public void ShouldNotDeleteNote() =>
        _notesController
            .DeleteNote(-1)
            .Result.Should().BeOfType<NotFoundObjectResult>();

    public void Dispose()
    {
        _database.Dispose();
        _connection.Dispose();
    }
}