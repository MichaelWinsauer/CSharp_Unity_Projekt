using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxForeGround : MonoBehaviour
{
    [SerializeField]
    private float amount;

    private GameObject cam;
    private float start;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        start = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (cam.transform.position.x * -amount);

        transform.position = new Vector3(start + distance, transform.position.y, transform.position.z);
    }
}
