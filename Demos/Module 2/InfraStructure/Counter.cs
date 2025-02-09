﻿using Microsoft.Extensions.Logging;

namespace InfraStructure;

public class Counter : ICounter
{
    private int _counter = 0;

    public Counter()
    {
    }

    public void Increment()
    {
        _counter++;
    }

    public void Show()
    {
        Console.WriteLine($"Counter value is {_counter}");
    }
}
