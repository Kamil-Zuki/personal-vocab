using sm_repetition_algorithm.BLL.Models;
using sm_repetition_algorithm.DAL.Entitis;
using sm_repetition_algorithm.DTOs;


namespace sm_repetition_algorithm.BLL.Interfeces
{
    public interface IFlashCardSevice
    {
        Task Learn(FlashCard card);

    }
}
