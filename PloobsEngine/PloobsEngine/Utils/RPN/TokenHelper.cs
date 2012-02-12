using System;
using System.Collections.Generic;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Token Helper used for RPN calculator
    /// </summary>
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


        /// <summary>
        /// Determines whether the specified token is operator.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if the specified token is operator; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOperator(string token)
        {
            if (token == null || token == " "  || token == "" )
            {
                return false;
            }
            return _operators.Contains(token);
        }

        /// <summary>
        /// Gets the precedence for.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified token is number.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if the specified token is number; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNumber(string token)
        {
            try
            {
                int result = int.Parse(token);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified token is function.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if the specified token is function; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFunction(string token)
        {
            char possibleFunction;
            try
            {
                possibleFunction = Convert.ToChar(token);
            }
            catch (Exception)
            {
                return false;
            }
            return (possibleFunction >= 'A' && possibleFunction <= 'Z');
        }

        /// <summary>
        /// Determines whether [is left associative] [the specified token].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if [is left associative] [the specified token]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLeftAssociative(string token)
        {
            if (String.IsNullOrEmpty(token) || token.Length != 1)
            {
                throw new ArgumentException("Incorrect token! Correct should be one character only", token);
            }
            return _leftAssociativeOperators.Contains(token);
        }

        /// <summary>
        /// Determines whether [is right associative] [the specified token].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   <c>true</c> if [is right associative] [the specified token]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRightAssociative(string token)
        {
            if (token == null || token == " " || token == "" || token.Length != 1)            
            {
                throw new ArgumentException("Incorrect token! Correct should be one character only", token);
            }
            return _rightAssociativeOperators.Contains(token);
        }
    }
}