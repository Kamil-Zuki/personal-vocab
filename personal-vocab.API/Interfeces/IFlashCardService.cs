using personal_vocab.BLL.Models;


namespace personal_vocab.Interfeces;

public interface IFlashCardService
{
    Task Learn(FlashCard card);
}
