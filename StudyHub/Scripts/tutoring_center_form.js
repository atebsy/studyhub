
$(document).ready(function () {
	initializeGooglePlaces();
	 $.validate({
    form : '#tc_reg_form',
    modules : 'security',
    onSuccess : function($form) {
        $("#add_new_tc_btn").button('loading');
        $.ajax({
            method: 'POST',
            url: "/tutoring-center/add-new",
            data: $('#tc_reg_form').serialize(),
            type: 'json',
            success: function(data){
                console.log(data);
            if(data.response == "success"){
                $.notify("Account sucessfully created", "success");
                setTimeout(function(){
                    window.location = siteUrl+"/login";
                },1500);
                }else if (data.response == "Duplicated email"){
                 $.notify("Email already exist", "error");
                }else{
                    $.notify(data.response, "error");
                }              
            },
            complete: function(response){
               console.log(response.responseText);
                $("#add_new_tc_btn").button('reset');
            }
        });
        return false;
    },
  });

	 $("#specialities").select2({
        placeholder: "Type a speciality. e.g: Biology",
        allowClear: true
      });

	 $("#infrastructure").select2({
        placeholder: "Enter infrastructure. e.g: HighSpeed internet",
        allowClear: true,
        tags: true
      });
});


 function geocodeAddress(address) {
 	if(typeof address != "undefined" && address != "" && address != null){

 			var geocoder =  new google.maps.Geocoder();
        geocoder.geocode({'address': address}, function(results, status) {
          if (status === 'OK') {
            console.log(results);
            var latitude = results[0].geometry.location.lat();
            var longitude = results[0].geometry.location.lng();
            $("#gps_coordinates").val(latitude+","+longitude);
          }
        });

 	}
}

function getStatesPerCountry(countryId){
	 $.ajax({
	     url: "/Home/getCountryStates/" + countryId,
	     dataType: "json",
	     method: "GET",
	     ContentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#states").html(data.options);
        }
    });
}

function getTownsPerState(stateId){
		 $.ajax({
		url: "/Home/getStateTowns/" + stateId,
		dataType: 'json',
		method: "GET",
		ContentType: "application/json; charset=utf-8",
		success: function (data) {
            $("#towns").html(data.options);
        }
    });
}

 function numbersonly(myfield, e, dec){
    var key;
    var keychar;

    if (window.event)
         key = window.event.keyCode;
              else if (e)
                  key = e.which;
                                                                else
                                                                    return true;
                                                                keychar = String.fromCharCode(key);

// control keys
                                                                if ((key == null) || (key == 0) || (key === 8) ||
                                                                        (key == 9) || (key == 13) || (key === 27))
                                                                    return true;

// numbers
                                                                else if ((("0123456789").indexOf(keychar) > -1))
                                                                    return true;

// decimal point jump
                                                                else if (dec && (keychar == "."))
                                                                {
                                                                    myfield.form.elements[dec].focus();
                                                                    return false;
                                                                }
                                                                else
                                                                    return false;
                                                            }