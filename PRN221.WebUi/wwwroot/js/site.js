﻿"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalRServer")
    .build();

connection.on("LoadAppointments", function () {
    location.href = '/Appointments'
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});
