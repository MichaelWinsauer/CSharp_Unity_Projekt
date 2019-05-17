using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence
{
    private string name;
    private KeyCode[] keys;
    private int index;
    private static List<Sequence> sequences = new List<Sequence>();

    public Sequence(string name, KeyCode[] keys)
    {
        this.name = name;
        this.keys = keys;
        index = 0;

        sequences.Add(this);
    }


    public KeyCode[] Keys { get => keys; set => keys = value; }
    public int Index { get => index; set => index = value; }
    public static List<Sequence> Sequences { get => sequences; set => sequences = value; }
    public string Name { get => name; set => name = value; }
}
