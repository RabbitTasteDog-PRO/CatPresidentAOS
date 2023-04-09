using UnityEngine;
using System;
using System.Collections;

public class MyObject
{
    public int Test;
    public string Name { get; private set; }

    public MyObject()
    {
    }

    public MyObject(int i, string s)
    {
        Test = i;
        Name = s;
    }
}