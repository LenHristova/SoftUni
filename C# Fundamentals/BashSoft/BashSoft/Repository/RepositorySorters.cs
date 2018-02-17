namespace BashSoft
{
    using System.Collections.Generic;
    using System.Linq;

    public static class RepositorySorters
    {
        //Public API
        public static void OrderAndTake(Dictionary<string, List<int>> wantedData,
        string comparisson, int studentsToTake)
        {
            comparisson = comparisson.ToLower();
            switch (comparisson)
            {
                //Order students ascending by grades and take wanted count of them
                case "ascending":
                    PrintStudents(wantedData
                        .OrderBy(x => x.Value.Sum())
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;
                //Order students descending by grades and take wanted count of them
                case "descending":
                    PrintStudents(wantedData
                        .OrderByDescending(x => x.Value.Sum())
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
                    break;
            }
        }

        private static void PrintStudents(Dictionary<string, List<int>> studentSorted)
        {
            foreach (var keyValuePair in studentSorted)
            {
                OutputWriter.PrintStudent(keyValuePair);
            }
        }
    }
}
