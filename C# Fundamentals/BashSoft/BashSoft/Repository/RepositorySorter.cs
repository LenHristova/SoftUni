using System.Collections.Generic;
using System.Linq;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class RepositorySorter
    {
        //Public API
        public void OrderAndTake(Dictionary<string, double> studentsMarks,
        string comparisson, int studentsToTake)
        {
            comparisson = comparisson.ToLower();
            switch (comparisson)
            {
                //Order students ascending by grades and take wanted count of them
                case "ascending":
                    PrintStudents(studentsMarks
                        .OrderBy(x => x.Value)
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;
                //Order students descending by grades and take wanted count of them
                case "descending":
                    PrintStudents(studentsMarks
                        .OrderByDescending(x => x.Value)
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
                    break;
            }
        }

        private void PrintStudents(Dictionary<string, double> studentSorted)
        {
            foreach (var keyValuePair in studentSorted)
            {
                OutputWriter.PrintStudent(keyValuePair);
            }
        }
    }
}