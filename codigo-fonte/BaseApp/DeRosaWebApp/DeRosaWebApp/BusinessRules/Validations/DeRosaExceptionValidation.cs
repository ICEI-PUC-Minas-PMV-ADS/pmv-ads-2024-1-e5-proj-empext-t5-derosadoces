namespace DeRosaWebApp.BusinessRules.Validations
{
    public class DeRosaExceptionValidation : Exception
    {
        public DeRosaExceptionValidation(string error) : base(error) { }
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DeRosaExceptionValidation(error);
            }
        }
    }
}
