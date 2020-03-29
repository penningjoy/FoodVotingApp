"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/VoterHub")
                      .configureLogging(signalR.LogLevel.Information).build();


//Disable the buttons until connection is established
disablevoting();


var votetable = document.getElementById("votetable");

connection.on("VoteReceived", function (item, votes) {
    switch (item) {
        case "Biriyani":
            votetable.rows[1].cells[1].innerHTML = votes;
            break;
        case "Polao":
            votetable.rows[1].cells[2].innerHTML = votes;
            break;
        case "Goat Curry Rice":
            votetable.rows[1].cells[3].innerHTML = votes;  
            break;

    }
    document.getElementById("messagesList").appendChild(li);
});

/* -- Starts a connection */
connection.start().then(function () {
    enablevoting();
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("biriyani").addEventListener("click", function (event) {
    var item = "Biriyani";
    var increment = 1;
    connection.invoke("Vote", item, increment).catch(function (err) {   
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("polao").addEventListener("click", function (event) {
    var item = "Polao";
    var increment = 1;
    connection.invoke("Vote", item, increment).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("curry").addEventListener("click", function (event) {
    var item = "Goat Curry Rice";
    var increment = 1;
    connection.invoke("Vote", item, increment).catch(function (err) {
       return console.error(err.toString());
    });
    event.preventDefault();
});

function disablevoting() {
    document.getElementById("biriyani").disabled = true;
    document.getElementById("polao").disabled = true;
    document.getElementById("curry").disabled = true;
}

function enablevoting() {
    document.getElementById("biriyani").disabled = false;
    document.getElementById("polao").disabled = false;
    document.getElementById("curry").disabled = false;
}