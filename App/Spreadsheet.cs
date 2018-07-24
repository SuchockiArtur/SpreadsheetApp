using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Spreadsheet
    {
        private Cell[,] Cells { get; set; }
        private readonly int _maxSize;
        private Communicator _communicator;
        private ReversePolishNotation _reversePolishNotation;

        public Spreadsheet(int maxSize)
        {
            _communicator = new Communicator();
            _reversePolishNotation = new ReversePolishNotation();
            _maxSize = maxSize;
            Cells = new Cell[_maxSize,_maxSize];
            for (int i = 0; i < _maxSize; i++)  //zamienic an forEachs
            {
                for (int j = 0; j < _maxSize; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }

        public void Start()
        {
            _communicator.LoadInitialState(Cells, _maxSize);

//            string expression = "93 + 1 * (2* 3+10/5)";
//            _reversePolishNotation.Calculate(expression);
        }

    }
}
