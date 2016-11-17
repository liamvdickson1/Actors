console.log("ldTest");
$.get("http://localhost:2004/api/values/1", function (data) { console.log(data); });
for (var i = 0;  i < 100; i++) {
    console.log(i);
    $.get("http://localhost:2004/api/values/" + i, function (data) { console.log(data); });
}