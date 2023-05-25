using sm_repetition_algorithm.BLL.Interfeces;
using sm_repetition_algorithm.BLL.Models;

namespace sm_repetition_algorithm.BLL.Logic
{
    public class SuperMemo2Algorithm : ISuperMemo2Algorithm
    {
        public async Task CalculateSuperMemo2Algorithm(FlashCard card)
        {
            if (card.Quality == 0 || card.Quality > 5)
            {
                throw new Exception("The quality isn't within 0-5");
            }

            //retrieve the stored values (default values if new cards)
            int repetitions = card.Repetitions;
            float easiness = card.EasinessFactor;
            int interval = card.Interval;

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
            long nextPracticeDate = now + millisecondsInDay * interval;

            // Store the nextPracticeDate in the database
            // ...
        }
    }
}
