using personal_vocab.BLL.Models;
using personal_vocab.DAL.Entitis;
using personal_vocab.DTOs;


namespace personal_vocab.BLL.Interfeces
{
    public interface IFlashCardSevice
    {
        Task Learn(FlashCard card);

    }
}
