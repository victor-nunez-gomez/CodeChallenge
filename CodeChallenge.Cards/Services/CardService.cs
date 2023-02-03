using CodeChallenge.Cards.Exceptions;
using CodeChallenge.Cards.Interfaces;
using CodeChallenge.Cards.ViewModel;
using CodeChallenge.DataAccess.Entities;
using CodeChallenge.DataAccess.Interfaces;
using CodeChallenge.Fees.Interfaces;

namespace CodeChallenge.Cards.Services;

public class CardService : ICardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFeeService _feeService;

    public CardService(IUnitOfWork unitOfWork, IFeeService feeService)
    {
        _unitOfWork = unitOfWork;
        _feeService = feeService;
    }

    public async Task<UpsertViewModel<Card>> InsertCard(Card card)
    {
        try
        {
            if (card.CardNumber.Length != 15)
                throw new CustomException("Card number must have 15 character length");

            if (card.Balance == default(int))
                throw new CustomException("Card balance must be greater than 0");

            var existingCard = _unitOfWork.CardRepository
                .GetAll()
                .Where(c => c.CardNumber == card.CardNumber)
                .FirstOrDefault();

            if (existingCard != null)
                throw new CustomException("The Card you are trying to add already exists");

            await _unitOfWork.CardRepository.AddAsync(card);
            await _unitOfWork.SaveChangesAsync();

            return CardResponse(card, false, string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            return CardResponse(card, true, "001", ex.Message);
        }
    }

    public async Task<UpsertViewModel<Payment>> MakePayment(int idCard, decimal amount)
    {
        Payment payment = null;

        try
        {
            var existingCard = await _unitOfWork.CardRepository.GetByIdAsync(idCard);

            if (existingCard == null)
                return PaymentResponse(payment, true, "404", "There is not such an existing Card with the Id you provided");

            var fee = _feeService.GetFee();

            if (amount == 0)
                throw new CustomException("The amount must be greater than 0");

            if ((amount + fee) > existingCard.Balance)
                throw new CustomException("Payment can not be done due to insuficient funds.");


            existingCard.Balance -= (amount + fee);

            payment = new Payment
            {
                CardId = idCard,
                Amount = amount,
                Fee = fee,
                TransactionDate = DateTime.Now
            };

            _unitOfWork.CardRepository.Update(existingCard);
            await _unitOfWork.paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return PaymentResponse(payment, false, string.Empty, string.Empty);
        }
        catch (Exception ex) 
        {
            return PaymentResponse(payment, true, "002", ex.Message);
        }
    }

    public async Task<Card> GetCard(int idCard)
    {
        return await _unitOfWork.CardRepository
            .GetByIdAsync(idCard);
    }

    private UpsertViewModel<Card> CardResponse(Card card, bool error = false, string errorCode = null, string errorMessage = null)
    {
        return new UpsertViewModel<Card>
        {
            Entity = card,
            Error = new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            },
            HasError = error
        };
    }

    private UpsertViewModel<Payment> PaymentResponse(Payment payment, bool error = false, string errorCode = null, string errorMessage = null)
    {
        return new UpsertViewModel<Payment>
        {
            Entity = payment,
            Error = new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            },
            HasError = error
        };
    }

}