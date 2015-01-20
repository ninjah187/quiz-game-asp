$("#start.btn-big").click(function () {
    $(this).hide("slow");
    setTimeout(function () {
        $.ajax({
            url: "/Game/CategoryChoose/",
            dataType: "html",
            success: function (data) {
                $("#game-content").html(data);
                setTimeout($("#game-content").show("slow"), 500);
            }
        });
    }, 500);
});