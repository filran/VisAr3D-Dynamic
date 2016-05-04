using UnityEngine;
using System.Collections;

public class Message {

    public string Name { get; private set; }
    public float StartX { get; private set; }
    public float StartY { get; private set; }
    public float Dist { get; private set; }

    public Message(string name, float startx, float starty, float dist)
    {
        Name = name;
        StartX = startx;
        StartY = starty;
        Dist = dist;
    }
}
