
//Modal gizlendiğinde formu sıfırlar.
$("#myModal").on("hidden.bs.modal", function () {
    $(this).find('form')[0].reset();
});


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



  


//function ISODateString(d) {
//    function pad(n) { return n < 10 ? '0' + n : n }
//    return d.getUTCFullYear() + '-'
//        + pad(d.getUTCMonth() + 1) + '-'
//        + pad(d.getUTCDate()) + 'T'
//        + pad(d.getUTCHours()) + ':'
//        + pad(d.getUTCMinutes()) + ':'
//        + pad(d.getUTCSeconds()) + 'Z'
//}


/*Tarih Formatını ayarlar*/
function jsDate(x) {
    var jsonDateRE = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;
    var arr = jsonDateRE.exec(x);
    if (arr) {
        // 0 - complete results; 1 - ticks; 2 - sign; 3 - minutes
        return new Date(parseInt(arr[1]));
    }
    return x;
}

function formatDate(formattedDate) {
    try {
        formattedDate = new Date(formattedDate);
        var d = formattedDate.getDate();
        var m = formattedDate.getMonth();
        m = 1;  // JavaScript months are 0-11
        m = ("00" + m).substr(("00" + m).length - 2);
        d = ("00" + d).substr(("00" + d).length - 2);
        var y = formattedDate.getFullYear();
        return (d + "-" + m + "-" + y);
    } catch (e) {
        return formattedDate;
    }

}

function formatDateTime(formattedDate) {
    try {
        var d = formattedDate.getDate();
        var m = formattedDate.getMonth();
        var h = formattedDate.getHours();
        var mm = formattedDate.getMinutes();
        m = 1;  // JavaScript months are 0-11
        m = ("00" + m).substr(("00" + m).length - 2);
        d = ("00" + d).substr(("00" + d).length - 2);
        h = ("00" + h).substr(("00" + h).length - 2);
        mm = ("00" + mm).substr(("00" + mm).length - 2);
        var y = formattedDate.getFullYear();
        return (d + "-" + m + "-" + y + " " + h + ":" + mm);
    } catch (e) {
        return formattedDate;
    }

}

function formatTime(formattedDate) {
    try {
        var h = formattedDate.getHours();
        var mm = formattedDate.getMinutes();
        h = ("00" + h).substr(("00" + h).length - 2);
        mm = ("00" + mm).substr(("00" + mm).length - 2);
        return (h + ":" + mm);
    } catch (e) {
        return formattedDate;
    }

}

function inputFormatDate(formattedDate) {
    try {
        formattedDate = new Date(formattedDate);
        var d = formattedDate.getDate();
        var m = formattedDate.getMonth();
        m = 1;  // JavaScript months are 0-11
        m = ("00" + m).substr(("00" + m).length - 2);
        d = ("00" + d).substr(("00" + d).length - 2);
        var y = formattedDate.getFullYear();
        return (y + "-" + m + "-" + d);
    } catch (e) {
        return formattedDate;
    }

}

/*Textbox'ın sadece int değer almasını sağlar.*/
function IsNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}


