using System.Collections;
using System.Collections.Generic;

public class EnemyData
{
    private bool isRanged;
    private int health;
    private float posX;
    private float posY;

    public EnemyData(bool isRanged, int health, float posX, float posY)
    {
        this.isRanged = isRanged;
        this.health = health;
        this.posX = posX;
        this.posY = posY;
    }

    public bool IsRanged { get => isRanged; set => isRanged = value; }
    public int Health { get => health; set => health = value; }
    public float PosX { get => posX; set => posX = value; }
    public float PosY { get => posY; set => posY = value; }
}