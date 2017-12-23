using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QuoteBook.Constants;

namespace QuoteBook.Web.Infrastructure.Extentions
{
    public static class TempDataDictionaryExtentions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[WebConstants.TempDataSuccessMessageKey] = message;
        }
    }
}
