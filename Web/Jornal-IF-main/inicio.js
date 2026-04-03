let lastScroll = 0;
const navbar = document.querySelector('.navbar');

window.addEventListener('scroll', () => {
    const currentScroll = window.pageYOffset;

    if (currentScroll > lastScroll) {
        // descendo → fica transparente
        navbar.classList.add('off');
    } else {
        // subindo → fica visível
        navbar.classList.remove('off');
    }

    lastScroll = currentScroll;
});