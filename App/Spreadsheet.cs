using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Spreadsheet
    {
        private Cell[,] Cells { get; set; }
        private readonly int _maxSize;
        private readonly Communicator _communicator;
        private readonly Transformator _transformator;
        private readonly ReversePolishNotation _reversePolishNotation;

        public Spreadsheet(int maxSize)
        {
            _communicator = new Communicator();
            _transformator = new Transformator();
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
            _transformator.TransformExpressionsToValues(Cells, _maxSize, _reversePolishNotation);
            _communicator.PrintSpreadsheetValues(Cells, _maxSize);
            _communicator.StartDialogWithUser(Cells, _reversePolishNotation, _transformator, _maxSize);
        }

    }
}
