namespace CodeChallenge.Cards.ViewModel;

public class UpsertViewModel<T>
{
    public bool HasError { get; set; }

    public ErrorViewModel Error { get; set; }

    public T Entity { get; set; }
}
