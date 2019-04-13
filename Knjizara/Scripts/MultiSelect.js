// selektovanje vise zanrova 
var options = [].slice.call(document.querySelectorAll("option"));

options.forEach(function (element) {
    element.addEventListener("mousedown",
        function (e) {
            e.preventDefault();
            element.parentElement.focus();
            this.selected = !this.selected;
            return false;
        }
        , false
    );
});