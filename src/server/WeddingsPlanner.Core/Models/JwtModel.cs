namespace WeddingsPlanner.Core.Models
{
    public class JwtModel
    {
        public JwtModel()
        {}

        public JwtModel(string tokenString)
        {
            TokenString = tokenString;
        }

        public string TokenString { get; set; }
    }
}
