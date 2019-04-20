$(document).ready(function () {
    var searchValue = document.getElementById("search").value;
    var links = Array.from(document.querySelectorAll('li.page-item a'));
    links.forEach(link => {
        link.href = link.href + '&search=' + searchValue;
    });
});