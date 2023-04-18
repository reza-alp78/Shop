using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ui.HandShort
{
    public static class Extension
    {
        /// <summary>
        /// مقادیر دارای ارور را به ما میدهد
        /// </summary>
        /// <param name="errors">لیست ارور ها</param>
        /// <returns>All Errors ModelState</returns>
        public static List<string> ErrorsModel(this ModelStateDictionary errors)
        {
            return errors.Values.SelectMany(p => p.Errors).Select(x => x.ErrorMessage).ToList();
        }


    }
}
