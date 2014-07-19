using System;
using System.IO;

namespace FeatureExtractor
{
  internal class Program
  {
    private static Points points;
    private static Points pointsPrev;

    private static void Main(string[] args)
    {
      StreamReader streamReader = new StreamReader((Stream) File.OpenRead("C:\\Users\\Timjay Pc\\Documents\\FourthYearDLSU\\THESIS\\6-26-2014\\Charlene Retanan.csv"));
      int num = 0;
      while (!streamReader.EndOfStream)
      {
        string line = streamReader.ReadLine();
        if (Program.points != null)
          Program.pointsPrev = Program.points;
        Program.points = new Points(line);
        if (Program.pointsPrev != null)
        {
          Console.WriteLine(Program.points.bodyMovement(Program.points, Program.pointsPrev));
          Console.WriteLine(Program.points.leaning(Program.points, Program.pointsPrev));
        }
        if ((double) num != (double) Program.points.arr[Points.TIMESTAMP])
        {
          num = (int) Program.points.arr[Points.TIMESTAMP];
          Console.Read();
        }
      }
      Console.Read();
    }
  }
}
