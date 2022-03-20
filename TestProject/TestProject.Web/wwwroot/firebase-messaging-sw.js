importScripts('https://www.gstatic.com/firebasejs/9.1.1/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/9.1.1/firebase-messaging.js');

var config = {
    apiKey: "AIzaSyAMkLuVd8jrGja6U3659-Rd8R-TYdyGY28",
    authDomain: "cmcweb-d4dfb.firebaseapp.com",
    projectId: "cmcweb-d4dfb",
    storageBucket: "cmcweb-d4dfb.appspot.com",
    messagingSenderId: "694837216123",
    appId: "1:694837216123:web:3cfd1efb245927184c73de",
    measurementId: "G-M04LWSW6KW"
};

firebase.initializeApp(config);

const messaging = firebase.messaging();

messaging.setBackgroundMessageHandler(function(payload) {
    //// Customize notification here
    var notificationTitle = 'My Titile';
    var notificationOptions = {
        body: payload.data.body
    };

    return self.registration.showNotification(notificationTitle,
        notificationOptions);
});