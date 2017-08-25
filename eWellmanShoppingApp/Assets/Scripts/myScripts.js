function navToggle() {
	var styleStatus = document.getElementById("hidey");
	var buttPosit = document.getElementById("showHideDiv")
	var buttStatus = document.getElementById("showHide");
	var bodyStat = document.getElementById("bodyContent");
	var contentStat = document.getElementById("mainContents");
	if (styleStatus.style.display == "block") {
		styleStatus.style.display = "none";
		buttStatus.classList.remove("fa-caret-up");
		buttStatus.classList.add("fa-caret-down");
		buttPosit.style.top = "0px";
		bodyStat.style.marginTop = "0px";
	}
	else if (styleStatus.style.display == "none") {
		styleStatus.style.display = "block";
		buttStatus.classList.remove("fa-caret-down");
		buttStatus.classList.add("fa-caret-up");
		buttPosit.style.top = "50px";
		bodyStat.style.marginTop = "40px";
	}
}
//Function to handle the model for the items
//
function modalPusher(passedImg, passedDesc) {
	var placeImgHere = document.getElementById("imgHold");
	var placeDescHere = document.getElementById("descHold");
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
	$("#mapModal").on("shown.bs.modal", function () {
		google.maps.event.trigger(map, "resize");
		map.setCenter(marker.getPosition())
	});
}

//var inputChange = document.getElementById("fileLabel");
//inputChange.addEventListener("change", labelChange, false);
//function labelChange() {
//	var fileName = this.files[0].name;
//	inputchange.innerHTML = fileName;

//}

var inputs = document.querySelectorAll('.inputFile');
Array.prototype.forEach.call(inputs, function (input) {
	var label = input.nextElementSibling,
		labelVal = label.innerHTML;

	input.addEventListener('change', function (e) {
		var fileName = '';
		if (this.files && this.files.length > 1)
			fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
		else
			fileName = e.target.value.split('\\').pop();

		if (fileName)
			label.innerHTML = fileName;
		else
			label.innerHTML = labelVal;
	});
});