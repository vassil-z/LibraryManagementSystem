$(document).ready(function () {
    $('.startDate').datepicker({
        dateFormat: "mm/dd/yy",
        onselect: function (dateText, inst) {
            inst = $('.startDate').datepicker();
            inst.val(dateTect);
        },
        onClose: function (selectedDate) {
            $('.endDate').datepicker('option', 'minDate', selectedDate);
        }
    });

    $('.endDate').datepicker({
        dateFormat: "mm/dd/yy",
        onselect: function (dateText, inst) {
            inst = $('.endDate').datepicker();
            inst.val(dateTect);
        },
        onClose: function (selectedDate) {
            $('.startDate').datepicker('option', 'mmaxDate', selectedDate);
        }
    });
});