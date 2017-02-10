
//function refresh() {

//	console.log('refresh every 6000 ms');    
    
//    window.location.reload(true);
//}

//setTimeout(refresh, 7000);


!function(){
	
	if ('WebSocket' in window){
		// WebSocket is supported. You can proceed with your code
		var connection = new WebSocket('ws://127.0.0.1:4570/livereload')
		
		connection.onmessage = function (event) {
			
			//console.log("data: " + event.data);
			
			if(event.data == "GPSON"){
				d3.select("#gpsicon").style("background-color","green");
				
				setTimeout(function(){
					d3.select("#gpsicon").style("background-color","red");	
				}, 19000);
			} else if(event.data == "ECMON"){
				d3.select("#ecmicon").style("background-color","green");
				
				setTimeout(function(){
					d3.select("#ecmicon").style("background-color","red");	
				}, 19000);
				
			} else if(event.data === "RELOAD") {
				location.reload(true);
			} else { //it must be speed data, kind of last option on the socket stream
				
				console.log("speed.data:"+ event.data);
				
				//compliance rules
				if( event.data > 0 ){
					
					var htmlcontent = "<div> <label> Change the Status to DRIVING </label> </div>"
					modal.open({content: htmlcontent, width:"70%"});
					setTimeout(function(){
						modal.close();	
					}, 5000);
					
				} else if( event.data == 0 ){
					
					var htmlcontent = "<div> <label> Change the Status to OFF DUTY </label> </div>"

					//1 sec
					var onesec = 0;
					setTimeout(function(){
						onesec = event.data;	
					}, 1000);
					var secsec = 0;
					setTimeout(function(){
						secsec = event.data;	
					}, 1000);
					var thrsec = 0;
					setTimeout(function(){
						thrsec = event.data;	
					}, 1000);					
					
					if((onesec === 0) && (secsec === 0) && (thrsec === 0)){
						modal.open({content: htmlcontent, width:"70%"});
						setTimeout(function(){
							modal.close();	
						}, 5000);
					}
				}
			}
			
			//window.location.reload(true);
			//location.reload(); //reload from cache
			//document.location.reload();
			//history.go(0);
			//$(window).trigger('hashchange');
			//navigate();
			//refresh()
			
		}
		
	} else {
		//WebSockets are not supported. Try a fall back method like long-polling etc
		alert('websocket not supported, live reoload feature cannot be used!')
	}

}();