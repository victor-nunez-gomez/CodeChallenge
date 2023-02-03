using CodeChallenge.Fees.Interfaces;

namespace CodeChallenge.Fees.Services;

public class FeeService : IFeeService
{
    private static decimal _fee = (decimal)0.5;
    private static int _hour = -1;

    public FeeService()
    {

    }

    /// <summary>
    /// Gets the current fee. 
    /// If the hour has changed, then calculates the new fee by getting a random decimal
    /// between 0 and 2, then multiplying it by the previous fee.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public decimal GetFee()
    {
        if (_hour != DateTime.Now.Hour)
        {
            _hour = DateTime.Now.Hour;

            var random = new Random();
            var randomValue = (decimal)random.NextDouble() * 2;
            _fee *= randomValue;
        };

        return _fee;
    }
}
