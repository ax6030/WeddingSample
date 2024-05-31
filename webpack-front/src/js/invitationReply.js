$(function() {
    $("#createInvitation").on("submit", function(e) {
      e.preventDefault();
  
      var reply = {
        name: $("#name").val(),
        phone: $("#phone").val(),
        address: $("#address").val(),
        count_person: +$("#count_person").val(),
        count_child: +$("#count_child").val(),
        count_veg: +$("#count_veg").val(),
        message: $("#message").val()

      };
  
      console.log(reply);
      createInvitation(reply);
      jQuery.noConflict(); 

    });
  });
  
  function createInvitation(reply) {
	var trackingJSON = JSON.stringify(reply);
	var urlAjax =  "https://localhost:7208/api/applications";

	$.ajax({
		type: "POST",
		url: urlAjax,
		contentType: "application/json",
		data: trackingJSON,
	        success: function( data, textStatus, jQxhr ){
                   alert("Success");
	        },
		error: function( jqXhr, textStatus, errorThrown ){
		    console.log( errorThrown );
		},
		dataType: 'json'
	});
  }