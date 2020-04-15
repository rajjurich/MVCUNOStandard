$(document).ready(function() {
/* initialize the external events
     -----------------------------------------------------------------*/

  $('#external-events div.ext-event').each(function() {

    // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
    // it doesn't need to have a start or end
    var eventObject = {
      title: $.trim($(this).text()) // use the element's text as the event title
    };

    // store the Event Object in the DOM element so we can get to it later
    $(this).data('eventObject', eventObject);

    // make the event draggable using jQuery UI
    $(this).draggable({
      zIndex: 999,
      revert: true,      // will cause the event to go back to its
      revertDuration: 0  //  original position after the drag
    });

  });


  /* initialize the calendar
   -----------------------------------------------------------------*/

  var date = new Date();
  var d = date.getDate();
  var m = date.getMonth();
  var y = date.getFullYear();

  $('#calendar').fullCalendar({
    header: {
      left: 'prev, today',
	  //left: 'prev,next',
      center: 'title',
     // right: 'month,basicWeek,basicDay'
	   right: 'next'
    },
	 /*dayRender: function(date, cell) {
		cell.append('<div class="attend text-center"><b>Work Day</b> <br/>Present</div>');
	    cell.append('<div class="in">in</div>');
       cell.append('<div class="out">out</div>');
	  },*/
	  disableDragging: true,
    editable: true,
    droppable: true, // this allows things to be dropped onto the calendar !!!
    drop: function(date, allDay) { // this function is called when something is dropped
        debugger;
      // retrieve the dropped element's stored Event Object
      var originalEventObject = $(this).data('eventObject');

      // we need to copy it, so that multiple events don't have a reference to the same object
      var copiedEventObject = $.extend({}, originalEventObject);

      // assign it the date that was reported
      copiedEventObject.start = date;
      copiedEventObject.allDay = allDay;

      // render the event on the calendar
      // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
      $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

      // is the "remove after drop" checkbox checked?
      if ($('#drop-remove').is(':checked')) {
          // if so, remove the element from the "Draggable Events" list
        $(this).remove();
      }

    },
	
    events: [
      {
         title: 'Holiday ',
        start: new Date(y, m, 28),
        allDay: false,
		className : 'text-danger'
       // url: '#',
       // className : 'gplus-bg'
      },
	  {
         title: '<b>Workday</b> <br/> <small>Present</small> <br/> <div style=" margin-top:10px;"><small class="pull-left">09:54</small> <small class="pull-right">18:24</small></div>',
		 start: new Date(y, m,  d-25),
        allDay: true,
		//className : 'text-success'
      },
	  {
         title: 'Workday <br> <small class="text-success">PL</small>',
       start: new Date(y, m,  25),
        allDay: true,
		//className : 'text-success'
      }
     /* {
        title: 'Long Event',
        start: new Date(y, m, d-5),
        end: new Date(y, m, d-2),
        className : 'success-bg',
      },
      {
        id: 999,
        title: 'Repeating Event',
        start: new Date(y, m, d-3, 15, 0),
        allDay: false,
        className : 'fb-bg',
      },
      /*{
        title: 'Europe Trip',
        start: new Date(y, m, 13),
        end: new Date(y, m, 15),
        className : 'brown-bg',
      },*/
      /* {
        title: 'Birthday Event',
        start: new Date(y, m, 9),
        end: new Date(y, m, 12),
        className : 'info-bg',
      },
      {
        id: 999,
        title: 'Repeating Event',
        start: new Date(y, m, d+4, 12, 0),
        allDay: false,
        className : 'linkedin-bg',
      },
      {
        title: 'Meeting',
        start: new Date(y, m, d, 10, 25),
        allDay: false,
        className : 'info-bg',
      },
      /*{
        title: 'Lunch',
        start: new Date(y, m, d, 12, 0),
        end: new Date(y, m, d, 14, 0),
        allDay: false,
        className : 'warning-bg',
      },*/
      /* {
        title: 'Birthday Party',
        start: new Date(y, m, d+1, 19, 0),
        end: new Date(y, m, d+1, 22, 30),
        allDay: false,
        className : 'twitter-bg',
      },
      {
        title: 'Click for link',
        start: new Date(y, m, 28),
        end: new Date(y, m, 29),
        url: '#',
        className : 'gplus-bg'
      }*/
    ],
	eventRender: function(event, element) {                                          
    element.find('span.fc-event-title').html(element.find('span.fc-event-title').text());                   

} 
  });
});