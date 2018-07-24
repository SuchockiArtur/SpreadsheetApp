using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Cell
    {
        private string Expression { get; set; }
        private int? Value;

        public Cell()
        {
            Expression = null;
            Value = null;
        }

        public void SetExpression(string newExpression)
        {
            Expression = newExpression;
        }

        public string GetExpression()
        {
            return this.Expression;
        }

    }
}
