using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Cell
    {
        private string Expression { get; set; }
        private double Value { get; set; }

        public Cell()
        {
            Expression = null;
            Value =0;
        }

        public void SetValue(double newValue)
        {
            Value = newValue;
        }

        public double getValue()
        {
            return this.Value;
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
