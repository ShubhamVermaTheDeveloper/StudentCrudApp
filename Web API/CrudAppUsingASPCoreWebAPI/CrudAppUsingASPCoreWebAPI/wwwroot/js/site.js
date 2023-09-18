// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.noNumbersInput').on('keypress', function (e) {
        // Check if the pressed key is a number (0-9)
        if (e.which >= 48 && e.which <= 57) {
            e.preventDefault(); // Prevent the keypress
        }
    });
});