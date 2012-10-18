jQuery(document).ready(function () {

    $(".comments").click(function () {

        if ($(this).parent().next().hasClass('selected')) {
            $(this).parent().next().removeClass('selected').slideToggle('fast', function () { });
        }
        else {
            $(this).parent().next().addClass('selected').slideToggle('fast', function () { });
        }


    });

});