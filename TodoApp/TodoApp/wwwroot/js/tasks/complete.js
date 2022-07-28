let completeButtons = document.querySelectorAll('.action-btn-complete');

const API_COMPLETE = 'https://localhost:7193/tasks/complete';

const completeTask = async taskId => {
    const request = `${API_COMPLETE}/${taskId}`;
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
        let taskId = (Number)(btn.classList[2]);
        let result = await completeTask(taskId);

        if (result) {
            location.reload();
        } else {
            console.error('ocurred an error while you marked as completed one task');
        }
    });
});