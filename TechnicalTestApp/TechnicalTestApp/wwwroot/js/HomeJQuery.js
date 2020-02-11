
/*TODO: finish this off*/
$.ajax({
    url: '/Home/Address/Id',
    success: function (data) { alert(data); },
    statusCode: {
        404: function (content) { alert('cannot find resource'); },
        500: function (content) { alert('internal server error'); }
    },
    error: function (req, status, errorObj) {
        // handle status === "timeout"
        // handle other errors
    }
});