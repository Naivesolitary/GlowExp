module.exports = {
    content: [
        './**/*.html',
        './**/*.razor',
        './Pages/**/*.razor',
        './Components/**/*.razor',
        './Shared/**/*.razor',
        './**/*.cshtml'
    ],
    theme: {
        extend: {
            fontFamily: {
                grotesk: ['"Space Grotesk"', 'sans-serif']
            }
        },
        plugins: [],
    }
}