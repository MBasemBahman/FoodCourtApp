namespace Dashboard.Helpers
{
    public class ExceptionHandler
    {
        private readonly LocalizationService _Localizer;

        public ExceptionHandler(LocalizationService Localizer)
        {
            _Localizer = Localizer;
        }

        public string GetException(Exception ex)
        {
            string Error = ex.Message;
            if (ex.InnerException != null)
            {
                Error = ex.InnerException.Message;
            }
            if (Error.Contains("duplicate key"))
            {
                Error = "Cannot insert duplicate data";
            }
            return _Localizer.Get(Error);
        }
    }
}
