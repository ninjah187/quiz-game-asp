$("#category-container").show("slow");

$(".category-button").click(function () {
    $(this).parent().parent().hide("slow");
    var category = $(this).text();
    setTimeout(function () {
        window.location = "/Game/Questions/" + category
    }, 500);
});