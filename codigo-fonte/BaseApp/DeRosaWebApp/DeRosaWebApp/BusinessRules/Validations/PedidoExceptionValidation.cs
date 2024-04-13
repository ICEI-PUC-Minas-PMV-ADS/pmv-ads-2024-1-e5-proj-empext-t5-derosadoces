namespace DeRosaWebApp.BusinessRules.Validations
{
    public class PedidoExceptionValidation : Exception
    {
        public PedidoExceptionValidation(string error) : base(error) { }
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new PedidoExceptionValidation(error);
            }
        }
    }
}
