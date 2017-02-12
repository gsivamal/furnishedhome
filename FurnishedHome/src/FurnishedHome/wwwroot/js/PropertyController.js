﻿var app = angular.module("app", []);

app.controller("PropertyController", function ($scope) {

});
var map;
function loadMapScenario() {
    map = new Microsoft.Maps.Map(document.getElementById('bingMap'), {
        credentials: "Al3nAvLTiRL6-UDLFNLk2hA4bpQ6RoTrigdFz1M5t-H2g7vFZxkSCo57_VREL56v",//'Your Bing Maps Key'
        maxZoom: 15,
        minZoom: 12,
        center: new Microsoft.Maps.Location(32.779891, -96.798563)
    });

    $.ajax({
        url: "/GetAllProperties",
        type: "post",
        dataType: "json",
        contentType: "application/json",
        success: function (properties) {
            $.each(properties, function (key, value) {
                AddNewMarker(value["id"], value["latitude"], value["longitude"], value["price"]);
            });
        }
    });
}

function AddNewMarker(PropId, lat, lng, price) {

    var contentString = '<div id="content">' +
        '<img src="/images/' + PropId + '_primary.jpg" style="width:280px; height:210px;" alt="La Estancia">' +
        '<p class="" style="position: relative; top: -20px; color: snow;"><strong>&nbsp;&nbsp;$' + price + '/month</strong></p>' +
        '<a href="/properties/' + PropId + '/" style="position: absolute; right: 0px;">More info</a></div>';

    var location = new Microsoft.Maps.Location(lat, lng);
    var marker = new Microsoft.Maps.Pushpin(location, null);
    var infobox = new Microsoft.Maps.Infobox(location, {
        //title: 'Map Center',
        //description: contentString,
        htmlContent: contentString,
        visible: false
    });
    infobox.setMap(map);
    Microsoft.Maps.Events.addHandler(marker, 'click', function () {
        infobox.setOptions({ visible: true });
    });
    Microsoft.Maps.Events.addHandler(marker, 'mouseout', function () {
        infobox.setOptions({ visible: false });
    });
    map.entities.push(marker);
}