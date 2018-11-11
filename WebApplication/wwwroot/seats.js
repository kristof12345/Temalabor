var r = undefined;
var selectedSeats = new Array();

$(document).ready(function () {
    getFlights();
});

function getSeats(flightId) {
    if (r == undefined) {
        r = Raphael(0, 100, 1000, 700);
    } else {
        r.clear();
    }
    r.image("img/Antonov125.png", 0, 0, 700, 700);
    $.ajax({
        type: 'GET',
        url: 'https://localhost:5001/api/seat/flightID/' + flightId,
        success: function (d) {
            
            for (var i = 0; i < d.length; ++i) {
                var color = "green";

                if (d[i].reserved)
                    color = "red";

                var seat = r.rect(d[i].coordinates.x-300, d[i].coordinates.y, 30, 30)
                seat.attr({ fill: color })
                seat.data("seatId", d[i].seatId);
                seat.data("flightId", flightId);
                seat.click(function () {
                    if (!this.reserved) {
                        console.log("flightId: " + this.data("flightId") + "eatId: " + this.data("seatId"));
                        var clickedSeat = { flightId: this.data("flightId"), seatId: this.data("seatId") };
                        var inSelectedSeats = selectedSeats.filter(row => row.flightId === clickedSeat.flightId && row.seatId === clickedSeat.seatId);
                        if (inSelectedSeats.length > 0) {
                            var index = selectedSeats.indexOf(inSelectedSeats[0]);
                            selectedSeats.splice(index, 1);
                            this.attr({ fill: "green" })
                        } else {
                            selectedSeats.push(clickedSeat);
                            this.attr({ fill: "yellow" })
                        }
                        console.log(selectedSeats);
                    } else {
                        alert("Ez a hely már foglalt!");
                    }
                });
                
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
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
        },
        error: function (request, status, error) {
            alert(request.responseText);
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
