/*$(function(){
    
});*/

var r = undefined;

$(document).ready(function () {
    getFlights();
});

function getSeats(flightId) {
    if (r == undefined) {
        r = Raphael(0, 100, 1000, 700);
    } else {
        r.clear();
    }
    r.image("img/Antonov125.png", 0, 0, 1000, 700);
    $.ajax({
        type: 'GET',
        url: 'https://localhost:5001/api/seat/flightID/' + flightId,
        success: function (d) {
            
            for (var i = 0; i < d.length; ++i) {
                console.log(d[i]);
                var color = "green";

                if (d[i].reserved)
                    color = "red";

                r.rect(d[i].coordinates.x, d[i].coordinates.y, 30, 30)
                    .attr({ fill: color })
                    .click(function () {
                        alert('rectangle clicked');
                    });
            }
        }
    });

}

function getFlights() {
    $.ajax({
        //url: 'mock/flight.json',
        type: 'GET',
        url: 'https://localhost:5001/api/flight',
        success: function (d) {
            //var t = $("#flightsTable");
            for (var i = 0; i < d.length; ++i) {
                var element = d[i];
                var tr = $("<tr/>")
                    .append($('<td/>', { text: element.planeTypeName }))
                    .append($('<td/>', { text: element.departure }))
                    .append($('<td/>', { text: element.destination }))
                    .append($('<td/>', { text: element.date }))
                    .append($('<td/>', { text: element.status }))
                    .append($('<td/>', { text: element.freeSeats }))
                    .append($('<td/>', { text: element.numberOfSeats }))
                    .append('<td><input id=' + element.flightId + ' type="button" value="Book" onclick="getSeats(this.id)"> </td>');
                $('#flightsTable > tbody:last-child').append(tr);
            }
        }
    })
}
/*var r = Raphael(0, 0, 300, 150);

r.rect(0, 0, 50, 50)
    .attr({fill: "#000"})
    .click(function () {
        alert('first rectangle clicked');
     });

r.rect(75, 0, 50, 50)
    .attr({fill: "#000"})
    .click(function () {
        alert('second rectangle clicked');
     });
   }*/
