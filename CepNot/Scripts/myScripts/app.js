/*Bildirim yapılacak text bu fonksiyona gelir.*/
function Bildirim(text) {
    $.notify({
        icon: 'fa fa-info',
        message: text
    },
        {
            type: 'info',
            timer: 5000
        });
}
/*Hata Bildirimi yapar.*/
function HataBildirim() {
    $.notify({
        icon: 'fa fa-warning',
        message: 'Bilgilerinizi kontrol ediniz!'
    },
        {
            type: 'danger',
            timer: 5000
        });
}



$("#uyeeklepanel").hide();

$("#rdbk").click(function () {
    $("#uyeeklepanel").hide();
});

$("#rdby").click(function () {
    $("#uyeeklepanel").fadeIn(1100);
    window.location = '#uyeeklepanel';
}); 

$("#hCalendar").datepicker({
    nextText: "Sonra",
    prevText: "Önce",
    inline: true,
    dateFormat: "dd.mm.yy",
    monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
    dayNamesMin: ["Pzr", "Pts", "Sl", "Çrş", "Prş", "Cm", "Cts"]
});



  





