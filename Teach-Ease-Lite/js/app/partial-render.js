var arrViewList = [
    {
        contentName: "CourseListPanel", URL: "/Viewer/CourseListPanel", BackContentName: "", BackContentID: ""
    },
    {
        contentName: "CourseList", URL: "/Viewer/CourseList", BackContentName: "", BackContentID: ""
    },
    {
        contentName: "CourseCategories", URL: "/Viewer/CourseCategories", BackContentName: "", BackContentID: ""
    },
    {
        contentName: "CourseDetail", URL: "/Viewer/CourseDetail", BackContentName: "CourseListPanel", BackContentID: "divMainContent"
    }
];
var backContentName = '';
var backContentID = '';
function loadPage(contentID, contentName, parms) {
    var callDetail = getURL(contentName);
    $.ajax({
        type: "POST",
        url: callDetail.URL, //"/Home/Details",
        data: JSON.stringify(parms),//'{customerId: "' + customerId + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $('#' + contentID).html(response);
            //$('#btnBack').html(response);
            //$('#dialog').dialog('open');
            backContentName = callDetail.BackContentName;
            backContentID = callDetail.BackContentID;
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function getURL(contentName) {
    var foundPageDetail = $.grep(arrViewList, function (v) {
        return v.contentName === contentName;
    });
    if (foundPageDetail != undefined && foundPageDetail.length>0) {
        return foundPageDetail[0];
    }
    return { contentName: "", URL: "", BackContentName: "" };
}

function clickBack() {
   loadPage(backContentID, backContentName, '');
}