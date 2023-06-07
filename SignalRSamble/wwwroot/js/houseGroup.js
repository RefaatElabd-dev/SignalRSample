let lbl_houseJoined = document.getElementById("lbl_houseJoined");


let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");
let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");


//create a connection
var connectionHouseGroups = new signalR.HubConnectionBuilder().withUrl("/hubs/houseGroup").build();



//triger Events
//subscribe
btn_gryffindor.addEventListener("click", event => {
    connectionHouseGroups.send("JoinHouse", "Gryffindor");
    event.preventDefault();
});

btn_slytherin.addEventListener("click", event => {
    connectionHouseGroups.send("JoinHouse", "Slytherin");
    event.preventDefault();
});

btn_hufflepuff.addEventListener("click", event => {
    connectionHouseGroups.send("JoinHouse", "Hufflepuff");
    event.preventDefault();
});

btn_ravenclaw.addEventListener("click", event => {
    connectionHouseGroups.send("JoinHouse", "Ravenclaw");
    event.preventDefault();
});

//unSubscribe
btn_un_gryffindor.addEventListener("click", event => {
    connectionHouseGroups.send("RemoveHouse", "Gryffindor");
    event.preventDefault();
});

btn_un_slytherin.addEventListener("click", event => {
    connectionHouseGroups.send("RemoveHouse", "Slytherin");
    event.preventDefault();
});

btn_un_hufflepuff.addEventListener("click", event => {
    connectionHouseGroups.send("RemoveHouse", "Hufflepuff");
    event.preventDefault();
});

btn_un_ravenclaw.addEventListener("click", event => {
    connectionHouseGroups.send("RemoveHouse", "Ravenclaw");
    event.preventDefault();
});

trigger_gryffindor.addEventListener("click", event => {
    connectionHouseGroups.send("TriggerHouseNotifications", "Gryffindor");
    event.preventDefault();
});

trigger_slytherin.addEventListener("click", event => {
    connectionHouseGroups.send("TriggerHouseNotifications", "Slytherin");
    event.preventDefault();
});

trigger_hufflepuff.addEventListener("click", event => {
    connectionHouseGroups.send("TriggerHouseNotifications", "Hufflepuff");
    event.preventDefault();
});

trigger_ravenclaw.addEventListener("click", event => {
    connectionHouseGroups.send("TriggerHouseNotifications", "Ravenclaw");
    event.preventDefault();
});


//connect to methods that hub invokes aka receive notificationsfrom hub

connectionHouseGroups.on("subscriptioStatue", (strJoinedGroups, houseName, hasSubscribed) => {
    lbl_houseJoined.innerHTML = strJoinedGroups;

    if (hasSubscribed) {
        switch (houseName) {
            case 'Gryffindor':
                btn_gryffindor.style.display = "none";
                btn_un_gryffindor.style.display = "";
                break;
            case 'Slytherin':
                btn_slytherin.style.display = "none";
                btn_un_slytherin.style.display = "";
                break;
            case 'Hufflepuff':
                btn_hufflepuff.style.display = "none";
                btn_un_hufflepuff.style.display = "";
                break;
            case 'Ravenclaw':
                btn_ravenclaw.style.display = "none";
                btn_un_ravenclaw.style.display = "";
                break;
            default:
                break;
        }
        toastr.success('You have been subscriped successfully to: ' + houseName);
    }
    else {
        switch (houseName) {
            case 'Gryffindor':
                btn_gryffindor.style.display = "";
                btn_un_gryffindor.style.display = "none";
                break;
            case 'Slytherin':
                btn_slytherin.style.display = "";
                btn_un_slytherin.style.display = "none";
                break;
            case 'Hufflepuff':
                btn_hufflepuff.style.display = "";
                btn_un_hufflepuff.style.display = "none";
                break;
            case 'Ravenclaw':
                btn_ravenclaw.style.display = "";
                btn_un_ravenclaw.style.display = "none";
                break;
            default:
                break;
        }
        toastr.warning('You have been unsubscriped successfully to: ' + houseName);
    }
});

connectionHouseGroups.on("newSubscriperAction", (alertMessage) => {
    toastr.info(alertMessage);
})

connectionHouseGroups.on("TriggerHouseNotifications", (houseName) => {
    toastr.info("New Nosification has been triggered for " + houseName);
})

function fullfilled() {
    //Do something on Start connection
    console.log("Connection to /hubs/deathlyHallows has established successfully");
}

function reject() {
    //Do something on connection failed
    console.log("Connection to /hubs/deathlyHallows has not established successfully XXXXXXX")
}

//start connection
connectionHouseGroups.start().then(fullfilled, reject);
