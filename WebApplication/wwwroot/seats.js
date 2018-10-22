$(function(){
  $.ajax({url:'mock/flight.json',success:function(d){
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
              .append('<td><button type="button">Book</button> </td>');
          $('#flightsTable > tbody:last-child').append(tr);
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
 }
})
});
