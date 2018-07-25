using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Communicator
    {
        public Communicator()
        {
            Console.WriteLine("Welcome in this fantastic Spreadsheet!");
        }

        public void PrintCellExpression(Cell cell)
        {
            int numberOfNeededSpaces;
            if (cell.GetExpression() != null)
            {
                numberOfNeededSpaces = Constants.Width - cell.GetExpression().Length;
                Console.Write(cell.GetExpression());
            }
            else
            {
                numberOfNeededSpaces = Constants.Width;
            }
            for (int i = 0; i < numberOfNeededSpaces; i++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
        }
        public void PrintCellValue(Cell cell)
        {
            int numberOfNeededSpaces = Constants.Width - cell.getValue().ToString().Length;
            Console.Write(cell.getValue().ToString());

            for (int i = 0; i < numberOfNeededSpaces; i++)
            {
                Console.Write(" ");
            }
            Console.Write("|");
        }
        public void PrintSpreadsheetExpressions(Cell[,] cells, int size)
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = 1; j < size; j++)
                {
                    PrintCellExpression(cells[i,j]);
                }
                Console.WriteLine();
            }
        }
        public void PrintSpreadsheetValues(Cell[,] cells, int size)
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = 1; j < size; j++)
                {
                    PrintCellValue(cells[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void ReadInitialStateFromUser(Cell[,] cells)
        {
            string line = null;
            int column = 1;
            int row = 1;
            while (true)
            {
                line = Console.ReadLine();
                string[] expressions = line.Split("|");
                column = 1;
                foreach (string expression in expressions)
                {
                    if (expression.EndsWith(";"))
                    {                       
                        cells[row, column].SetExpression(expression.Replace(";", ""));
                        return;
                    }
                    cells[row, column].SetExpression(expression);
                    column++;
                }
                row++;
            }
        }
        public void LoadInitialState(Cell[,] cells, int maxSize)
        {
            ReadInitialStateFromUser(cells);
            Console.WriteLine();
            Console.WriteLine("It looks like:");
            PrintSpreadsheetExpressions(cells, maxSize);
            Console.WriteLine();
        }
        public void ReadAndCalculateExpression(Cell[,] cells, ReversePolishNotation reversePolishNotation, Transformator transformator)
        {
            string userExpression = Console.ReadLine();
            double result= transformator.CalculateExpressionWithCoordinates(userExpression, cells, reversePolishNotation);
            Console.WriteLine(result.ToString());
        }
        public void PrintPossibleOptions()
        {
            Console.WriteLine("If you want to leave program press 'q'");
            Console.WriteLine("If you want to calculate something 'c'");
            Console.WriteLine("If you want to print values press 'v'");
            Console.WriteLine("If you want to print expressions press 'e'");
            Console.WriteLine("If you want to update some value press 'u' (Not supported yet)");
        }
        public void StartDialogWithUser(Cell[,] cells, ReversePolishNotation reversePolishNotation, Transformator transformator, int size)
        {
            Console.WriteLine();
            PrintPossibleOptions();
            while (true)
            {
                char command = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (command)
                {
                    case 'q':
                        Console.WriteLine("Ty for using our Spreadsheet. See you later");
                        return;
                    case 'c':
                        Console.WriteLine("Type your expression");
                        ReadAndCalculateExpression(cells, reversePolishNotation, transformator);
                        break;
                    case 'u':
                        Console.WriteLine("Updating values is not suported yet");
                        break;
                    case 'v':
                        Console.WriteLine("Here you go");
                        PrintSpreadsheetValues(cells, size);
                        break;
                    case 'e':
                        Console.WriteLine("Here you go");
                        PrintSpreadsheetExpressions(cells, size);
                        break;
                    default:
                        Console.WriteLine("Something goes wrong. try again");
                        PrintPossibleOptions();
                        break;
                }
            }
        }
    }
}
