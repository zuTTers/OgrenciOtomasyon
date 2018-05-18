var taskObject = { taskUser: [] };

$("#btnTaskManSave").click(function () {

    taskObject.AId = $("#txtAId").val();
    taskObject.SId = $("#txtSId").val();
    taskObject.Title = $("#txtTitle").val();
    taskObject.Description = $("#txtDescription").val();
    taskObject.Date = $("#txtDate").val();
    taskObject.Type = $("#txtType").val();
    taskObject.CreatedDate = $("#txtCreatedDate").val();
    taskObject.CreatedUser = $("#txtCreatedUser").val();
    taskObject.IsActive = true;
    taskObject.IsDeleted = false;

    if ($("#txtSId").val() != "" && $("#txtTitle").val() != "" && $("#txtDate").val() != "") {
        $("#btnTaskManSave").attr("disabled", "disabled");

        $.post("TaskManSave", { taskManagerDetail: taskObject }, function (data, status) {
            if (data.success == true) {
                Bildirim(data.message);
                setTimeout(function () {
                    location.href = "/Home/TaskManager";
                }, 3000);
            }
            else {
                $("#btnTaskManSave").removeAttr("disabled");
                if (data.requiredLogin) {
                    Bildirim(data.message);
                    setTimeout(function () { location.href = "/Login/Index"; }, 2000);
                }
                else {
                    Bildirim(data.message);
                }
            }
        });
    }
    else {
        HataBildirim();
    }
});


function GetTaskMan(taskid) {
    //var date = new Date('2016-01-01 00:00:00');

    $.post("TaskManGet", {
        TaskID: taskid
    },
        function (data, status) {

            taskObject = data;

            $("#txtAId").val(taskObject.AId);
            $("#txtSId").val(taskObject.SId);
            $("#txtTitle").val(taskObject.Title);
            $("#txtDescription").val(taskObject.Description);
            $("#txtDate").val(jsDate(taskObject.Date));
            $("#txtType").val(taskObject.Type);
            $("#txtCreatedDate").val(taskObject.CreatedDate);
            $("#txtCreatedUser").val(taskObject.CreatedUser);

        });
}

function GetData() {
    $.get("GetAllTask",
        function (data, status) {
            taskObject.taskUser.push(data);
        });
}



$("#txt1").click(function () {
    GetTaskMan($("#txt11").val());
});

$("#txt2").click(function () {
    GetTaskMan($("#txt22").val());
});

$("#txt3").click(function () {
    GetTaskMan($("#txt33").val());
});

$("#txt4").click(function () {
    GetTaskMan($("#txt44").val());
});

$("#txt5").click(function () {
    GetTaskMan($("#txt55").val());
});

$("#txt6").click(function () {
    GetTaskMan($("#txt66").val());
});

$("#txt7").click(function () {
    GetTaskMan($("#txt77").val());
});

$("#txt8").click(function () {
    GetTaskMan($("#txt88").val());
});

$("#txt9").click(function () {
    GetTaskMan($("#txt99").val());
});

$("#txt10").click(function () {
    GetTaskMan($("#txt1010").val());
});

$("#txt11").click(function () {
    GetTaskMan($("#txt1111").val());
});

$("#txt12").click(function () {
    GetTaskMan($("#txt1212").val());
});

$("#txt13").click(function () {
    GetTaskMan($("#txt1313").val());
});

$("#txt14").click(function () {
    GetTaskMan($("#txt1414").val());
});

$("#txt15").click(function () {
    GetTaskMan($("#txt1515").val());
});




////////////////////////////////////////////////////ÜÇ GÜNÜN KAYITLI VERİLERİNİ GETİR////////////////////////////////////////////////////

//Bugün ve saatleri
var n11Date = new Date();
var n22Date = new Date();
var n33Date = new Date();
var n44Date = new Date();
var n55Date = new Date();
var n1Date = $.datepicker.formatDate('yy-dd-mm 08:00:00', n11Date);
var n2Date = $.datepicker.formatDate('yy-dd-mm 10:00:00', n22Date);
var n3Date = $.datepicker.formatDate('yy-dd-mm 13:00:00', n33Date);
var n4Date = $.datepicker.formatDate('yy-dd-mm 15:00:00', n44Date);
var n5Date = $.datepicker.formatDate('yy-dd-mm 17:00:00', n55Date);

//Yarın 
var mDate = new Date();
var m1Date = mDate.setDate(mDate.getDate() + 1);
var m2Date = mDate.getFullYear() + '-' + ("0" + mDate.getDate()).slice(-2) + '-' + ("0" + (mDate.getMonth() + 1)).slice(-2);
var m3Date = m2Date + " " + "08:00:00";
var m4Date = m2Date + " " + "10:00:00";
var m5Date = m2Date + " " + "13:00:00";
var m6Date = m2Date + " " + "15:00:00";
var m7Date = m2Date + " " + "17:00:00";


//Ertesigün
var kDate = new Date();
var k1Date = kDate.setDate(kDate.getDate() + 2);
var k2Date = kDate.getFullYear() + '-' + ("0" + kDate.getDate()).slice(-2) + '-' + ("0" + (kDate.getMonth() + 1)).slice(-2);
var k3Date = k2Date + " " + "08:00:00";
var k4Date = k2Date + " " + "10:00:00";
var k5Date = k2Date + " " + "13:00:00";
var k6Date = k2Date + " " + "15:00:00";
var k7Date = k2Date + " " + "17:00:00";


