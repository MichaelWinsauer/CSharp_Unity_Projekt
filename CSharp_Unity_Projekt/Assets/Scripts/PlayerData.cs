using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    private float lastPosX;
    private float lastPosY;
    private bool canDash;
    private int health;

    public PlayerData()
    {

    }

    public PlayerData(float lastPosX, float lastPosY, bool canDash, int health)
    {
        this.lastPosX = lastPosX;
        this.lastPosY = lastPosY;
        this.canDash = canDash;
        this.health = health;
    }

    public float LastPosX { get => lastPosX; set => lastPosX = value; }
    public float LastPosY { get => lastPosY; set => lastPosY = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public int Health { get => health; set => health = value; }
}
