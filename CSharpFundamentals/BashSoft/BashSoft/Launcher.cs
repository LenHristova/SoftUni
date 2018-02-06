using BashSoft.SimpleJudge;

namespace BashSoft
{
    using System;

    class Launcher
    {
        static void Main(string[] args)
        {
            //StudentsRepository.InitializeData();
            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCourse("Unity");
            //StudentsRepository.GetStudentScoresFromCourse("unity", "Lila");
            //StudentsRepository.GetStudentScoresFromCourse("Unity", "Lil");
            //StudentsRepository.GetStudentScoresFromCourse("unity", "Lil");           
            //Console.WriteLine("............................................................");

            //IOManager.CreateDirectoryInCurrentFolder("len");
            //IOManager.TraverseDirectory(0);
            //Console.WriteLine("............................................................");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.CreateDirectoryInCurrentFolder("l");
            //IOManager.TraverseDirectory(0);
            //IOManager.ChangeCurrentDirectoryRelative("Len");
            //IOManager.ChangeCurrentDirectoryRelative("BashSoft");
            //IOManager.TraverseDirectory(1);
            //Console.WriteLine("............................................................");

            // IOManager.TraverseDirectory(3);
            //Console.WriteLine("............................................................");


            // Test: Compare tests with SimpleJudge
            //var outputPath = "L:\\SoftUni\\CSharpFundamentals\\BashSoft\\BashSoft\\Tests";

            //Tester.CompareContent(outputPath + "\\test1.txt", outputPath+ "\\test2.txt");

            //Tester.CompareContent(outputPath + "\\test2.txt", outputPath + "\\test3.txt");

            //Tester.CompareContent(outputPath + "\\test3.txt", outputPath + "\\test4.txt");

            //Test: Making a Directory with Illegal Symbols
            //IOManager.CreateDirectoryInCurrentFolder("*2");
            //Console.WriteLine("............................................................");


            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            IOManager.ChangeCurrentDirectoryRelative("..");
            Console.WriteLine();
        }
    }
}
