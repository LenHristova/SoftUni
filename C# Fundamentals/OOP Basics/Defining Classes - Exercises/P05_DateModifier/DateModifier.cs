namespace P05_DateModifier
{
    using System;
    using System.Linq;

    public class DateModifier
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public int CalcDifference(string firstDateString, string secondDateString)
        {
            _startDate = ConvertStringToDate(firstDateString);
            _endDate = ConvertStringToDate(secondDateString);

            return Math.Abs((_endDate - _startDate).Days);
        }

        private static DateTime ConvertStringToDate(string dateString)
        {
            var dateParams = dateString.Split().Select(int.Parse).ToArray();
            return new DateTime(dateParams[0], dateParams[1], dateParams[2]);
        }
    }
}