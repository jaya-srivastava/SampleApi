using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sample.Models
{
    public enum Operation
    {
         None,

        [Description("+")]
        Add,

        [Description("-")]
        Subtract,

        [Description("X")]
        Multiply,

        [Description("/")]
        Divide,

        Compare,

        [Description("Even-Odd")]
        EvenOdd,

        [Description("Absolute Number")]
        AbsValue,

        [Description("Convert Word to Number")]
        WordToNum,

        [Description("Convert Number to Word")]
        NumToWord,

        [Description("Square")]
        Square,

        [Description("Cube")]
        Cube,

        [Description("SquareRoot")]
        SquareRoot,

        [Description("CubeRoot")]
        CubeRoot,


        [Description("Addition and Subtraction of Integers")]
        IntegerRules,

        [Description("Addition of Doubles")]
        AddDoubles,

        [Description("Rounding to 10")]
        RoundTo10,

        [Description("Rounding to 100")]
        RoundTo100,

        [Description("Rounding to 1000")]
        RoundTo1000,


    }

    public enum MathGenre
    {
        None,
        BasicOps,  //integer Arithmatic
		NumberOps,
        Decimal,
        Comparison,
        Fraction,
        Algebra, 
        Geometry,
        Trignometry,
        WordProblem
    }

    public enum AlgSpecific
    {
       None,        
       AlgAdd,       
       AlgSub,
       AlgMul,
       AlgDiv,
       OneVariable,
       TwoVariable 
    }

    // this denotes what part of an epxression will be changed to
    //create an Algebric expression or missing operand
    public enum ReplaceExpressionType
    {
        None,          //i.e no replacement required it is normal airthmetic question of Add,Sub,Mul,Div and Compare
        OneOperator,   // replace Operator +, -, * or / with ?
        OneOperand,       //replace One Operand with ? mark 
        OneOperand_X,    //replace One Operand with x mark 
        OneOperand_nX,    //replace One Operand with n times of x 
    }

    public enum OperationGeo
    {
        Perimeter,

        Area,

        Volume,

    }
}
