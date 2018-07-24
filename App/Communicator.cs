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
            Console.WriteLine();
            PrintSpreadsheetExpressions(cells, maxSize);
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadKey();
        }

    }
}
