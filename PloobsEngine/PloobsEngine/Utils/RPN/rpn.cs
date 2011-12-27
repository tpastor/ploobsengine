using System;
using System.Collections.Generic;

namespace PloobsEngine.Utils
{

public class Rpn  {

    public Rpn()
    {
        _infixTransformer = new InfixTransformer();
    }
   private InfixTransformer _infixTransformer;
   private char [] sp = new char [] {' ','\t'};

    /// <summary>
    /// Evaluate Infix expression
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
   public bool EvaluateInfix(String s, out float result)
   {
       string transformedString = _infixTransformer.Transform(s);
       return EvaluateRPN(transformedString, out result);
   }
    

  /// <summary>
  /// Evaluate RPN expression
  /// </summary>
  /// <param name="s"></param>
  /// <param name="result"></param>
  /// <returns></returns>
  public bool EvaluateRPN(String s, out float result)  
  {
      result = -1;
      if (s == null)
      {          
          return false;
      }

      Stack<string> tks = new Stack<string>(s.Split(sp,StringSplitOptions.RemoveEmptyEntries));
      if (tks.Count==0)          
      {
          return false;
      }

        float r = evalrpn(tks);
        if (tks.Count != 0) return false;
        result =  r;
        return true;
      
    } 

  private float evalrpn(Stack<string> tks)  {
    string tk = tks.Pop();
    float x,y;
    if (!float.TryParse(tk, out x))  {
      y = evalrpn(tks);  x = evalrpn(tks);
      if      (tk=="+")  x += y;
      else if (tk=="-")  x -= y;
      else if (tk=="*")  x *= y;
      else if (tk == "/") x /= y;
      else
      {
          tks.Push("ERRO"); /// will cause error upside
          return -1;          
      }
    }
    return x;
  }
}

}