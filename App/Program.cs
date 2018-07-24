#define SIZE

using System;


namespace App
{

    class Program
    {
        static void Main(string[] args)
        {
            Spreadsheet spreadsheet = new Spreadsheet(Constants.MaxSize);
            spreadsheet.Start();

            Console.ReadKey();
        }
    }
}
