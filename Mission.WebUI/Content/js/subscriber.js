jQuery(document).ready(function () {


    $('.subscribe-link').click(function () {

        if ($('#subscribe').css('display') == 'block') {
            $('#subscribe').css('display', 'none');
            $('#unsubscribe').css('display', 'block');
        }
        else 
        {
            $('#subscribe').css('display', 'block');
            $('#unsubscribe').css('display', 'none');
        }


    });
});