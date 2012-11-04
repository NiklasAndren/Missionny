$(function () {
    for (var x = 1; x < 4; x++) {
        var t = "#news-" + x;
        var textToHide = $(t).text().substring(100);
        $(t).text().substring(101);
        var visibleText = $(t).text().substring(1, 230);
        var link = $(t).closest('article').find('h2').find('a').attr('href');
        $(t)
            .html(visibleText + ('<span>' + textToHide + '</span>'))
            .append('<a href ="' + link + '" id="read-more" title="Read More" style="display: block; cursor: pointer;">Läs mer&hellip;</a>');
        $(t + " span").hide();
    }
});