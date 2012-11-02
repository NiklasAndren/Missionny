$(function () {
    for (var x = 1; x < 4; x++) {
        var t = "#news-" + x;
        var textToHide = $(t).text().substring(100);
        var visibleText = $(t).text().substring(1, 400);
        var link = $(t).closest('article').find('h2').find('a').attr('href');
        $(t)
            .html(visibleText + ('<span>' + textToHide + '</span>'))
            .append('<a href ="' + link + '" id="read-more" title="Read More" style="display: block; cursor: pointer;">Read More&hellip;</a>');


        $(t + " span").hide();
    }
});