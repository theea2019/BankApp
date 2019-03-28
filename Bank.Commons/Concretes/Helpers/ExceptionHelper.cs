using System;
using System.Text;

namespace Bank.Commons.Concretes.Helpers
{
    /// <summary>
    ///     <english>
    ///         This static class helps to create log strings from Exceptions.
    ///     </english>
    ///     <turkish>
    ///         Bu statik sınıf Exception(hata) sonuçlarından loglama ifadeleri oluşturmaya yardımcı olur.
    ///     </turkish>
    /// </summary>
    public static class ExceptionHelper
    {
        private const string StrCoreErrorLineSeparator = "--------------------[Core Exception]--------------------";
        private const string StrWrapErrorLineSeparator = "--------------------[Wrap Exception]--------------------";
        private const string StrTab = "\t";

        public static string ExceptionToString(Exception ex)
        {
            var sb = new StringBuilder();

            do
            {
                if (ex.InnerException == null)
                {
                    sb.AppendLine(StrCoreErrorLineSeparator);
                    sb.AppendLine("Source\t\t: " + ex.Source.Trim());
                    sb.AppendLine("Method\t\t: " + ex.TargetSite.Name);
                    sb.AppendLine("Date\t\t: " + DateTime.Now.ToLongTimeString());
                    sb.AppendLine("Time\t\t: " + DateTime.Now.ToShortDateString());
                    sb.AppendLine("Error\t\t: " + ex.Message.Trim());
                    sb.AppendLine("Stack Trace\t: " + ex.StackTrace.Trim());
                }
                else
                {
                    sb.AppendLine(StrTab + StrWrapErrorLineSeparator);
                    sb.AppendLine(StrTab + "Source\t\t: " + ex.Source.Trim());
                    sb.AppendLine(StrTab + "Method\t\t: " + ex.TargetSite.Name);
                    sb.AppendLine(StrTab + "Date\t\t: " + DateTime.Now.ToLongTimeString());
                    sb.AppendLine(StrTab + "Time\t\t: " + DateTime.Now.ToShortDateString());
                    sb.AppendLine(StrTab + "Error\t\t: " + ex.Message.Trim());
                    sb.AppendLine(StrTab + "Stack Trace\t: " + ex.StackTrace.Trim());

                    ex = ex.InnerException;
                }
            } while (ex.InnerException != null);

            return sb.ToString();
        }
    }
}
