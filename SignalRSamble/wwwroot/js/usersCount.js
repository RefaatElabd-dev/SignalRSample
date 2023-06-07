//create a connection
var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();
// Choose Http Transport type
// types:
//  HttpTransportType.None
//  HttpTransportType.WebSocket
//  HttpTransportType.ServerSentEvents
//  HttpTransportType.LongPolling

//var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount", signalR.HttpTransportType.LongPolling).build();

//connect to methods that hub invokes aka receive notificationsfrom hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerHTML = value.toString();
})

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerHTML = value.toString();
})
//invoke hub methods akasend notification to hub
function newWindowLoadedOnClient() {
    //use send if there is no return type to use.
    //connectionUserCount.send("NewPageLoaded");
    //use invoke if there is a return type.
    connectionUserCount.invoke("NewPageLoaded").then(value => console.log(value));
}

function fullfilled() {
    //Do something on Start connection
    console.log("Connection has established successfully");
    newWindowLoadedOnClient();
}

function reject() {
    //Do something on connection failed
    console.log("Connection has not established successfully")
}

//start connection
connectionUserCount.start().then(fullfilled, reject);
