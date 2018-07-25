using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    class Transformator
    {
        public Transformator()
        {

        }
        public string CoordinatesToCellExpression(Cell[,] cells, string coordinates)
        {
            char letter = coordinates[0];
            char number = coordinates[1];
            int row = letter - 'A'+1;
            int column = number - '0';

            return cells[row, column].getValue().ToString();
        }
        public void TransformNumbersOnlyExpressions(Cell[,] cells, int maxSize, ReversePolishNotation reversePolishNotation)
        {
            Cell cell = new Cell();
            for (int i = 1; i < maxSize; i++)
            {
                for (int j = 1; j < maxSize; j++)
                {
                    cell = cells[i, j];
                    if (cell.GetExpression() == null)
                    {
                        cell.SetValue(0);
                    }
                    else if (cell.GetExpression()[0] == '-')
                    {
                        cell.SetValue(Convert.ToDouble(cell.GetExpression()));
                    }
                    else if (Regex.IsMatch(cell.GetExpression(), @"^[^A-Z]*$"))
                    {
                        cell.SetValue(reversePolishNotation.Calculate(cell.GetExpression()));
                    }

                }
            }
        }
        public double CalculateExpressionWithCoordinates(string expression, Cell[,] cells, ReversePolishNotation reversePolishNotation)
        {
            string transformatedExpression = "";
            string[] parts = reversePolishNotation.DivideExpressionOnParts(expression);
            foreach (string part in parts)
            {
                if (Regex.IsMatch(part, @"^[^A-Z]*$"))
                {
                    transformatedExpression += part;
                }
                else
                {
                    transformatedExpression += CoordinatesToCellExpression(cells, part);
                }
            }

            return reversePolishNotation.Calculate((transformatedExpression));
        }
        public void TransformCellsWithCoordinates(Cell[,] cells, int maxSize, ReversePolishNotation reversePolishNotation)
        {
            for (int i = 1; i < maxSize; i++)
            {
                for (int j = 1; j < maxSize; j++)
                {
                    if (cells[i, j].getValue() == 0 && cells[i,j].GetExpression()!=null)
                    {
                        string expression = cells[i, j].GetExpression();
                        cells[i,j].SetValue(CalculateExpressionWithCoordinates(expression,cells, reversePolishNotation));
                    }
                }
            }
        }

        public void TransformExpressionsToValues(Cell[,] cells, int maxSize, ReversePolishNotation reversePolishNotation)
        {
            TransformNumbersOnlyExpressions(cells, maxSize, reversePolishNotation);
            TransformCellsWithCoordinates(cells, maxSize, reversePolishNotation);
        }
    }
}
