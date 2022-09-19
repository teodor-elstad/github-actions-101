//////////////////////////////
// HTTP Utilities

const getText = (url, defaultValue, success) => {
    var request = new XMLHttpRequest();
    request.open('GET', url, true);
    request.onload = () => {
        if (request.status >= 200 && request.status < 400) {
            success(request.response);
        } else {
            console.log(`Call to ${url} failed`);
            alert('Call to Notes.Api failed');
            success(defaultValue);
        }
    };

    request.onerror = () => {
        console.log(`Call to ${url} failed`);
        alert('Call to Notes.Api failed');
        success(defaultValue);
    };

    request.send();
};

const get = (url, defaultValue, success) => getText(url, defaultValue, (json) => success(JSON.parse(json)));

const post = (url, data, success, error) => {
    var request = new XMLHttpRequest();
    request.open('POST', url, true);
    request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

    request.onload = () => {
        if (request.status >= 200 && request.status < 400) {
            success(JSON.parse(request.response));
        } else if (error) {
            error(request.response);
        } else {
            console.log(`Call to ${url} failed`);
            alert('Call to Notes.Api failed');
        }
    };

    request.onerror = () => {
        console.log(`Call to ${url} failed`);
        alert('Call to Notes.Api failed');
    };

    request.send(JSON.stringify(data));
};

const patch = (url, data, success) => {
    var request = new XMLHttpRequest();
    request.open('PATCH', url, true);
    request.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

    request.onload = () => {
        if (request.status >= 200 && request.status < 400) {
            success(JSON.parse(request.response));
        } else {
            console.log(`Call to ${url} failed`);
            alert('Call to Notes.Api failed');
        }
    };

    request.onerror = () => {
        console.log(`Call to ${url} failed`);
        alert('Call to Notes.Api failed');
    };

    request.send(JSON.stringify(data));
};

const remove = (url, success) => {
    var request = new XMLHttpRequest();
    request.open('DELETE', url, true);

    request.onload = () => {
        if (request.status >= 200 && request.status < 400) {
            success();
        } else {
            console.log(`Call to ${url} failed`);
            alert('Call to Notes.Api failed');
        }
    };

    request.onerror = () => {
        console.log(`Call to ${url} failed`);
        alert('Call to Notes.Api failed');
    };

    request.send();
};

//////////////////////////////
// API Client

const ping = () => getText('/ping', 'NOT PONG', console.log);

const getNotes = (success) => get('/notes', '[]', success);

const createNote = (note, success) => post('/notes', note, success);

const getNote = (noteId, success) => get(`/notes/${noteId}`, '{}', success);

const updateNote = (noteId, content, success) => patch(`/notes/${noteId}`, { content: content }, success);

const deleteNote = (noteId, success) => remove(`/notes/${noteId}`, success);

//////////////////////////////
// Rendering Notes

const notesList = document.getElementById('notesList');

const noteListItem = note => {
    var content = document.createElement('div');
    content.setAttribute('class', 'note-content');
    content.innerHTML = note.content;

    var button = document.createElement('button');
    button.setAttribute('type', 'button');
    button.textContent = 'X';
    button.addEventListener('click', () =>
        deleteNote(
            note.id,
            () => drawAllNotes()), false);

    var item = document.createElement('li');
    item.appendChild(content);
    item.appendChild(button);

    return item;
}

const newNoteListItem = () => {
    var textarea = document.createElement('textarea');

    var button = document.createElement('button');
    button.setAttribute('type', 'button');
    button.textContent = '✓';
    button.addEventListener('click', () =>
        createNote(
            { content: textarea.value },
            () => drawAllNotes()), false);

    var item = document.createElement('li');
    item.appendChild(textarea);
    item.appendChild(button);

    return item;
}

const drawAllNotes = () =>
    getNotes(notes => {
        const listItems = notes.map(note => noteListItem(note));
        listItems.push(newNoteListItem());

        notesList.innerHTML = null;
        listItems.forEach(item => {
            notesList.appendChild(item);
        })
    });

drawAllNotes();