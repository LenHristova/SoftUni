namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RepositoryFilters
    {
        //Public API 
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData,
            string wantedFilter, int studentsToTake)
        {
            //Parses given filter to predicate (if exists)
            switch (wantedFilter)
            {
                case "excellent":
                    FilterAndTake(wantedData, x => x >= 5, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(wantedData, x => x < 5 && x >= 3.5, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(wantedData, x => x < 3.5, studentsToTake);
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
                    break;
            }
        }

        //Filters and display students by given criteria (filter)
        private static void FilterAndTake(Dictionary<string, List<int>> wantedData,
            Predicate<double> givenFilter, int studentsToTake)
        {
            var counterForPrinted = 0;
            foreach (var usernameScores in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                var averageScore = usernameScores.Value.Average();
                var percentageOfFullfillments = averageScore / 100;
                var mark = percentageOfFullfillments * 4 + 2;

                //If average mark meets given criterion -> display student and count him
                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(usernameScores);
                    counterForPrinted++;
                }
            }
        }      
    }
}