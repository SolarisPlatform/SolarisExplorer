function setPage(page) {
    $("#pageNumber").val(page);
    $("#pagingForm").submit();
    return false;
}
function setPageSize(obj) {
    $("#pageNumber").val(1);
    $("#pageSize").val($(obj).val());
    $("#pagingForm").submit();
    return false;
}

$(document).ready(function() {
    $(".pageSizeControl").val($("#pageSize").val());
});