using System;
using System.Web.Mvc;

namespace AxadoTest.Api.Infrastructure
{
    public static class ErrorHelper
    {
        public static void SetModelExceptionError(ModelStateDictionary modelState, Exception e)
        {
            var errorMessage = e.Message;
            Exception ex = e.InnerException;

            while (ex != null)
            {
                errorMessage = ex.Message;
                ex = ex.InnerException;
            }

            modelState.AddModelError("generalerror", errorMessage);
        }
    }
}