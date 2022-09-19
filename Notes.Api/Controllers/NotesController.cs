namespace Notes.Api.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Database;
using Notes.Api.Models;

[ApiController]
[Route("[controller]")]
public class NotesController : ControllerBase
{
    private readonly NotesDb _database;

    public NotesController(NotesDb database)
    {
        _database = database;
    }

    /// <summary>
    /// Retrieves a list of all sticky notes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Note[] Get()
    {
        return _database.Notes.ToArray();
    }

    /// <summary>
    /// Creates a new sticky note.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Note> Post([FromBody] CreateNote createNote)
    {
        var note = new Note
        {
            Content = createNote.Content,
        };

        _database.Add(note);
        _database.SaveChanges();

        return CreatedAtRoute("GetNoteById", new { noteId = note.Id }, note);
    }

    /// <summary>
    /// Retrieves a single sticky note.
    /// </summary>
    [HttpGet("{noteId}", Name = "GetNoteById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<Note> Get([FromRoute] int noteId)
    {
        var note = _database.Notes.Find(noteId);
        if (note == null)
        {
            return NotFound($"Note with noteId {noteId} not found");
        }

        return Ok(note);
    }

    /// <summary>
    /// Updates part of a single sticky note.
    /// </summary>
    [HttpPatch("{noteId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Note> Patch([FromRoute] int noteId, [FromBody] UpdateNote patchNote)
    {
        var note = _database.Notes.Find(noteId);
        if (note == null)
        {
            return NotFound($"Note with noteId {noteId} not found");
        }

        note.Content = patchNote.Content;
        _database.SaveChanges();

        return Ok(note);
    }

    /// <summary>
    /// Deletes a single sticky note.
    /// </summary>
    [HttpDelete("{noteId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Note> Delete([FromRoute] int noteId)
    {
        var note = _database.Notes.Find(noteId);
        if (note == null)
        {
            return NotFound($"Note with noteId {noteId} not found");
        }

        _database.Notes.Remove(note);
        _database.SaveChanges();

        return Ok();
    }
}