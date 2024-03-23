"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadDepartments", function () {
    console.log("bruh");
    location.href = '/Departments/Manage';
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});