namespace E_CommerceApp.ViewModel
{
    public class CheckoutViewModel
    {
        public string StripePublishableKey { get; set; } = string.Empty;
        public int Amount { get; set; }
    }
}
