
$(".toggleLoader").on('click', function () {
    $('#loader').show();
    $('#overlay-content').show();
});

$(document).ready(function () {
    $('#fadein').hide();
});

$(".fadein").hide().toggle({ effect: "scale", direction: "horizontal", duration: 800 });