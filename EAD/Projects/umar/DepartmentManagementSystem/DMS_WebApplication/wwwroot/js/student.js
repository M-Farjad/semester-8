//$(function () {
//    $("#loadStudents").click(function () {
//        $.get('/api/StudentApi', function (data) {
//            var rows = "";
//            data.forEach(function (s) {
//                rows += `<tr><td>${s.Name}</td><td>${s.Section}</td><td>${s.Session}</td></tr>`;
//            });
//            $("#studentsTable").append(rows);
//        });
//    });
//});

$(function () {
    $("#loadStudents").click(function () {
        $.get('/api/StudentApi', function (data) {
            var rows = "";
            data.forEach(function (s) {
                rows += `<tr>
                    <td>${s.name}</td>
                    <td>${s.regNo}</td>
                    <td>${s.section}</td>
                    <td>${s.session}</td>
                    <td></td>
                </tr>`;
            });
            $("#studentsTable tbody").html(rows);
        });
    });
});