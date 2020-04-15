$(function(){
  $('.datatable').DataTable( {
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'Add Role',
               /* action: function ( e, dt, node, config ) {
                    alert( 'Button activated' );
                }*/
				 action: function myFunction() {
    location.href = "Role_add.html";
}
            }
        ]
    } );
  

  // $('.datatable').css({'border-collapse':'collapse !important'});
  $('.datatable').attr('style', 'border-collapse: collapse !important');
});

var table = $('#UserZoneAccess').DataTable( {
									 dom: 'Bfrtip',
        buttons: [
            {
                text: 'Add',
               /* action: function ( e, dt, node, config ) {
                    alert( 'Button activated' );
                }*/
				 action: function myFunction() {
    location.href = "UserZone_Add.html";
}
            }
        ]
    } );
var table = $('#Zonelist').DataTable( {
									 dom: 'Bfrtip',
        buttons: [
            {
                text: 'Add',
               /* action: function ( e, dt, node, config ) {
                    alert( 'Button activated' );
                }*/
				 action: function myFunction() {
    location.href = "UserZone_Add.html";
}
            }
        ]
    } );

var table = $('#Zoneindex').DataTable( {
									 dom: 'Bfrtip',
        buttons: [
            {
                text: 'Add',
               /* action: function ( e, dt, node, config ) {
                    alert( 'Button activated' );
                }*/
				 action: function myFunction() {
    location.href = "#";
}
            }
        ]
    } );

var table = $('#Textlist').DataTable( {
									 dom: 'Bfrtip',
        buttons: [
            {
                text: 'Add',
               /* action: function ( e, dt, node, config ) {
                    alert( 'Button activated' );
                }*/
				 action: function myFunction() {
    location.href = "UserZone_Add.html";
}
            }
        ]
    } );


// EDITOR

