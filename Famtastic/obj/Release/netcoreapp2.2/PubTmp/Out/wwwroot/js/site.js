let deferredPrompt;

window.addEventListener('beforeinstallprompt', (e) => {
    // Prevent Chrome 67 and earlier from automatically showing the prompt
    e.preventDefault();
    // Stash the event so it can be triggered later.
    deferredPrompt = e;
});

async function install() {
    if (deferredPrompt) {
        deferredPrompt.prompt();
        console.log(deferredPrompt);
        deferredPrompt.userChoice.then(function (choiceResult) {
            if (choiceResult.outcome === 'accepted') {
                alert('Congrats! Famtastic has been installed');
            } else {
                alert('Maybe next time :-)');
            }
            deferredPrompt = null;
        });
    }
}

function ClearFamilyNotification() {
    var messageToClearById = document.getElementById("notifyFam");
    messageToClearById.hidden = true;
}

function ClearPersonalNotification() {
    var messageToClearById = document.getElementById("notifyPersonal");
    messageToClearById.hidden = true;
}

function ClearSuccessNotification() {
    var messageToClearById = document.getElementById("notifySuccess");
    messageToClearById.hidden = true;
}

function ClearWarningNotification() {
    var messageToClearById = document.getElementById("notifyWarning");
    messageToClearById.hidden = true;
}

function CheckChangedEvent(id1, idSelect) {
    var mySelectedState = document.getElementById(idSelect).checked;
    
    var itemToChange = document.getElementById(id1);
    console.log(id1);
    
    if (mySelectedState === true) {
        itemToChange.style.display = "block";
    }

    if (mySelectedState === false) {
        itemToChange.style.display = "none";
    }

    console.log(mySelectedState);
}

function swapElements(id1, id2, idSelect) {
    console.log(id1 + ' ' + id2 + ' ' + idSelect);
    var mySelectedState = document.getElementById(idSelect).checked;
    var btnOp1 = document.getElementById(id1);
    var btnOp2 = document.getElementById(id2);
    
    if (mySelectedState === true) {
        btnOp2.style.display = "block";
        btnOp1.style.display = "none";
    }
    if (mySelectedState === false) {
        btnOp1.style.display = "block";
        btnOp2.style.display = "none";
    }

    console.log(mySelectedState);
}