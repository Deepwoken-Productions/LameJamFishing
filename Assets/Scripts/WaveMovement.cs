using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public Transform rotationCenter;

    public float angularSpeed;
    public float stormAngularSpeed;
    public float rotationRadius;
    public bool rotateClockwise;

    private float posX;
    private float posY;
    private float currentAngularSpeed;
    private float posZ;
    private float angle = 0;

    private void Awake()
    {
        posZ = transform.position.z;
        currentAngularSpeed = angularSpeed;
    }

    private void Update()
    {
        

        if (!rotateClockwise)
        {
            posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
            posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

            transform.position = new Vector3(posX, posY, posZ);
            angle = angle + Time.deltaTime * currentAngularSpeed;
        }
        else if(rotateClockwise)
        {
            posX = rotationCenter.position.x + Mathf.Sin(angle) * rotationRadius;
            posY = rotationCenter.position.y + Mathf.Cos(angle) * rotationRadius;

            transform.position = new Vector3(posX, posY, posZ);
            angle = angle + Time.deltaTime * currentAngularSpeed;
        }
        

        if (angle >= 360)
        {
            angle = 0;
        }
    }

    public void OnStorm(float waveSpeed)
    {
        currentAngularSpeed = waveSpeed;
    }

    public void EndStorm()
    {
        currentAngularSpeed = angularSpeed;
    }
}
