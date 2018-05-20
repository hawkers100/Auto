window.common = window.common || {};
common.showGeneralError = function() {
    alert("Произошла ошибка. Попробуйте позже.");
}
common.fixDate = function (date) {
    if (!date)
        return date;
    date.setTime(date.getTime() - date.getTimezoneOffset() * 60 * 1000);
    return date.toJSON().replace("Z", "");
}