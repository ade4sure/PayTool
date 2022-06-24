const promotionArrManyView = {
    'categoryId': 0,
    'staffNumber': "0000",
    'ranges': []
};

var table;

let newRange = {};

const xhttp = new XMLHttpRequest();

document.addEventListener("DOMContentLoaded", function () {
    //docReady
    table = document.getElementById("resultTableBody");
    totalrow = document.getElementById("totalMargin");

    console.log("ReadyDoc");

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

    console.log(promotionArrManyView);

    newRange = range.startMonthStr + " To " + range.endMonthStr + " (" + range.paid.grade + ":" + range.paid.step + " || " + + range.expected.grade + ":" + range.expected.step + ")";

    addLi();

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

    response.ranges.forEach(addRow);
        
    totalrow.innerHTML = response.totalMargin;

}

function addLi() {
    var ul = document.getElementById("rangeList");
    var li = document.createElement("li");
    li.appendChild(document.createTextNode(newRange));
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

    // Add some text to the new cells:
    cell1.innerHTML = item.startMonthStr;
    cell2.innerHTML = item.paid.grade + ":" + item.paid.step;
    cell3.innerHTML = item.expected.grade + ":" + item.expected.step;
    cell4.innerHTML = item.endMonthStr;
    cell5.innerHTML = item.margin;

}

