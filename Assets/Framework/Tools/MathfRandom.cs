using System.Collections;
using System.Collections.Generic;
using System;

public class MathfRandom
{
    private static Random random = new Random();
    public static List<int> RandomNum(int wantNum, int dataCount)
    {
        HashSet<int> values = new HashSet<int>();
        List<int> list = new List<int>();
        int n;
        while (values.Count < wantNum)
        {
            n = random.Next(0, dataCount + 1);


            if (values.Add(n))
            {
                list.Add(n);
            }
        }
        return list;
    }
}