DOLDUR();

function DOLDUR() {
    $.get("GetAllTask",
        function (data, status) {
            taskObject.taskUser.push(data);

            for (var i = 0; i < taskObject.taskUser[0].length; i++) {

                //Gelen data formatlama ayarları
                var x11Date = jsDate(taskObject.taskUser[0][i].Date);
                var x1Date = $.datepicker.formatDate('yy-dd-mm', x11Date);
                var myTime = x11Date.toTimeString().replace(/.*(\d{2}:\d{2}:\d{2}).*/, "$1");
                var fulldate = x1Date + " " + myTime;

                //Bugün kolonu

                //1
                if ($("#txt1").val() != "Boş") {
                    $("#txt1").val($("#txt1").val());
                }
                else {
                    if (fulldate == n1Date) {
                        $("#txt1").val(taskObject.taskUser[0][i].SName);
                        $("#txt11").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt1").val("Boş");
                    }

                }

                //2
                if ($("#txt4").val() != "Boş") {
                    $("#txt4").val($("#txt4").val());
                }
                else {
                    if (fulldate == n2Date) {
                        $("#txt4").val(taskObject.taskUser[0][i].SName);
                        $("#txt44").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt4").val("Boş");
                    }

                }

                //3
                if ($("#txt7").val() != "Boş") {
                    $("#txt7").val($("#txt7").val());
                }
                else {
                    if (fulldate == n3Date) {
                        $("#txt7").val(taskObject.taskUser[0][i].SName);
                        $("#txt77").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt7").val("Boş");
                    }

                }

                //4
                if ($("#txt10").val() != "Boş") {
                    $("#txt10").val($("#txt10").val());
                }
                else {
                    if (fulldate == n4Date) {
                        $("#txt10").val(taskObject.taskUser[0][i].SName);
                        $("#txt1010").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt10").val("Boş");
                    }

                }

                //5
                if ($("#txt13").val() != "Boş") {
                    $("#txt13").val($("#txt13").val());
                }
                else {
                    if (fulldate == n5Date) {
                        $("#txt13").val(taskObject.taskUser[0][i].SName);
                        $("#txt1313").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt13").val("Boş");
                    }

                }

                //Yarın kolonu

                //1
                if ($("#txt2").val() != "Boş") {
                    $("#txt2").val($("#txt2").val());
                }
                else {
                    if (fulldate == m3Date) {
                        $("#txt2").val(taskObject.taskUser[0][i].SName);
                        $("#txt22").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt2").val("Boş");
                    }

                }

                //2
                if ($("#txt5").val() != "Boş") {
                    $("#txt5").val($("#txt5").val());
                }
                else {
                    if (fulldate == m4Date) {
                        $("#txt5").val(taskObject.taskUser[0][i].SName);
                        $("#txt55").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt5").val("Boş");
                    }

                }

                //3
                if ($("#txt8").val() != "Boş") {
                    $("#txt8").val($("#txt8").val());
                }
                else {
                    if (fulldate == m5Date) {
                        $("#txt8").val(taskObject.taskUser[0][i].SName);
                        $("#txt88").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt8").val("Boş");
                    }

                }

                //4
                if ($("#txt11").val() != "Boş") {
                    $("#txt11").val($("#txt11").val());
                }
                else {
                    if (fulldate == m6Date) {
                        $("#txt11").val(taskObject.taskUser[0][i].SName);
                        $("#txt1111").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt11").val("Boş");
                    }

                }

                //5
                if ($("#txt14").val() != "Boş") {
                    $("#txt14").val($("#txt14").val());
                }
                else {
                    if (fulldate == m7Date) {
                        $("#txt14").val(taskObject.taskUser[0][i].SName);
                        $("#txt1414").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt14").val("Boş");
                    }

                }

                ////Ertesigün kolonu

                //1
                if ($("#txt3").val() != "Boş") {
                    $("#txt3").val($("#txt3").val());
                }
                else {
                    if (fulldate == k3Date) {
                        $("#txt3").val(taskObject.taskUser[0][i].SName);
                        $("#txt33").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt3").val("Boş");
                    }
                }

                //2
                if ($("#txt6").val() != "Boş") {
                    $("#txt6").val($("#txt6").val());
                }
                else {
                    if (fulldate == k4Date) {
                        $("#txt6").val(taskObject.taskUser[0][i].SName);
                        $("#txt66").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt6").val("Boş");
                    }
                }

                //3
                if ($("#txt9").val() != "Boş") {
                    $("#txt9").val($("#txt9").val());
                }
                else {
                    if (fulldate == k5Date) {
                        $("#txt9").val(taskObject.taskUser[0][i].SName);
                        $("#txt99").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt9").val("Boş");
                    }
                }

                //4
                if ($("#txt12").val() != "Boş") {
                    $("#txt12").val($("#txt12").val());
                }
                else {
                    if (fulldate == k6Date) {
                        $("#txt12").val(taskObject.taskUser[0][i].SName);
                        $("#txt1212").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt12").val("Boş");
                    }
                }

                //5
                if ($("#txt15").val() != "Boş") {
                    $("#txt15").val($("#txt15").val());
                }
                else {
                    if (fulldate == k7Date) {
                        $("#txt15").val(taskObject.taskUser[0][i].SName);
                        $("#txt1515").val(taskObject.taskUser[0][i].TaskID);
                    }
                    else {
                        $("#txt15").val("Boş");
                    }
                }

            }
        });
}



