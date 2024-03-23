"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadDepartments", function () {
    location.href = '/Departments/Manage/Index'
});



connection.start().catch(function (err) {
    return console.error(err.toString());
});