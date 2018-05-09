var taskObject = {};

$("#btnTaskTime1").click(function () {
    $("#txtDate").val($("#txtDatetime1").val());
});
$("#btnTaskTime2").click(function () {
    $("#txtDate").val($("#txtDatetime2").val());
});
$("#btnTaskTime3").click(function () {
    $("#txtDate").val($("#txtDatetime3").val());
});
$("#btnTaskTime4").click(function () {
    $("#txtDate").val($("#txtDatetime4").val());
});
$("#btnTaskTime5").click(function () {
    $("#txtDate").val($("#txtDatetime5").val());
});
$("#btnTaskTime6").click(function () {
    $("#txtDate").val($("#txtDatetime6").val());
});


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

    if ($("#txtSId") !== "" && $("#txtTitle") !== "" && $("#txtDate") !== "") {
        $("#btnTaskManSave").attr("disabled", "disabled");

        $.post("Home/TaskManSave", { taskManagerDetail: taskObject }, function (data, status) {
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


function GetTaskMan(dateId) {

    $.post("Home/TaskManGet", {
        Date: dateId
    },
        function (data,status) {

        taskObject = data;

        $("#txtAId").val(taskObject.AId);
        $("#txtSId").val(taskObject.SId);
        $("#txtTitle").val(taskObject.Title);
        $("#txtDescription").val(taskObject.Description);
        $("#txtDate").val(taskObject.Date);
        $("#txtType").val(taskObject.Type);
        $("#txtCreatedDate").val(taskObject.CreatedDate);
        $("#txtCreatedUser").val(taskObject.CreatedUser);

    });
}