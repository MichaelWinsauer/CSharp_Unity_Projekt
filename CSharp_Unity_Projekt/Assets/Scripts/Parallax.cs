﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float amount;

    private float length;
    private float start;
    private GameObject cam;
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        start = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - amount));
        float distance = (cam.transform.position.x * amount);

        transform.position = new Vector3(start + distance, transform.position.y, transform.position.z);

        if(temp > start + length)
        {
            start += length;
        }

        if (temp < start - length)
        {
            start -= length;
        }
    }
}
