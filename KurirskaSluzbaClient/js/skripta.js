// podaci od interesa
var host = "https://localhost:";
var port = "44352/";
var paketiEndpoint = "api/paketi/";
var kuririEndpoint = "api/kuriri/";
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
var statisticsEndpoint = "api/statistics";
var searchEndpoint = "api/pretraga";

var formAction = "Create";
var editingId;
var jwt_token;


function loadPage() {

	//setupPopper();
	showLogin();
	//loadPaketi();
}

// prikaz forme za prijavu
function showLogin() {
	loadPaketi();
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "block";
	document.getElementById("registerForm").style.display = "none";
	document.getElementById("loginFormStartDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("searchFormZaposleni").style.display = "none";


	
}
function showLoginNew() {
	document.getElementById("usernameLogin").value = "";
	document.getElementById("passwordLogin").value = "";
	document.getElementById("usernameRegister").value;
	document.getElementById("emailRegister").value = "";
	document.getElementById("passwordRegister").value = "";
	document.getElementById("confirmPasswordRegister").value = "";
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "none";
	document.getElementById("loginFormStartDiv").style.display = "block";
	document.getElementById("registerForm").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("searchFormZaposleni").style.display = "none";



	loadPaketi();
}

function validateRegisterForm(username, email, password, confirmPassword) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (email.length === 0) {
		alert("Email field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	} else if (confirmPassword.length === 0) {
		alert("Confirm password field can not be empty.");
		return false;
	} else if (password !== confirmPassword) {
		alert("Password value and confirm password value should match.");
		return false;
	}
	return true;
}

function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful registration");
					alert("Successful registration");
					showLoginNew();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}

// prikaz forme za registraciju
function showRegistration() {
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "none";
	document.getElementById("registerForm").style.display = "block";
	document.getElementById("logout").style.display = "none";		
	document.getElementById("searchFormZaposleni").style.display = "none";
	//loadPaketi();


}

function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}

function loginUser() {
	var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					alert("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Currently logged in user: <b>" + data.username + "<b/>";
						document.getElementById("logout").style.display = "block";
						document.getElementById("loginForm").style.display = "none";
						document.getElementById("btnRegister").style.display = "none";
						document.getElementById("searchFormZaposleni").style.display = "block";
						document.getElementById("loginFormStartDiv").style.display = "none";


						// koristimo Window sessionStorage Property za cuvanje key/value parova u browser-u
						// sessionStorage cuva podatke za samo jednu sesiju
						// podaci će se obrisati kad se tab browser-a zatvori
						// (postoji i localStorage koji čuva podatke bez datuma njihovog "isteka")
						// dobavljanje tokena: token = sessionStorage.getItem(data.token);
						//sessionStorage.setItem("token", data.token);
						jwt_token = data.token;
						loadPaketi();
						loadKuriri()
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}


// prikaz autora
function loadPaketi() {
	/* document.getElementById("data").style.display = "block";
	document.getElementById("loginForm").style.display = "none";
	document.getElementById("formDiv").style.display = "block";

	document.getElementById("registerForm").style.display = "none"; */


	// ucitavanje kurira
	var requestUrl = host + port + paketiEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
		
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setPaket);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
};


function loadKuriri() {
	// ucitavanje kurira
	var requestUrl = host + port + kuririEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setKurir);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
}

function showError() {
	var container = document.getElementById("data");
	container.innerHTML = "";

	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var errorText = document.createTextNode("Greska prilikom preuzimanja paketa!");

	h1.appendChild(errorText);
	div.appendChild(h1);
	container.append(div);
}

// metoda za postavljanje autora u tabelu
function setPaket(data) {
	var container = document.getElementById("data");
	container.innerHTML = "";

	console.log(data);

	// ispis naslova
	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var headingText = document.createTextNode("Paketi");
	h1.appendChild(headingText);
	div.appendChild(h1);

	// ispis tabele
	var table = document.createElement("table");
	table.setAttribute("class", "table table-bordered table-striped table-hover");
	var header = createHeader();

	table.append(header);

	var tableBody = document.createElement("tbody");

	for (var i = 0; i < data.length; i++) {
		// prikazujemo novi red u tabeli
		var row = document.createElement("tr");
		// prikaz podataka
		row.appendChild(createTableCell(data[i].posiljalac));
		row.appendChild(createTableCell(data[i].primalac));
		row.appendChild(createTableCell(data[i].tezina));
		row.appendChild(createTableCell(data[i].kurirIme));

		if (jwt_token) {

			row.appendChild(createTableCell(data[i].cenaPostarine));

			// prikaz dugmadi za izmenu i brisanje
			var stringId = data[i].id.toString();

			var buttonDelete = document.createElement("button");
			buttonDelete.setAttribute("class", "btn btn-danger");
			buttonDelete.name = stringId;
			buttonDelete.addEventListener("click", deletePaket);
			var buttonDeleteText = document.createTextNode("Obrisi");
			buttonDelete.appendChild(buttonDeleteText);
			var buttonDeleteCell = document.createElement("td");
			buttonDeleteCell.appendChild(buttonDelete);
			row.appendChild(buttonDeleteCell);



		}

		tableBody.appendChild(row);
	}

	table.appendChild(tableBody);
	div.appendChild(table);


	if (jwt_token) {
		// prikaz forme
		document.getElementById("formDiv").style.display = "block";
	}

	// ispis novog sadrzaja
	container.appendChild(div);
};

