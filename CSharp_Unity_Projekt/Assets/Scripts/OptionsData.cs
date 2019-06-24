using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsData
{
    private bool useController;
    private float volume;

    public OptionsData(bool useController, float volume)
    {
        this.useController = useController;
        this.volume = volume;
    }

    public bool UseController { get => useController; set => useController = value; }
    public float Volume { get => volume; set => volume = value; }
}
