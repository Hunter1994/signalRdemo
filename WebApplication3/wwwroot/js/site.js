"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7165/chatHub", { accessTokenFactory: () =>"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiemhhbmdzYW4iLCJleHAiOjE3MzkwODM5MDcsImF1ZCI6Ik91c3VfWGxkX1BsYXRmb3JtIn0.Et4tCzYS_0Q0ajOi29YWIEj6Fw-zciJoyQXjChhuxs8" })
    .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
    .build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});