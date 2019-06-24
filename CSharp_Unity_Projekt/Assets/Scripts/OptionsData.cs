using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsData
{
    private bool useController;
    private float volume;
    private bool useXbox;

    public OptionsData(bool useController, float volume, bool useXbox)
    {
        this.useController = useController;
        this.volume = volume;
        this.useXbox = useXbox;
    }

    public bool UseController { get => useController; set => useController = value; }
    public float Volume { get => volume; set => volume = value; }
    public bool UseXbox { get => useXbox; set => useXbox = value; }
}
