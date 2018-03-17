using System;
using System.Collections.Generic;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Repository
{
    public class RepositoryFilter
    {
        //Public API 
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks,
            string wantedFilter, int studentsToTake)
        {
            //Parses given filter to predicate (if exists)
            switch (wantedFilter)
            {
                case "excellent":
                    FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.5, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(studentsWithMarks, x => x < 3.5, studentsToTake);
                    break;
                default:
                    throw new ArgumentException(ExceptionMessages.INVALID_STUDENT_FILTER);   
            }
        }

        //Filters and display students by given criteria (filter)
        private void FilterAndTake(Dictionary<string, double> studentsWithMarks,
            Predicate<double> givenFilter, int studentsToTake)
        {
            var counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                //If average mark meets given criterion -> display student and count him
                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(studentMark);
                    counterForPrinted++;
                }
            }
        }      
    }
}