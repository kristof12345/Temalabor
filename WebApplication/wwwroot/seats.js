var r = undefined;
var selectedSeats = new Array();
var API_BASE_URL = 'https://localhost:5001/api/';

$(document).ready(function () {
    $('#listFlights').click( function() {
        listFlights();
    });
    $('#myBookedSeats').click(function () {
        listReservations();
        /*console.log(selectedSeats);
        alert("Még nincs implementálva");*/
    });
});

function createFlightsTable() {
    var table = $('<table class="table table-hover">');
    var thead = $('<thead class="thead-light">');
    table.attr('id','flightsTable');
    var row = $('<tr>');
    var tableHeaders = ["Járat", "Repülőtípus", "Honnan", "Hova", "Dátum", "Státusz", "Szabad helyek", "Összes hely"];//, "Book"];
    for (i=0; i<tableHeaders.length; ++i) {
        var tableHeader = $('<th scope="col">').text(tableHeaders[i]);
        row.append(tableHeader);
    }
    thead.append(row);
    table.append(thead);
    
    return table;
}

function getSeats(flight) {
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
        url: API_BASE_URL + 'seat/flightID/' + flight.flightId,
        success: function (d) {
            
            for (var i = 0; i < d.length; ++i) {
                var color = "green";

                if (d[i].reserved)
                    color = "red";

                var seat = r.rect(d[i].coordinates.x-150, d[i].coordinates.y, 30, 30)
                seat.attr({ fill: color })
                seat.data("seatId", d[i].seatId);
                seat.data("flightId", flight.flightId);
                seat.data("reserved", d[i].reserved);
                if (d[i].seatType === 0) {
                    seat.data("price", flight.normalPrice);
                    seat.data("category", "normal");
                } else {
                    seat.data("price", flight.firstClassPrice);
                    seat.data("category", "first class");
                }

                seat.click(function () {
                    if (!this.data("reserved")) {
                        //console.log("flightId: " + this.data("flightId") + "seatId: " + this.data("seatId"));
                        var clickedSeat = {
                            flightId: this.data("flightId"),
                            seatId: this.data("seatId"),
                            price: this.data("price"),
                            category: this.data("category")
                        };
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
                            listElement.append("JáratAz: " + ss.flightId + " HelyAz: " + ss.seatId + " " + ss.category + " " + ss.price + " Ft");
                            listOfSelectedSeats.append(listElement);
                        });
                        let listElement = $('<il class="list-group-item">');
                        var sum = 0;
                        selectedSeats.forEach(ss => sum += ss.price);
                        listElement.append("Összesen: " + sum + " Ft");
                        listOfSelectedSeats.append(listElement);
                        $('#selectedSeats').replaceWith('<div id="selectedSeats" class="col">Kiválasztott helyek:</div>');
                        $('#selectedSeats').append(listOfSelectedSeats);
                        let payButton = $('<button type="button" class="btn btn-primary">Fizetés</button>');
                        var now = new Date();
                        var reservation = {
                            SeatList: selectedSeats.map(ss => ss.seatId),
                            ReservationId: 5,
                            UserName: "Baranyai Gergely",
                            UserID: 2,
                            FlightId: clickedSeat.flightId,
                            SeatCount: selectedSeats.length,
                            SeatCountString: selectedSeats.length + " seats",
                            Cost: sum,
                            Date: now.toJSON(),
                            Details: undefined
                        };
                        payButton.click(function () {
                            
                            $.ajax({
                                type: 'POST',
                                url: API_BASE_URL + 'reservation',
                                dataType: 'json',
                                contentType: 'application/json',
                                data: JSON.stringify(reservation),
                                success: function() {
                                    listReservations()
                                },
                                error: function (request, status, error) {
                                    alert(request.responseText);
                                }
                            });
                        });
                        $('#selectedSeats').append(payButton);
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
                .append($('<td/>', { text: flight.flightId }))
                .append($('<td/>', { text: element.planeTypeName }))
                .append($('<td/>', { text: element.departure }))
                .append($('<td/>', { text: element.destination }))
                .append($('<td/>', { text: dateStr }))
                .append($('<td/>', { text: element.status }))
                .append($('<td/>', { text: element.freeSeats }))
                .append($('<td/>', { text: element.numberOfSeats }));
                //.append('<td><input id=' + element.flightId + ' type="button" value="Book" onclick="getSeats(this.id)"> </td>');
            tr.click(function () {
                selectedSeats = new Array();
                getSeats(element);
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
            var placeForTable = $('<div id="placeForTable">');
            var responsiveDiv = $('<div class="table-responsive">');
            responsiveDiv.append(table);
            placeForTable.append('<h1>Járatok</h1>');
            placeForTable.append(responsiveDiv);
            $('#placeForTable').replaceWith(placeForTable);
        }
    });
}

function createReservationsTable() {
    var table = $('<table class="table">');
    var thead = $('<thead class="thead-light">');
    var row = $('<tr>');
    var tableHeaders = ["Járat", "Indulás napja", "Honnan", "Hova", "Leírás", "Ár", "Vásárlás dátuma", "Helyek azonosítója"];
    for (i = 0; i < tableHeaders.length; ++i) {
        var tableHeader = $('<th scope="col">').text(tableHeaders[i]);
        row.append(tableHeader);
    }
    thead.append(row);
    table.append(thead);

    return table;
}

function queryReservations(userID) {
    var table = createReservationsTable();
    var tbody = $("<tbody>");
    $.ajax({
        type: 'GET',
        url: API_BASE_URL + 'reservation/userID/' + userID,
        success: function (d) {

            for (var i = 0; i < d.length; ++i) {
                var tr = $("<tr/>")
                    .append($('<td/>', { text: d[i].flightId }))
                    .append($('<td/>', { text: d[i].details.travelDate }))
                    .append($('<td/>', { text: d[i].details.departure }))
                    .append($('<td/>', { text: d[i].details.destination }))
                    .append($('<td/>', { text: d[i].details.detailsString }))
                    .append($('<td/>', { text: d[i].cost }))
                    .append($('<td/>', { text: d[i].date }))
                    .append($('<td/>', { text: d[i].seatList.join(", ") }));
                tbody.append(tr);
            }
        },
        error: function (request, status, error) {
            alert(request.responseText);
        },
        complete: function () {
            table.append(tbody);
            var placeForTable = $('<div id="placeForTable">');
            var responsiveDiv = $('<div class="table-responsive">');
            responsiveDiv.append(table);
            placeForTable.append("<h1>Jegyeim</h1>");
            placeForTable.append(responsiveDiv);
            $('#placeForTable').replaceWith(placeForTable);
        }
    });
}

function listReservations() {
    r = undefined;
    queryReservations(2);
    $('#placeForCanvas').replaceWith('<div id="placeForCanvas" class="col-6"></div>');
    $('#selectedSeats').replaceWith('<div id="selectedSeats" class="col"></div>');
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
