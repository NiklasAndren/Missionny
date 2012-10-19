jQuery(document).ready(function () {

    $(".comments").click(function () {

        if ($(this).parent().next().hasClass('selected')) {
            $(this).parent().next().removeClass('selected').slideToggle('fast', function () { });
        }
        else {
            $(this).parent().next().addClass('selected').slideToggle('fast', function () { });
        }


    });


    $(".createcommenttext").click(function () {

        if ($(this).next().hasClass('showinputs')) {
            $(this).next().removeClass('showinputs').slideToggle('fast', function () { });
            $(this).html('<a>Skriv egen kommentar</a>');
        }
        else {
            $(this).next().addClass('showinputs').slideToggle('fast', function () { });
            $(this).html('<a>Dölj</a>');
        }


    });
});