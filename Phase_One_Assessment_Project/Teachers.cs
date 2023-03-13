using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase_One_Assessment_Project
{
    class Teachers
    {
        static void Main(string[] args)
        {
            string teachersFile = "teachersdata.txt";
            string[] teachersData = LoadTeachersData(teachersFile);

            while (true)
            {
                Console.WriteLine("1. Add Teacher");
                Console.WriteLine("2. Show Teachers List");
                Console.WriteLine("3. Update Teacher Data");
                Console.WriteLine("4. Exit");

                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddTeacher(teachersData, teachersFile);
                        break;
                    case "2":
                        TeachersList(teachersData);
                        break;
                    case "3":
                        UpdateTeacher(teachersData, teachersFile);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

                Console.WriteLine();
            }
        }

        static string[] LoadTeachersData(string filename)
        {
            if (File.Exists(filename))
            {
                return File.ReadAllLines(filename);
            }
            else
            {
                return new string[0];
            }
        }

        static void TeachersList(string[] teachersData)
        {
            if (teachersData.Length == 0)
            {
                Console.WriteLine("No teachers found");
            }
            else
            {
                Console.WriteLine("ID\tName\tClass and Section");
                foreach (string teacher in teachersData)
                {
                    string[] teacherFields = teacher.Split(',');
                    Console.WriteLine("{0}\t{1}\t{2}", teacherFields[0], teacherFields[1], teacherFields[2]);
                }
            }
        }

        static void AddTeacher(string[] teachersData, string filename)
        {
            Console.Write("Enter teacher ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter teacher name: ");
            string name = Console.ReadLine();

            Console.Write("Enter class and section: ");
            string classAndSection = Console.ReadLine();

            string newTeacher = string.Format("{0},{1},{2}", id, name, classAndSection);

            File.AppendAllText(filename, newTeacher + Environment.NewLine);

            Console.WriteLine("Teacher added successfully");
        }

        static void UpdateTeacher(string[] teachersData, string filename)
        {
            Console.Write("Enter teacher ID to update: ");
            string id = Console.ReadLine();

            bool teacherFound = false;
            for (int i = 0; i < teachersData.Length; i++)
            {
                string teacher = teachersData[i];
                string[] teacherFields = teacher.Split(',');
                if (teacherFields[0] == id)
                {
                    Console.Write("Enter new teacher name (leave blank to keep the same): ");
                    string name = Console.ReadLine();

                    Console.Write("Enter new class and section (leave blank to keep the same): ");
                    string classAndSection = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name))
                    {
                        teacherFields[1] = name;
                    }
                    if (!string.IsNullOrEmpty(classAndSection))
                    {
                        teacherFields[2] = classAndSection;
                    }

                    string updatedTeacher = string.Join(",", teacherFields);
                    teachersData[i] = updatedTeacher;

                    File.WriteAllLines(filename, teachersData);

                    Console.WriteLine("Teacher updated successfully");

                    teacherFound = true;
                    break;
                }
            }

            if (!teacherFound)
            {
                Console.WriteLine("Teacher not found");
            }
        }
    }
}
