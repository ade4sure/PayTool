let arrIndex = 0;

const extras = {
    'isProffesional': false,
    'isCallDutyNurse': false,
    'isCallDutyOthers': false,
    'isCallDutyASUU': false,
    'isShiftDutyNurse': false,
    'isShiftDutyOthers': false,
}

let promotionArrManyView = {
    'categoryId': 0,
    'staffNumber': "0000",
    'staffName': "",
    'staffStatus': "",
    'staffUnit': "",
    'ranges': [],
    'extras': extras
}

var table;



const xhttp = new XMLHttpRequest();

document.addEventListener("DOMContentLoaded", function () {
    //docReady
    table = document.getElementById("resultTableBody");
    totalrow = document.getElementById("totalMargin");

    //console.log("ReadyDoc");

});

function addRange() {
    var startMnt = document.getElementById("startMonth").value;
    var endMnt = document.getElementById("endMonth").value;

    const range = {
        'startMonthStr': startMnt,
        'endMonthStr': endMnt,
        'paid': {
            'grade': document.getElementById("paidGrade").value,
            'step': document.getElementById("paidStep").value
        },
        'expected': {
            'grade': document.getElementById("expectedGrade").value,
            'step': document.getElementById("expectedStep").value
        }
    };

    promotionArrManyView.ranges.push(range);

    promotionArrManyView.categoryId = document.getElementById("categoryId").value;
    promotionArrManyView.staffNumber = document.getElementById("staffNumber").value;
    promotionArrManyView.staffName = document.getElementById("staffName").value;
    promotionArrManyView.staffStatus = document.getElementById("staffStatus").value;
    promotionArrManyView.staffUnit = document.getElementById("staffUnit").value;



    console.log(promotionArrManyView);

    const newRange = range.startMonthStr + " To " + range.endMonthStr
        + " (" + range.paid.grade + ":"
        + range.paid.step + " || " + + range.expected.grade
        + ":" + range.expected.step + ") ";


    addLi(newRange);

    document.getElementById("paidGrade").value = ""
    document.getElementById("paidStep").value = ""

    document.getElementById("expectedGrade").value = ""
    document.getElementById("expectedStep").value = ""

}

async function postData() {
    const obj = await fetch("/Home/PromoArrears", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(promotionArrManyView)
    });

    // console.log(JSON.stringify(promotionArrManyView));

    const response = await obj.json();

    console.log("ResponseData = ");
    console.log(response);

    document.getElementById("resultTableBody").innerHTML = "";

    response.ranges.forEach(addRow);

    totalrow.innerHTML = "<strong>" + response.totalMargin + "</strong>";

}

function addLi(nuRange) {
    var ul = document.getElementById("rangeList");
    var li = document.createElement("li");

    //console.log(nuRange.startMonthStr);

    if (typeof nuRange.startMonthStr != 'undefined') {
        nuRange = nuRange.startMonthStr + " To " + nuRange.endMonthStr
            + " (" + nuRange.paid.grade + ":"
            + nuRange.paid.step + " || " + + nuRange.expected.grade
            + ":" + nuRange.expected.step + ") ";
    }

    li.appendChild(document.createTextNode(nuRange));
    var button = document.createElement("a");
    button.innerHTML = "X";

    button.setAttribute("class", " btn btn-danger btn-sm mb-2");
    button.setAttribute("onclick", "removeElement(" + arrIndex + ")");

    li.appendChild(button);
    ++arrIndex;
    ul.appendChild(li);
}

function addRow(item) {
    // Create an empty <tr> element and add it to the 1st position of the table:
    var row = table.insertRow(0);

    // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);
    var cell7 = row.insertCell(6);

    // Add some text to the new cells:
    cell1.innerHTML = item.startMonthStr;
    //cell2.innerHTML = item.paid.grade + ":" + item.paid.step;
    cell2.innerHTML = `${item.paid.grade} : ${item.paid.step} (${item.paid.amount})`;
   // cell3.innerHTML = item.expected.grade + ":" + item.expected.step;
    cell3.innerHTML = `${item.expected.grade} : ${item.expected.step} (${item.expected.amount})`;
    cell4.innerHTML = item.endMonthStr;
    cell5.innerHTML = item.numberOfMonths;
    cell6.innerHTML = item.payMargin;
    cell7.innerHTML = item.rangeAmount;

}

function addProfessional() {
    var optionProf = document.getElementById("isProff");
    promotionArrManyView.extras.isProffesional = optionProf.checked ? true : false;
    //console.log(promotionArrManyView);
}

function addCallDutyNurses() {
    var optionCall = document.getElementById("isCallNurses");
    promotionArrManyView.extras.isCallDutyNurse = optionCall.checked ? true : false;
    //console.log(promotionArrManyView);
}
function addCallDutyOthers() {
    var optionCall = document.getElementById("isCallOthers");
    promotionArrManyView.extras.isCallDutyOthers = optionCall.checked ? true : false;
    //console.log(promotionArrManyView);
}
function addCallDutyASUU() {
    var optionCall = document.getElementById("isCallASUU");
    promotionArrManyView.extras.isCallDutyASUU = optionCall.checked ? true : false;
    //console.log(promotionArrManyView);
}

function addShiftDutyNurses() {
    var optionShift = document.getElementById("isShiftNurses");
    promotionArrManyView.extras.isShiftDutyNurse = optionShift.checked ? true : false;
    //console.log(promotionArrManyView);
}function addShiftDutyOthers() {
    var optionShift = document.getElementById("isShiftOthers");
    promotionArrManyView.extras.isShiftDutyOthers = optionShift.checked ? true : false;
    //console.log(promotionArrManyView);
}

function removeElement(indx) {
    promotionArrManyView.ranges.splice(indx, 1);
    console.log(promotionArrManyView);
    reRenderUL()
}

function reRenderUL() {
    var ul = document.getElementById("rangeList");
    ul.innerHTML = "";
    arrIndex = 0;
    promotionArrManyView.ranges.forEach(addLi);

}

function reRenderUIinput() {
    promotionArrManyView.categoryId = promotionArrManyView.categoryId === 0 ? 1 : promotionArrManyView.categoryId;
    document.getElementById("categoryId").value = promotionArrManyView.categoryId;
    document.getElementById("staffName").value = promotionArrManyView.staffName;
    document.getElementById("staffStatus").value = promotionArrManyView.staffStatus;
    document.getElementById("staffUnit").value = promotionArrManyView.staffUnit;

    document.getElementById("isProff").checked = promotionArrManyView.extras.isProffesional;
    document.getElementById("isCallNurses").checked = promotionArrManyView.extras.isCallDutyNurse;
    document.getElementById("isCallOthers").checked = promotionArrManyView.extras.isCallDutyOthers;
    document.getElementById("isCallASUU").checked = promotionArrManyView.extras.isCallDutyASUU;
    document.getElementById("isShiftNurses").checked = promotionArrManyView.extras.isShiftDutyNurse;
    document.getElementById("isShiftOthers").checked = promotionArrManyView.extras.isShiftDutyOthers;

    console.log(promotionArrManyView);

    reRenderUL()
}

async function getStaffPromo() {
    var stfNum = document.getElementById("staffNumber").value;

    const obj = await fetch("/Home/GetStaffPromo?staffNumber=" + stfNum, {
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    });

    const response = await obj.json();

    promotionArrManyView = response;

    //console.log(promotionArrManyView);

    reRenderUIinput();

}

