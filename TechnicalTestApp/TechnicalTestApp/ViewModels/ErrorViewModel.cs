using System;

namespace TechnicalTestApp.ViewModels
{
    /// <summary>
    /// ViewModel for handling errors containing the information to be passed to the view
    /// </summary>
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public string RouteOfException { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
