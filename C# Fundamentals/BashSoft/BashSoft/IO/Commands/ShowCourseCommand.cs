using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("show")]
    public class ShowCourseCommand : IExecutable
    {
        private readonly IDatabase _repository;

        public ShowCourseCommand(IDatabase repository)
        {
            _repository = repository;
        }

        //Shows info for given course IF data consists 2 or 3 elements 
        // 2 elements -> "show" command + course's name 
        // 3 elements -> "show" command + course's name + student's username
        public void Execute(params string[] args)
        {
            var input = args[0];
            var data = input.Split();
            switch (data.Length)
            {
                case 2:
                    {
                        var courseName = data[1];
                        _repository.GetAllStudentsFromCourse(courseName);
                        break;
                    }
                case 3:
                    {
                        var courseName = data[1];
                        var studentUsername = data[2];
                        _repository.GetStudentScoresFromCourse(courseName, studentUsername);
                        break;
                    }
                default:
                {
                    throw new InvalidCommandException(input);
                }
            }
        }
    }
}