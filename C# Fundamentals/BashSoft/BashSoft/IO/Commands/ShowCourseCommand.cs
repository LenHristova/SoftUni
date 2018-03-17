﻿using BashSoft.Exceptions;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft.IO.Commands
{
    public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
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
                        Repository.GetAllStudentsFromCourse(courseName);
                        break;
                    }

                case 3:
                    {
                        var courseName = Data[1];
                        var studentUsername = Data[2];
                        Repository.GetStudentScoresFromCourse(courseName, studentUsername);
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