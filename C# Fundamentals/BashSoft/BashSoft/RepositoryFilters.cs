namespace BashSoft
{
    using System;
    using System.Collections.Generic;

    public static class RepositoryFilters
    {
        //Public API 
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData,
            string wantedFilter, int studentsToTake)
        {
            //Parses given filter to predicate (if exists)
            switch (wantedFilter)
            {
                case "exellent":
                    FilterAndTake(wantedData, ExellentFilter, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(wantedData, AverageFilter, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(wantedData, PoorFilter, studentsToTake);
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

                var averageMark = Average(usernameScores.Value);

                //If average mark meets given criterion -> display student and count him
                if (givenFilter(averageMark))
                {
                    OutputWriter.DisplayStudent(usernameScores);
                    counterForPrinted++;
                }
            }
        }

        //Finds marks that are equal or higher then 5.0 (top mark is 6.0)
        private static bool ExellentFilter(double mark)
        {
            return mark >= 5.0;
        }

        //Finds marks that are equal or higher then 3.5 (top mark is 6.0)
        private static bool AverageFilter(double mark)
        {
            return mark >= 3.5;
        }

        //Finds marks that are lower then 3.5
        private static bool PoorFilter(double mark)
        {
            return mark < 3.5;
        }

        //Finds average mark based on all scores on tasks
        private static double Average(List<int> scoresOnTasks)
        {
            var totalScore = 0;
            foreach (var score in scoresOnTasks)
            {
                totalScore += score;
            }

            var percentageOfAll = totalScore / (scoresOnTasks.Count * 100);
            var mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}