//On ready function fires when page has finished loading
$(document).ready(function () {

    //Click Handler for elements with addressBtn class
    $(".addressBtn").click(function (event)
    {
        var customerId = event.target.name;        
        GetCustomerAddressFromServer(customerId);
    });

});

//Make an AJAX call to the Web Api controller to get the address for the specified customer
//Parameters:
//  - customerId: the ID of the customer to look up
function GetCustomerAddressFromServer(customerId) {    
    $.ajax({
        url: '/api/CustomerAddress/' + customerId,
        success: function (data)
        {     
            var name = data[0];
            var address = data[1];

            //Set the details in the modal
            $('#customerModalName').html(name);
            $('#customerModalAddress').html(address);
        },
        statusCode: {
            404: function (content) {
                alert('AJAX error: server URL not found! See the console for more information.');
                console.log(content);
            },
            500: function (content) {
                alert('AJAX error: internal server error. See the console for more information.');
                console.log(content);
            }
        },
        error: function (req, status, errorObj)
        {
            alert("An error occurred communicating with the server, check the log for more information");
            console.log(errorObj, req, status);
        }
    });
}



