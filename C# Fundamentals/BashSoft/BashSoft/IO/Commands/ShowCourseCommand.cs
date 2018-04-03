using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("show")]
    public class ShowCourseCommand : Command, IExecutable
    {
        [Inject]
        private readonly IDatabase _repository;

        public ShowCourseCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        //Shows info for given course IF data consists 2 or 3 elements 
        // 2 elements -> "show" command + course's name 
        // 3 elements -> "show" command + course's name + student's username
        public override void Execute()
        {
            switch (Data.Length)
            {
                case 2:
                    {
                        var courseName = Data[1];
                        _repository.GetAllStudentsFromCourse(courseName);
                        break;
                    }
                case 3:
                    {
                        var courseName = Data[1];
                        var studentUsername = Data[2];
                        _repository.GetStudentScoresFromCourse(courseName, studentUsername);
                        break;
                    }
                default:
                {
                    throw new InvalidCommandException(Input);
                }
            }
        }
    }
}