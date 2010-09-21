﻿using System;
using System.ComponentModel;
using System.Console;
using System.Linq;
using SCG = System.Collections.Generic;
using LIST = System.Collections.Generic.List<int>;

namespace CSharpToNemerle.Test
{
  public delegate T X<T>(int a, T b) where T : class;

  public interface IVarianceTest<out T> {
    T Bar();
  }
  
  public enum A {
    A1 = 10,
    A2,
    A3
  }

  public class Foo<T> where T : new()
  {
    public Foo() : base() {  }
    ~Foo() {}

    public event EventHandler Bar;

    private EventHandler bla;
    public event EventHandler Bla {
      add { bla = (EventHandler) Delegate.Combine(bla, value); }
      remove { bla = (EventHandler) Delegate.Remove(bla, value); }
    }

    const string F = "Generic ";

    public void DoSomething(int x, global::System.String p = "class: ")
    {
      if(x > 0) {
        System.Console.WriteLine("x is greater than 0.");
        ++x;
      }
      delegate(string p) {
        Console.WriteLine("p Length is {0}", p.Length);
        WriteLine(F + p + typeof(T).FullName);
      }(p);
      const int a = 10;
      var b = x + a,
          c = x * a;
      b += 44;
      Console.WriteLine("b is {0}, c is {1}", b, c);

      var list = new SCG.List<int>(10) { a, b, c * 2 };
      foreach(var x in list)
        Console.WriteLine("collection element {0}", x);

      object obj1 = new {};
      var obj2 = new { a, B = b, C = c + 1, int.MaxValue };
      Console.WriteLine("Anonymous type test: obj1 {0}, obj2 {1}", obj1, obj2);
    }
  }

  // some magic!
  public class Magic {
    [Nemerle.Utility.Accessor(flags = WantSetter)] int a;

    [property:Description("only property")]
    [Description("only property too")]
    [method:Description("both accessors")]
    public string B {
        [Description("getter")]
        get;

        [method:Description("setter")]
        [param:Description("value parameter")]
        set;
    }

    [return:Description("shuold be on return value")]
    public override string ToString()
    {
      return string.Format(@"a is {0}, b is ""{1}""", a, B);
    }
  }

  struct __<T>
  {
      public T _;
  
      public __(T _)
      {
          this._ = _;
      }
  }

  class X { public int A { get; set; } public int B { get; set; } }
  
  static partial class Program 
  {
    static Program() { }

    void TestAlias()
    {
      Console.WriteLine("Alias LIST is {0}", typeof(LIST));
    }

    void TestArray()
    {
      var x = new byte[] { 1, 2, 3, 4 };
      Console.WriteLine("x is {0}: {1}, {2}, {3}, {4}", x, x[0], x[1], x[2], x[3]);

      var y = new long[,] { { 1, 2 }, { 3, 4 } };
      Console.WriteLine("y is {0}: {1}, {2}, {3}, {4}", y, y[0, 0], y[0, 1], y[1, 0], y[1, 1]);

      var z = new short[][] { new[]{ (short)1, 2 }, new short[]{ 3, 4 } }; // "(short)" is issue :(
      Console.WriteLine("z is {0}: {1}, {2}, {3}, {4}", z, z[0][0], z[0][1], z[1][0], z[1][1]);
    }

    void TestMagic() 
    {
      Magic m = new Magic { A = 10, B = "s" };
      Console.WriteLine("magic: {0}", m);
    }

    void TestFor(int count) 
    {
      for(int i = 0, j = 5; i < count; ++i, ++j)
        Console.WriteLine("i = {0}, j = {1}", i, j);
    }

    void TestLinq()
    {
        //magic!!
        var Scores = new SCG.List() {
            new { name = "Оля",  score = 97 },
            new { name = "Петя", score = 60 },
            new { name = "Вася", score = 92 },
            new { name = "Маша", score = 81 }
        };

        // Create the query.
        var queryHighScores =
            from rec in Scores
            where rec.score > 80
            orderby rec.score descending, rec.name
            select rec;

        // Execute the query.
        foreach (var rec in queryHighScores)
        {
          Console.WriteLine(rec);
        }
        Console.WriteLine();
    }
    
    void TestNullCheckOperator(string str)
    {
      Console.WriteLine("Null-checkin operator: {0}", str ?? "was null :)");
    }
    
    void TestCrazy()
    {
      var x = new __<X>(new X { A = 1,  B = 2 }) { _ = { A = 3, B = 4 } }._;
      Console.WriteLine("A: {0}, B: {1}", x.A, x.B);
    }

    void TestNullable()
    {
      int? nullableX = 10;
      Console.WriteLine(nullableX.HasValue ? "nullableX has value" : "no value in nullableX");
    }
  }
}