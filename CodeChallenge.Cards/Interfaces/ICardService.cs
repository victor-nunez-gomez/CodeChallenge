using CodeChallenge.Cards.ViewModel;
using CodeChallenge.DataAccess.Entities;

namespace CodeChallenge.Cards.Interfaces;

public interface ICardService
{
    Task<UpsertViewModel<Card>> InsertCard(Card card);

    Task<UpsertViewModel<Payment>> MakePayment(int idCard, decimal amount);

    Task<Card> GetCard(int idCard);

}
