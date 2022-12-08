using sm_repetition_algorithm.BLL.DTOs;
using sm_repetition_algorithm.BLL.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sm_repetition_algorithm.BLL.Logic
{
    public class SuperMemo2Algorithm : ISuperMemo2Algorithm
    {
        public void CalculateSuperMemo2Algorithm(FlashCard card, int quality)
        {
            if(quality == 0 || quality > 5)
            {
                throw new Exception("The quality isn't within 0-5");
            }

            //retrieve the stored values (default values if new cards)
            int repetitions = card.GetRepetitions();
            float easiness = card.GetEasinessFactor();
            int interval = card.GetInterval();

            // easiness factor
            easiness = (float)Math.Max(1.3, easiness + 0.1 - (5.0 - quality) * (0.08 + (5.0 - quality) * 0.02));

            // repetitions
            if (quality < 3)
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
