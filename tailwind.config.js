/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Web/Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                'jumbo-yellow': '#ffcc02',
            }
        },
    },
    safelist: [
        'bg-error',
        'bg-warning',
        'bg-success',
    ],
    plugins: [require("daisyui")],
    daisyui: {
        themes: ["emerald"]
    }
}

