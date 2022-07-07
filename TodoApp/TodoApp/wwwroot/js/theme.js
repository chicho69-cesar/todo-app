const btnSwitch = document.querySelector('.switch-theme');

setTheme = () => {
    if (localStorage.getItem('theme') === null) {
        document.body.classList.add('light');
    } else {
        let isDarkTheme = localStorage.getItem('theme') === 'dark';
        document.body.classList.toggle('dark', isDarkTheme);
        document.body.classList.toggle('light', !isDarkTheme);

        btnSwitch.innerHTML = (localStorage.getItem('theme') === 'dark') 
            ? '<i class="bi bi-sun-fill"></i>'
            : '<i class="bi bi-moon-stars"></i>';
    }
}

btnSwitch.addEventListener('click', () => {
    if (document.body.classList.contains('dark')) {
        localStorage.setItem('theme', 'light');
    } else {
        localStorage.setItem('theme', 'dark');
    }

    setTheme();
});

setTheme();