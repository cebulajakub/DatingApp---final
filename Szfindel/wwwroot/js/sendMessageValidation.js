    $(document).ready(function () {
        // Ustawienie początkowego wyglądu przycisku
        updateButtonState();

    // Obsługa zdarzenia wysyłania formularza
    $('#sendMessageForm').submit(function (event) {
            var text = $('#text').val().trim();
    if (text === '') {
        alert('Treść wiadomości jest wymagana.');
    event.preventDefault(); // Zapobiega wysłaniu formularza, jeśli pole jest puste
            }
        });

    // Obsługa zdarzenia zmiany zawartości pola "Treść wiadomości"
    $('#text').on('input', function () {
        updateButtonState();
        });

    // Funkcja aktualizująca wygląd przycisku na podstawie zawartości pola "Treść wiadomości"
    function updateButtonState() {
            var text = $('#text').val().trim();
    var button = $('#sendMessageForm button[type="submit"]');

    if (text === '') {
        button.css('background-color', 'gray'); // Ustawienie koloru szarego, gdy pole jest puste
    button.prop('disabled', true); // Wyłączenie możliwości kliknięcia
            } else {
        button.css('background-color', 'blue'); // Ustawienie koloru niebieskiego, gdy pole nie jest puste
    button.prop('disabled', false); // Włączenie możliwości kliknięcia
            }
        }
    });
