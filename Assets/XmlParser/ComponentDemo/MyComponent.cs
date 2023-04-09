using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    public int JustSomething;
    public string MyName;

    void Start()
    {
        GameObject.Find("GUI Text").GetComponent<GUIText>().text += "\nComponent started! " + MyName + " [" + JustSomething + "]";
    }
}