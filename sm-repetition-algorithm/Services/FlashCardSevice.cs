using Microsoft.EntityFrameworkCore;
using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Models;
using sm_repetition_algorithm.DAL.DataAccess;
using sm_repetition_algorithm.DAL.Entitis;
using sm_repetition_algorithm.DTOs;
using System.Security.Claims;

namespace sm_repetition_algorithm.BLL.Logic
{
    public class FlashCardSevice : IFlashCardSevice
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FlashCardSevice(DataContext repetitionAlgorithmContext, IHttpContextAccessor httpContextAccessor) 
        {
            _dataContext = repetitionAlgorithmContext;
            _httpContextAccessor = httpContextAccessor;
    }
        public async Task Learn(FlashCard card)
        {
            if (card.Quality <= 0 || card.Quality > 5)//Quality also known as quality of assessment.
                                                      //This is how difficult (as defined by the user) a flashcard is.
                                                      //The scale is from 0 to 5.
            {
                throw new Exception("The quality isn't within 0-5");
            }

            var repetitionData = await _dataContext.Repetitions.FindAsync(card.TermId);

            int repetitions;
            int interval;
            float easiness;
            if (repetitionData.Repetitions == 0)//Default values
            { 
                repetitions = 0;
                interval = 1;
                easiness = 2.5F;
            }
            else
            {
                //retrieve the stored values 
                repetitions = repetitionData.Repetitions; //this is the number of times a user sees a flashcard.
                                                //0 means they haven't studied it yet,
                                                //1 means it is their first time, and so on.
                                                //It is also referred to as n in some of the documentation.
                easiness = repetitionData.Easiness;
                interval = repetitionData.Interval; // this is the length of time (in days) between repetitions.
                                          // It is the "space" of spaced repetition.
            }

            // easiness factor
            easiness = (float)Math.Max(1.3, easiness + 0.1 - (5.0 - card.Quality) * (0.08 + (5.0 - card.Quality) * 0.02));

            // repetitions
            if (card.Quality < 3)
            {
                repetitions = 0;
            }
            else
            {
                repetitions += 1;
            }

            // interval
            if (repetitions <= 1)
            {
                interval = 1;
            }
            else if (repetitions == 2)
            {
                interval = 6;
            }
            else
            {
                interval = System.Convert.ToInt32(Math.Round(interval * easiness));
            }

            // next practice 
            int millisecondsInDay = 60 * 60 * 24 * 1000;
            long now = DateTime.Now.Millisecond;
            long nextPracticeDate = now + millisecondsInDay * interval;//This is the date/time of when the flashcard comes due to review again.

            // Store the nextPracticeDate in the database
            // ...
        }


    }
}
