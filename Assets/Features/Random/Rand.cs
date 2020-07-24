using System;
using System.Collections.Generic;

public class Rand
{
    public static Rand game;

    readonly Random random;

    public Rand(int seed)
    {
        random = new Random(seed);
    }

    public bool Bool(float chance) => Float() < chance;
    public int Int() => random.Next();
    public int Int(int maxValue) => random.Next(maxValue);
    public int Int(int minValue, int maxValue) => random.Next(minValue, maxValue);
    public float Float() => (float)random.NextDouble();
    public float Float(float minValue, float maxValue) => minValue + (maxValue - minValue) * Float();
    public T Element<T>(IList<T> elements) => elements[Int(0, elements.Count)];
}
