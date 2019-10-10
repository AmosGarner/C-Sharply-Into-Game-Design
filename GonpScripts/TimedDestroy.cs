using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float lifeTime;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time - startTime > lifeTime){
            Destroy(gameObject);
        }
    }
}
