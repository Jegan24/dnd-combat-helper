using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Engine
{
    public static class Functions
    {
        public static Random random = new Random();

        public static bool CoinFlip()
        {
            bool result;
            int randomNumber = random.Next(1, 100);
            result = randomNumber > 50;
            return result;
        }

        public static int RollD20(int modifier)
        {
            return random.Next(1, 20) + modifier;            
        }

        public static int RollD20WithAdvantage(int modifier)
        {
            int resultA = RollD20(modifier);
            int resultB = RollD20(modifier);
            int finalResult;

            if(resultA >= resultB)
            {
                finalResult = resultA;
            }
            else
            {
                finalResult = resultB;
            }

            return finalResult;
        }

        public static int RollD20WithDisadvantage(int modifier)
        {
            int resultA = RollD20(modifier);
            int resultB = RollD20(modifier);
            int finalResult;

            if (resultA <= resultB)
            {
                finalResult = resultA;
            }
            else
            {
                finalResult = resultB;
            }

            return finalResult;
        }
    }
}
