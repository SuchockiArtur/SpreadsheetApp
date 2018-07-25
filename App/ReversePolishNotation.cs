using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace App
{
    public class ReversePolishNotation
    {
        private string[] _expressionInInfixationNotation;
        private string[] _expressionInOnpNotation;
        private readonly Dictionary<string, int> _priority = new Dictionary<string, int>();

        public ReversePolishNotation()
        {
            _priority.Add("+", 1);
            _priority.Add("-", 1);
            _priority.Add("*", 2);
            _priority.Add("/", 2);
            _priority.Add("^", 3);
            _priority.Add("(", 0);
            _priority.Add(")", 0);
        }

        public string[] DivideExpressionOnParts(string expression)
        {
            expression = expression.Replace(" ", "");
            string[] parts = Regex.Split(expression, @"([-+/*^()])");
            parts = parts.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return parts;
        }

        public int GetPriorityOfLastSign(Stack stack)
        {
            return _priority[stack.Peek().ToString()];
        }

        public string[] InfixationNotationToOnp(string[] expressionInfixationArray)
        {
            Stack stack = new Stack();
            string[] expressionInOnpNotation = new string[expressionInfixationArray.Length];
            int expressionInOnpNotationSize = 0;
            for (int i = 0; i < expressionInfixationArray.Length; i++)
            {
                string part = expressionInfixationArray[i];
                if (Regex.IsMatch(part, @"^\d+$"))
                {
                    expressionInOnpNotation[expressionInOnpNotationSize] = part;
                    expressionInOnpNotationSize++;
                }
                else if (part == "(")
                {
                    stack.Push(part);
                }
                else if (part == ")")
                {
                    while (stack.Peek().ToString() != "(")
                    {
                        expressionInOnpNotation[expressionInOnpNotationSize] = stack.Pop().ToString();
                        expressionInOnpNotationSize++;
                    }
                    stack.Pop(); // pobierz nawias otwierajacy ze stosu - trzeba się go pozbyć
                }
                else
                {
                    if ((stack.Count == 0) || (_priority[part] > GetPriorityOfLastSign(stack)))
                    {
                        stack.Push(part);
                    }
                    else
                    {
                        while ((stack.Count != 0) && (_priority[part] <= GetPriorityOfLastSign(stack)))
                        {
                            expressionInOnpNotation[expressionInOnpNotationSize] = stack.Pop().ToString();
                            expressionInOnpNotationSize++;
                        }
                        stack.Push(part);
                    }
                }
            }
            while (stack.Count != 0)
            {
                expressionInOnpNotation[expressionInOnpNotationSize] = stack.Pop().ToString();
                expressionInOnpNotationSize++;
            }
            return expressionInOnpNotation;
        }

        public double CalculateValueFromOnp(string[] parts)
        {
            Stack stack = new Stack();
            double result = 0;
            for (int i = 0; i < parts.Length; i++){
                string part = parts[i];
                if (Regex.IsMatch(part, @"^\d+$")){
                    stack.Push(part);
                }
                else{
                    switch (part){
                        case "+":
                            result = Convert.ToDouble(stack.Pop()) + Convert.ToDouble(stack.Pop());
                            stack.Push(result);
                            break;
                        case "-":
                            result = Convert.ToDouble(stack.Pop()) - Convert.ToDouble(stack.Pop());
                            stack.Push(result);
                            break;
                        case "*":
                            result = Convert.ToDouble(stack.Pop()) * Convert.ToDouble(stack.Pop());
                            stack.Push(result);
                            break;
                        case "/":
                            double temp = Convert.ToDouble(stack.Pop()); //Have to divide the second number by the first one
                            result = Convert.ToDouble(stack.Pop()) / temp;
                            stack.Push(result);
                            break;
                        case "^":
                            Console.WriteLine("^ not implemented yet");
                            break;
                        default:
                            Console.WriteLine("Something goes wrong");
                            break;
                    }
                }

            }
            return Convert.ToDouble(stack.Pop());
        }
       
        public double Calculate(string expression)
        {
            _expressionInInfixationNotation = DivideExpressionOnParts(expression);
            _expressionInOnpNotation = InfixationNotationToOnp(_expressionInInfixationNotation);
            _expressionInOnpNotation = _expressionInOnpNotation.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            double result = CalculateValueFromOnp(_expressionInOnpNotation);

            return result;
        }
    }

}
