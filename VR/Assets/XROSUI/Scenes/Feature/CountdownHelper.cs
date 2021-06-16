using System;
using UnityEngine;

[Serializable]
public class CounterHelper
{
    public string name = "UnKnOwN";
    public int max = 3;
    public int current = 0;

    public bool counting = false;
    public CounterHelper(string name, int max)
    {
        this.name = name;
        this.max = max;
    }

    public bool IsReachLimit(bool resetWhenReached)
    {
        if (!counting)
            return false;
        
        if (current >= max)
        {
            if(resetWhenReached)
                Reset();

            Debug.Log($"{name}: ReachLimit");
            return true;
        }

        Debug.Log($"{name}: ReachLimit?{current}/{max}");
        return false;
    }

    public void ToMax()
    {
        current += max;
    }

    public void Increment()
    {
        if(counting)
            current++;
    }

    public void Reset()
    {
        current = 0;
        counting = false;
    }

    public void Run(bool start)
    {
        counting = start;
    }
    
    public override string ToString()
    {
        return string.Format($"CounterHelper-{name}: {current}/{max}");
    }
}