
using System;
using System.Collections.Generic;
using System.Linq;

static class CollectionExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        Random rnd = new Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static Stack<T> Shuffle<T>(this Stack<T> stack)
    {
        Random rnd = new Random();
        return new Stack<T>(stack.OrderBy(x => rnd.Next()));
    }
}