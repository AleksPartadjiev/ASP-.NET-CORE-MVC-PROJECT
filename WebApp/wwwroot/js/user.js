
function CheckUsernameExist() {
    const name = document.getElementById("username").value;
    let url = `/api/users/CheckUsernameExist?username=${encodeURIComponent(name)}`;
    fetch(url)
        .then(response => {
            console.log(response)
            if (!response.ok) {
                throw new Error("Network response error!");
            }
            console.log(response.json());
            return response.json(); //���������� �������� � JSON
        })
        .then(data => {
            console.log(data);
            if (data) {
                document.getElementById("username-existing").innerHTML = "Username is already used!"
            }
            else {
                document.getElementById("username-existing").innerHTML = "";
            }
        })
        .catch(error => console.error("Error:" , error))
}
function CheckEmailExist() {
    const email = document.getElementById("email").value;
    let url = `/api/users/CheckEmailExist?email=${encodeURIComponent(email)}`;
    fetch(url)
        .then(response => {
            console.log(response)
            if (!response.ok) {
                throw new Error("Network response error!");
            }
            return response.json(); //���������� �������� � JSON
        })
        .then(data => {
            console.log(data);
            if (data) {
                document.getElementById("email-existing").innerHTML = "Email is already used!"
            }
            else {
                document.getElementById("email-existing").innerHTML = "";
            }
        })
        .catch(error => console.error("Error:", error))
}
const minimumLengthOfName = 5;
const minimumLengthOfMailAndPass = 8;
document.getElementById("username").addEventListener("input", function () {
    var name = document.getElementById("username").value;
    name.length < minimumLengthOfName ? document.getElementById("validation-user").innerText = "Minimum 5 symbols!" :
        document.getElementById("validation-user").innerText = "";
    if (name.length >= minimumLengthOfName) {
        CheckUsernameExist();

    }
})
document.getElementById("password").addEventListener("input", function () {
    var password = document.getElementById("password").value;
    password.length < minimumLengthOfMailAndPass ? document.getElementById("validation-pass").innerText = "Minimum 8 symbols!" :
        document.getElementById("validation-pass").innerText = "";
})
document.getElementById("email").addEventListener("input", function () {
    var email = document.getElementById("email").value;
    email.length < minimumLengthOfMailAndPass ? document.getElementById("validation-email").innerText = "Minimum 8 symbols!" :
        document.getElementById("validation-email").innerText = "";
    if (email.length >= minimumLengthOfMailAndPass) {
        CheckEmailExist();
    }  
})

document.getElementById("register-user-button").addEventListener("click", function (event) {
    event.preventDefault();
    const user = {
        name: document.getElementById("username").value,
        password: document.getElementById("password").value,
        email: document.getElementById("email").value,
    };
    console.log(user);
    //fetch � �������, ����� �� �������� �� ���������� �� ���������� ������ �� �������.
    fetch('/api/users', {
        method: "POST",
        headers: { // ���������� ����� ���������� �� �������� �� ������� ����� ��������� 
            'Content-Type': 'application/json',//Content-Type � HTTP ��������, ����� ������ �� ������� ����� ��� ����� �� ���������.
            //���������� 'application/json' ��������, �� �������, ����� ���������, �� � JSON ������
        },
        body: JSON.stringify(user), // ����������� ������ � JSON ������
    })
        .then(response => {

            if (response.ok) {
                document.getElementById("registration-message").innerText = ("Successful registration!");
            }
            else {
                document.getElementById("registration-message").innerText = ("Try again!")
            }
        });
});
