using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public Transform rotationCenter;

    public float angularSpeed;
    public float rotationRadius;
    public bool rotateClockwise;

    private float posX;
    private float posY;
    private float angle = 0;

    private void Update()
    {
        

        if (!rotateClockwise)
        {
            posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
            posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

            transform.position = new Vector2(posX, posY);
            angle = angle + Time.deltaTime * angularSpeed;
        }
        else if(rotateClockwise)
        {
            posX = rotationCenter.position.x + Mathf.Sin(angle) * rotationRadius;
            posY = rotationCenter.position.y + Mathf.Cos(angle) * rotationRadius;

            transform.position = new Vector2(posX, posY);
            angle = angle + Time.deltaTime * angularSpeed;
        }
        

        if (angle >= 360)
        {
            angle = 0;
        }
    }
}