function setKurir(data) {
	console.log(data);
	var dropdown = document.getElementById("kurirPaketa");
	for (var i = 0; i < data.length; i++) {
		var option = document.createElement("option");
		option.value = data[i].id;
		var text = document.createTextNode(data[i].ime);
		option.appendChild(text);
		dropdown.appendChild(option);
	}
}

function createHeader() {
	var thead = document.createElement("thead");
	var row = document.createElement("tr");
	row.setAttribute("class", "table-danger");
	//row.appendChild(createTableCell("Id"));

	row.appendChild(createTableCellHead("Posiljalac"));
	row.appendChild(createTableCellHead("Primalac"));
	row.appendChild(createTableCellHead("Tezina(kg)"));
	row.appendChild(createTableCellHead("Kurir"));


	if (jwt_token) {
		row.appendChild(createTableCellHead("Postarina (din)"));

		row.appendChild(createTableCellHead("Delete"));
	}

	thead.appendChild(row);
	return thead;
}
function createTableCellHead(text) {
	var cell = document.createElement("th");
	var cellText = document.createTextNode(text);
	cell.setAttribute("style", "text-align:left;")
	cell.appendChild(cellText);
	return cell;
}

function createTableCell(text) {
	var cell = document.createElement("td");
	var cellText = document.createTextNode(text);
	cell.appendChild(cellText);
	return cell;
}

function submitSearchPaketiiForm() {

	var minimum = document.getElementById("minimum").value;
	var maksimum = document.getElementById("maksimum").value;
	var sendData = {
		"firstValue": minimum,
		"secondValue": maksimum,
	};
	var url = host + port + searchEndpoint;

	console.log("Objekat za slanje");
	console.log(sendData);
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: "POST", headers: headers, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setPaket);
				document.getElementById("searchFormZaposleniForm").reset();
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
	return false;
}
// dodavanje novog paketa
function submitPaketForm() {

	var imePrimaoca = document.getElementById("imePrimaoca").value;
	var imePosiljaoca = document.getElementById("imePosiljaoca").value;
	var tezinaPaketa = document.getElementById("tezinaPaketa").value;
	var kurirPaketa = document.getElementById("kurirPaketa").value;
	var cenaPaketa = document.getElementById("cenaPaketa").value;
	var httpAction;
	var sendData;
	var url;

	// u zavisnosti od akcije pripremam objekat

	httpAction = "POST";
	url = host + port + paketiEndpoint;
	sendData = {
		"posiljalac": imePrimaoca,
		"primalac": imePosiljaoca,
		"tezina": tezinaPaketa,
		"cenaPostarine": cenaPaketa,

		"kuririd": kurirPaketa
	};


	console.log("Objekat za slanje");
	console.log(sendData);
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: httpAction, headers: headers, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200 || response.status === 201) {
				console.log("Successful action");
				formAction = "Create";
				refreshTable();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
	return false;
}

// brisanje autora
function deletePaket() {
	// izvlacimo {id}
	var deleteID = this.name;
	// saljemo zahtev 
	var url = host + port + paketiEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: "DELETE", headers: headers })
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
				refreshTable();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
};



function cancel() {
	formAction = "Create";
	refreshTable();
}
// osvezi prikaz tabele
function refreshTable() {
	// cistim formu
	document.getElementById("imePrimaoca").value = "";
	document.getElementById("imePosiljaoca").value = "";
	document.getElementById("tezinaPaketa").value = "";
	var cenaPaketa = document.getElementById("cenaPaketa").value= "";
	// osvezavam
	//document.getElementById("btnSellers").click();
	loadPaketi();
};

function logout() {
	jwt_token = undefined;
	document.getElementById("info").innerHTML = "";
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginForm").style.display = "block";
	document.getElementById("registerForm").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("btnLogin").style.display = "initial";
	document.getElementById("btnRegister").style.display = "initial";
	document.getElementById("searchFormZaposleni").style.display = "none";
	showLogin();

}


function setupPopper() {

	const button = document.getElementById('btnLogout');
	const tooltip = document.getElementById('tooltip');

	const popperInstance = Popper.createPopper(button, tooltip, {
		modifiers: [
			{
				name: 'offset',
				options: {
					offset: [0, 8],
				},
			},
		],
	});

	function show() {
		// Make the tooltip visible
		tooltip.setAttribute('data-show', '');

		// Enable the event listeners
		popperInstance.setOptions((options) => ({
			...options,
			modifiers: [
				...options.modifiers,
				{ name: 'eventListeners', enabled: true },
			],
		}));

		// Update its position
		popperInstance.update();
	}

	function hide() {
		// Hide the tooltip
		tooltip.removeAttribute('data-show');

		// Disable the event listeners
		popperInstance.setOptions((options) => ({
			...options,
			modifiers: [
				...options.modifiers,
				{ name: 'eventListeners', enabled: false },
			],
		}));
	}

	const showEvents = ['mouseenter', 'focus'];
	const hideEvents = ['mouseleave', 'blur'];

	showEvents.forEach((event) => {
		button.addEventListener(event, show);
	});

	hideEvents.forEach((event) => {
		button.addEventListener(event, hide);
	});

}