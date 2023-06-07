var wandSpan = document.getElementById("wandCounter");
var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
 //create a connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder().withUrl("/hubs/deathlyHallows").build();



//connect to methods that hub invokes aka receive notificationsfrom hub
connectionDeathlyHallows.on("updateDeathlyHallowsCount", (wand, cloak, stone) => {
    console.log("whhhhat");
    wandSpan.innerHTML = wand.toString();
    cloakSpan.innerHTML = cloak.toString();
    stoneSpan.innerHTML = stone.toString();
})

function getRaceHallows() {
    connectionDeathlyHallows.invoke("getRaceHallows").then((dictionary) => {
        wandSpan.innerHTML = dictionary["wand"].toString();
        cloakSpan.innerHTML = dictionary["cloak"].toString();
        stoneSpan.innerHTML = dictionary["stone"].toString();
    })
}

function fullfilled() {
    //Do something on Start connection
    console.log("Connection to /hubs/deathlyHallows has established successfully");
    getRaceHallows();
}

function reject() {
    //Do something on connection failed
    console.log("Connection to /hubs/deathlyHallows has not established successfully XXXXXXX")
}

//start connection
connectionDeathlyHallows.start().then(fullfilled, reject);
