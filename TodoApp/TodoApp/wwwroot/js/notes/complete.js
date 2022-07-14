let completeButtons = document.querySelectorAll('.action-btn-complete');

const API_COMPLETE = 'https://localhost:7193/notes/complete';

const completeNote = async noteId => {
    const request = `${API_COMPLETE}/${noteId}`;
    const options = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    };

    try {
        const response = await fetch(request, options);
        return (response.status === 200);
    } catch (error) {
        return false;
    }
}

completeButtons.forEach(btn => {
    btn.addEventListener('click', async () => {
        let noteId = (Number)(btn.classList[2]);
        console.log(noteId);
        let result = await completeNote(noteId);

        if (result) {
            location.reload();
        } else {
            console.error('ocurred an error while you marked as completed one task');
        }
    });
});