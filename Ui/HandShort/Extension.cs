using Core.Domain.Entity.Products;
using Core.ViewModel.Products;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ui.HandShort
{
    public static class Extension
    {

        #region Alerts

        /// <summary>
        /// مقادیر دارای ارور را به ما میدهد
        /// </summary>
        /// <param name="errors">لیست ارور ها</param>
        /// <returns>All Errors ModelState</returns>
        public static string AlertErrorsModel(this ModelStateDictionary errors)
        {
            var error = errors.Values.SelectMany(p => p.Errors).Select(x => x.ErrorMessage).ToList();
            return string.Join(Environment.NewLine, error);
        }

        public static string AlertSuccess()
        {
            return "با موفقیت انجام شد";
        }
        public static string AlertError()
        {
            return "با مشکل مواجه شد";
        }
        public static string AlertNotValue()
        {
            return "مقداری وارد نشده است";
        }
        public static string AlertDuplicate()
        {
            return "مقدار تکراری است";
        }
        public static string AlertNotFound()
        {
            return "پیدا نشد";
        }
        public static string AlertUnKnown()
        {
            return "با پشتیبان تماس بگیرید";
        }
        public static string AlertAddToCard()
        {
            return "به سبد خریدتان اضافه شد";
        }

        public static string AlertDuplicateToCard()
        {
            return "قبلا به سبد خریدتان افزوده شده است";
        }

        public static string AlertDeleteToCard()
        {
            return "از سبد خریدتان حذف شد";
        }

        #endregion

        #region CreateRandomNUmberCode

        public static int Random(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        #endregion

        #region Discount

        public static double Discount(double Price, double DiscountPrice)
        {
            return Convert.ToDouble((((Price - DiscountPrice) / Price) * 100).ToString("0.0"));
        }

        #endregion

    }
}
