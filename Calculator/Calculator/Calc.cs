﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Calculator.Tests")]

namespace Calculator
{
    public class Calc
    {
        internal int Sum(int a, int b)
        {
            checked
            {
                return a + b;
            }
        }
    }
}
