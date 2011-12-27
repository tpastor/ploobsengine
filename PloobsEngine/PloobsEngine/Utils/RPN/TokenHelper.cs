using System;
using System.Collections.Generic;

namespace PloobsEngine.Utils
{
    public class TokenHelper
    {
        private const string _operators = "+-*/%=!^";
        private const string _leftAssociativeOperators = "*/%+-";
        private const string _rightAssociativeOperators = "!=^";

        private static readonly Dictionary<string, int> _operatorsToPrecedenceMapping =
            new Dictionary<string, int>
                {
                    {"=", 1},
                    {"+-", 2},
                    {"*/%", 3},
                    {"!", 4},
                    {"^", 5}
    };


        public bool IsOperator(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            return _operators.Contains(token);
        }

        public int GetPrecedenceFor(string token)
        {
            foreach (KeyValuePair<string, int> precedenceMapping in _operatorsToPrecedenceMapping)
            {
                if (precedenceMapping.Key.Contains(token))
                {
                    return precedenceMapping.Value;
                }
            }
            throw new InvalidOperationException(string.Format("{0} is not a valid operator!", token));
        }

        public bool IsNumber(string token)
        {
            try
            {
                int result = int.Parse(token);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool IsFunction(string token)
        {
            char possibleFunction;
            try
            {
                possibleFunction = char.Parse(token);
            }
            catch (Exception e)
            {
                return false;
            }
            return (possibleFunction >= 'A' && possibleFunction <= 'Z');
        }

        public bool IsLeftAssociative(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length != 1)
            {
                throw new ArgumentException("Incorrect token! Correct should be one character only", token);
            }
            return _leftAssociativeOperators.Contains(token);
        }

        public bool IsRightAssociative(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || token.Length != 1)
            {
                throw new ArgumentException("Incorrect token! Correct should be one character only", token);
            }
            return _rightAssociativeOperators.Contains(token);
        }
    }
}