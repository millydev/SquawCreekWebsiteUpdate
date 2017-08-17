var uri = '../api/events';
var nav;
var calendar;

$(document).ready(function () {
	nav = new DayPilot.Navigator("nav");
	nav.showMonths = 3;
	nav.skipMonths = 3;
	nav.selectMode = "week";
	nav.onTimeRangeSelected = function (args) {
		calendar.startDate = args.day;
		calendar.update();
		loadEvents();
	};
	nav.init();

	calendar = new DayPilot.Calendar("calendar");
	calendar.theme = "calendar_default";
	calendar.viewType = "Week";

	// Move an event
	calendar.onEventMoved = function (args) {
		$.ajax({
			type: 'POST',
			url: uri,
			data: '{action:"move", id:"' + args.e.id() + '", start:"' + args.newStart.toString() + '", end:"' + args.newEnd.toString() + '" }',
			success: function (data) { },
			contentType: "application/json",
			dataType: 'json'
		});
	};

	// Resize an event
	calendar.onEventResized = function (args) {
		$.ajax({
			type: 'POST',
			url: uri,
			data: '{action:"move", id:"' + args.e.id() + '", start:"' + args.newStart.toString() + '", end:"' + args.newEnd.toString() + '" }',
			success: function (data) { },
			contentType: "application/json",
			dataType: 'json'
		});
	};

	// Create an event
	calendar.onTimeRangeSelected = function (args) {
		var name = prompt("New event name:", "Event");
		calendar.clearSelection();
		if (!name) return;

		$.ajax({
			type: 'POST',
			url: uri,
			data: '{action:"create", text:"' + name + '", start:"' + args.start.toString() + '", end:"' + args.end.toString() + '" }',
			success: function (data) {
				var e = new DayPilot.Event({
					start: args.start,
					end: args.end,
					id: data.id,
					text: name
				});
				calendar.events.add(e);
			},
			contentType: "application/json",
			dataType: 'json'
		});
	};

	// Show an event
	calendar.onEventClick = function (args) {
		alert("clicked: " + args.e.id());
	};

	calendar.init();

	loadEvents();

});

function loadEvents() {
	var start = calendar.visibleStart();
	var end = calendar.visibleEnd();
	$.ajax({
		type: 'POST',
		url: uri,
		data: '{action:"filter", text:"", start:"' + start + '", end:"' + end + '" }', // or JSON.stringify ({name: 'jonas'}),
		success: function (data) {
			calendar.events.list = data;
			calendar.update();
		},
		contentType: "application/json",
		dataType: 'json'
	});
}