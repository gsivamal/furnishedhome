var map;
var _id;
var app = angular.module("app", []);

app.controller("PropertyController", function ($scope) {
    $scope.property;
    $scope.zipcode = "75940";
    $scope.init = function (id) {
        _id = id;
    };
    $scope.SetProperty = function (property) {
        $scope.property = property;
        $scope.$applyAsync();
    };
    $scope.SetProperties = function (properties) {
        $scope.properties = properties;
        $scope.$applyAsync();
    };
    $scope.addmarker = function () {
        alert(123);
    };
});

//http://dev.virtualearth.net/REST/v1/Locations/US/WA/98052/Redmond/1%20Microsoft%20Way?o=xml&key=BingMapsKey

/*var searchManager;
function createSearchManager() {
    if (!searchManager) {
        map.addComponent('searchManager', new Microsoft.Maps.Search.SearchManager(map));
        searchManager = map.getComponent('searchManager');
        displayAlert('Search module loaded');
    }
}
Microsoft.Maps.loadModule('Microsoft.Maps.Search', { callback: createSearchManager })

//url: 'http://dev.virtualearth.net/REST/v1/Locations/' + "32.789891" + ',' + "-96.798593",
function geocodeRequest() {
    createSearchManager();
    var where = 'Dallas';
    var userData = { name: 'Maps Test User', id: 'XYZ' };
    var request =
    {
        where: where,
        count: 5,
        bounds: map.getBounds(),
        callback: onGeocodeSuccess,
        errorCallback: onGeocodeFailed,
        userData: userData
    };
    searchManager.geocode(request);
};

function onGeocodeSuccess(result, userData) {
    if (result) {
        map.entities.clear();
        var topResult = result.results && result.results[0];
        if (topResult) {
            var pushpin = new Microsoft.Maps.Pushpin(topResult.location, null);
            map.setView({ center: topResult.location, zoom: 10 });
            map.entities.push(pushpin);
        }
    }
}

function onGeocodeFailed(result, userData) {
    displayAlert('Geocode failed');
}

if (searchManager) {
    geocodeRequest();
}
else {
    Microsoft.Maps.loadModule('Microsoft.Maps.Search', { callback: geocodeRequest });
}
*/
function showMap() {
    $.ajax({
        url: "/Home/GetProperty",
        type: "post",
        data: { id: _id },
        dataType: "html",
        success: function (data) {
            var property = JSON.parse(data);
            map = new Microsoft.Maps.Map(document.getElementById('bingMap'), {
                credentials: "Al3nAvLTiRL6-UDLFNLk2hA4bpQ6RoTrigdFz1M5t-H2g7vFZxkSCo57_VREL56v",//'Your Bing Maps Key'
                maxZoom: 15,
                minZoom: 12,
                center: new Microsoft.Maps.Location(property["latitude"], property["longitude"])
            });
            AddNewMarker(property["id"], property["latitude"], property["longitude"], property["price"]);
            angular.element(document.getElementById('controller')).scope().SetProperty(property);
        }
    });
}

function loadMapScenario() {
    map = new Microsoft.Maps.Map(document.getElementById('bingMap'), {
        credentials: "Al3nAvLTiRL6-UDLFNLk2hA4bpQ6RoTrigdFz1M5t-H2g7vFZxkSCo57_VREL56v",//'Your Bing Maps Key'
        maxZoom: 15,
        minZoom: 12,
        center: new Microsoft.Maps.Location(32.779891, -96.798563)
    });

    $.ajax({
        url: "/Home/GetAllProperties",
        type: "post",
        dataType: "json",
        contentType: "application/json",
        success: function (properties) {
            angular.element(document.getElementById('controller')).scope().SetProperties(properties);
            $.each(properties, function (key, value) {
                AddNewMarker(value["id"], value["latitude"], value["longitude"], value["price"]);
            });
        }
    });
}

function AddNewMarker(PropId, lat, lng, price) {

    var contentString = '<div id="content" style="background-color:White; width: 240px;">' +
        '<img src="/images/' + PropId + '_primary.jpg" style="padding: 10px;" alt="La Estancia">' +
        '<p class="" style="position: relative; top: -30px; left:10px; color: snow; padding:0px"><strong>&nbsp;&nbsp;$' + price + '/month</strong></p>' +
        '<a href="/Home/property/' + PropId + '/" style="position: relative; right: 0px;">More info</a></div>';

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