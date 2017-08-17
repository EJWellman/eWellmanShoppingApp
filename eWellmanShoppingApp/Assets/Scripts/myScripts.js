function navToggle() {
	var styleStatus = document.getElementById("hidey");
	var buttPosit = document.getElementById("showHideDiv")
	var buttStatus = document.getElementById("showHide");
	var contentStat = document.getElementById("mainContents");
	if (styleStatus.style.display == "block") {
		styleStatus.style.display = "none";
		buttStatus.classList.remove("fa-caret-up");
		buttStatus.classList.add("fa-caret-down");
		buttPosit.style.top = "0px";
		contentStat.style.margin = "-55px auto auto auto";
	}
	else if (styleStatus.style.display == "none") {
		styleStatus.style.display = "block";
		buttStatus.classList.remove("fa-caret-down");
		buttStatus.classList.add("fa-caret-up");
		buttPosit.style.top = "50px";
		contentStat.style.margin = "auto auto auto auto";
	}
}
//Function to handle the model for the items
//
function modalPusher(passedImg, passedDesc) {
	var placeImgHere = document.getElementById("imgHold");
	var placeDescHere = document.getElementById("descHold");
	var placeMapHere = document.getElementById("map");
	if (passedImg && passedDesc == 'map') {
		placeMapHere.style.display = ("block");
		placeImgHere.src = "";
		placeDescHere.innerHTML = "";
	}
	else {
		placeMapHere.style.display = ("none")
		placeImgHere.src = passedImg;
		placeDescHere.innerHTML = passedDesc.toString();
	}
}

var map;
function initMap() {
	var coderF = { lat: 36.0967156, lng: -80.0721315 };
	map = new google.maps.Map(document.getElementById('map'), {
		center: coderF,
		zoom: 11
	});
	var marker = new google.maps.Marker({
		position: coderF,
		map: map
	});
	$("#itemModal").on("shown.bs.modal", function () {
		google.maps.event.trigger(map, "resize");
		map.setCenter(marker.getPosition())
	});
}