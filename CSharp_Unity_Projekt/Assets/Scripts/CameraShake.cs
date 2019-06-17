using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private GameObject camera;
    private float amount;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void ShakeCamera(float duration, float amount)
    {
        this.amount = amount;
        InvokeRepeating("shake", 0, 0.02f);
        Invoke("stop", duration);
    }

    private void shake()
    {
        camera.transform.localPosition = new Vector3(Random.Range(0 - amount, amount), Random.Range(0 - amount, amount));
    }

    private void stop()
    {
        CancelInvoke("shake");
        camera.transform.localPosition = Vector3.zero;
    }
}
