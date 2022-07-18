const deleteButtons = document.querySelectorAll('.action-btn-delete');
const modal = document.querySelector('.modal');
const value = document.querySelector('.modal-value');
const confirmDelete = document.querySelector('.modal-delete');
const cancelDelete = document.querySelector('.modal-cancel');

const API_DELETE = 'https://localhost:7193/notes/delete';

const deleteNote = async noteId => {
    const request = `${API_DELETE}/${noteId}`;
    const options = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    };

    try {
        const response = await fetch(request, options);
        return (response.status === 200);
    } catch(error) {
        return false;
    }
}

deleteButtons.forEach(btn => {
    btn.addEventListener('click', () => {
        console.log(btn);
        let oldValue = value.classList[1];
        value.classList.replace(`${oldValue}`, btn.classList[2]);
        modal.classList.replace('hidde', 'show');
    });
});

cancelDelete.addEventListener('click', () => {
    modal.classList.replace('show', 'hidde');
});

confirmDelete.addEventListener('click', async () => {
    let noteId = (Number)(value.classList[1]);
    let result = await deleteNote(noteId);

    if (result) {
        let deleteNotes = document.querySelectorAll('.note');
        for (const deleteNote of deleteNotes) {
            if ((Number)(deleteNote.classList[1]) === noteId) {
                deleteNote.remove();
            }
        }
    } else {
        console.error('ocurred an error while you marked as deleted this task');
    }

    modal.classList.replace('show', 'hidde');
});