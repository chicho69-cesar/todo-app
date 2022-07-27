const deleteButtons = document.querySelectorAll('.action-btn-delete');
const modal = document.querySelector('.modal');
const value = document.querySelector('.modal-value');
const confirmDelete = document.querySelector('.modal-delete');
const cancelDelete = document.querySelector('.modal-cancel');

const API_EXIT = 'https://localhost:7193/groups/exit';

const exitGroup = async groupId => {
    const request = `${API_EXIT}/${groupId}`;
    const options = {
        method: 'DELETE',
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

deleteButtons.forEach(btn => {
    btn.addEventListener('click', () => {
        let oldValue = value.classList[1];
        value.classList.replace(`${oldValue}`, btn.classList[2]);
        modal.classList.replace('hidde', 'show');
    });
});

cancelDelete.addEventListener('click', () => {
    modal.classList.replace('show', 'hidde');
});

confirmDelete.addEventListener('click', async () => {
    let groupId = (Number)(value.classList[1]);
    let result = await exitGroup(groupId);

    if (result) {
        let exitedGroups = document.querySelectorAll('.group');
        for (const deleteGroup of exitedGroups) {
            if ((Number)(deleteGroup.classList[1]) === groupId) {
                deleteGroup.remove();
                break;
            }
        }
    } else {
        console.error('ocurred an error while you marked as deleted this task');
    }

    modal.classList.replace('show', 'hidde');
});