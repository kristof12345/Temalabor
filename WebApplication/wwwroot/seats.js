var r = undefined;
var selectedSeats = new Array();
var API_BASE_URL = 'https://localhost:5001/api/';

$(document).ready(function () {
    $('#listFlights').click( function() {
        listFlights();
    });
    $('#mySelectedSeats').click(function () {
        console.log(selectedSeats);
        alert("Még nincs implementálva");
    });
});

function createFlightsTable() {
    var table = $('<table class="table table-hover">');
    var thead = $('<thead class="thead-light">');
    table.attr('id','flightsTable');
    var row = $('<tr>');
    var tableHeaders = ["Repülőtípus", "Honnan", "Hova", "Dátum", "Státusz", "Szabad helyek", "Összes hely"];//, "Book"];
    for (i=0; i<tableHeaders.length; ++i) {
        var tableHeader = $('<th scope="col">').text(tableHeaders[i]);
        row.append(tableHeader);
    }
    thead.append(row);
    table.append(thead);
    
    return table;
}

function getSeats(flightId) {
    if (r == undefined) {
        $('#placeForCanvas').css('border','1px solid #aaa');
        r = Raphael('placeForCanvas', '100%', '100%');
        r.setViewBox(0, 0, 1000, 700);
    } else {
        r.clear();
    }
    $('#selectedSeats').replaceWith('<div id="selectedSeats" class="col">Kiválasztott helyek:</div>');
    r.image("img/Antonov125.png", 0, 0, 1000, 700);
    $.ajax({
        type: 'GET',
        url: API_BASE_URL + 'seat/flightID/' + flightId,
        success: function (d) {
            
            for (var i = 0; i < d.length; ++i) {
                var color = "green";

                if (d[i].reserved)
                    color = "red";

                var seat = r.rect(d[i].coordinates.x-150, d[i].coordinates.y, 30, 30)
                seat.attr({ fill: color })
                seat.data("seatId", d[i].seatId);
                seat.data("flightId", flightId);
                seat.data("reserved", d[i].reserved);
                seat.click(function () {
                    if (!this.data("reserved")) {
                        //console.log("flightId: " + this.data("flightId") + "seatId: " + this.data("seatId"));
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
                        var listOfSelectedSeats = $('<ul class="list-group">');
                        selectedSeats.forEach(ss => {
                            let listElement = $('<il class="list-group-item">');
                            listElement.append("JáratAz: " + ss.flightId + " HelyAz: " + ss.seatId);
                            listOfSelectedSeats.append(listElement);
                        });
                        $('#selectedSeats').replaceWith('<div id="selectedSeats" class="col">Kiválasztott helyek:</div>');
                        $('#selectedSeats').append(listOfSelectedSeats);
                        //console.log(selectedSeats);
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

// Számok kétszámjeggyel
function toTwoDigits(number) {
    return ('0' + number).slice(-2);
}

function loadFlightsTable(table, flight) {
    $.ajax({
        type: 'GET',
        url: API_BASE_URL + 'seat/flightId/' + flight.flightId,
        success: function (data) {
            flight.freeSeats = data.filter(s => !s.reserved).length;
            flight.numberOfSeats = data.length;
            
            var element = flight;
            var date = new Date(element.date);
            var dateStr = date.getFullYear() + '.' + toTwoDigits(date.getMonth() + 1) + '.'
                + toTwoDigits(date.getDate()) + '. ' + toTwoDigits(date.getHours()) + ':' + toTwoDigits(date.getMinutes());
            var tr = $("<tr/>")
                .append($('<td/>', { text: element.planeTypeName }))
                .append($('<td/>', { text: element.departure }))
                .append($('<td/>', { text: element.destination }))
                .append($('<td/>', { text: dateStr }))
                .append($('<td/>', { text: element.status }))
                .append($('<td/>', { text: element.freeSeats }))
                .append($('<td/>', { text: element.numberOfSeats }));
                //.append('<td><input id=' + element.flightId + ' type="button" value="Book" onclick="getSeats(this.id)"> </td>');
            tr.click(function () {
                getSeats(element.flightId);
            });
            //$('#flightsTable > tbody:last-child')
            table.append(tr);
            
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function listFlights() {
    var table = createFlightsTable();
    var tbody = $("<tbody>");
    $.ajax({
        //url: 'mock/flight.json',
        type: 'GET',
        url: API_BASE_URL + 'flight',
        success: function (d) {
            //var t = $("#flightsTable");
            
            for (var i = 0; i < d.length; ++i) {
                loadFlightsTable(tbody, d[i]);
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        },
        complete: function () {
            table.append(tbody);
            var responsiveDiv = $('<div id="placeForTable" class="table-responsive">');
            responsiveDiv.append(table);
            $('#placeForTable').replaceWith(responsiveDiv);
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
