using System.Collections.Generic;
using System.Linq;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Transform Infix to RPN
    /// </summary>
    public class InfixTransformer
    {
        private readonly TokenHelper _tokenHelper = new TokenHelper();
        private readonly Queue<string> _output = new Queue<string>();
        private readonly Stack<string> _operators = new Stack<string>();

        /// <summary>
        /// Transforms the specified current expression.
        /// </summary>
        /// <param name="currentExpression">The current expression.</param>
        /// <returns></returns>
        public string Transform(string currentExpression)
        {
            List<string> tokens = currentExpression.Split(new char[] { ' ' }).ToList();
            foreach (string token in tokens)
            {
                if (_tokenHelper.IsNumber(token))
                {
                    _output.Enqueue(token);
                }
                else if (_tokenHelper.IsFunction(token))
                {
                    _operators.Push(token);
                }
                else if (token.Equals(","))
                {
                    while (_operators.Count > 0 && _operators.Peek() != "(")
                    {
                        string topOperator = _operators.Pop();
                        _output.Enqueue(topOperator);
                    }
                }
                else if (_tokenHelper.IsOperator(token))
                {
                    while (_operators.Count > 0 && _tokenHelper.IsOperator(_operators.Peek()))
                    {
                        if ((_tokenHelper.IsLeftAssociative(token) && _operators.Count > 0 && (_tokenHelper.GetPrecedenceFor(token) <= _tokenHelper.GetPrecedenceFor(_operators.Peek()))) || (_tokenHelper.IsRightAssociative(token) && (_tokenHelper.GetPrecedenceFor(token) < _tokenHelper.GetPrecedenceFor(_operators.Peek()))))
                        {
                            string operatorToReturn = _operators.Pop();
                            _output.Enqueue(operatorToReturn);
                        }
                        else break;
                    }
                    _operators.Push(token);
                }
                if (token.Equals("("))
                {
                    _operators.Push(token);
                }
                if (token.Equals(")"))
                {
                    while (_operators.Count > 0 && _operators.Peek() != "(")
                    {
                        _output.Enqueue(_operators.Pop());
                    }
                    _operators.Pop();
                }
                if (_operators.Count > 0 && _operators.Count > 0 && _tokenHelper.IsFunction(_operators.Peek()))
                {
                    _output.Enqueue(_operators.Pop());
                }
            }
            while (_operators.Count > 0 && _tokenHelper.IsOperator(_operators.Peek()))
            {
                _output.Enqueue(_operators.Pop());
            }
            string transformedString = string.Empty;
            while (_output.Count > 0)
            {
                transformedString += _output.Dequeue() + " ";
            }
            return transformedString.TrimEnd();
        }
    }
}