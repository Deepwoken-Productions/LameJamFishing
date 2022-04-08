using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
public class Fish : MonoBehaviour
{
    [SerializeField] private int ptsValue;
    [SerializeField] private float jumpAngleMin;
    [SerializeField] private float jumpAngleMax;
    [SerializeField] private float verticalForce;
    [SerializeField] private LayerMask catchLayer;
    
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        //Launch the fish
        Vector2 direction = Vector2.up * verticalForce;
        float rot = Random.Range(jumpAngleMin, jumpAngleMax) * Mathf.Deg2Rad;
        direction.x = Mathf.Cos(rot) - Mathf.Sin(rot);
        direction.y = Mathf.Sin(rot) + Mathf.Cos(rot);
        _rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }
    
    //Begin grapple
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Fish has it something that should catch it.
        if (other.gameObject.layer == catchLayer)
        {
            WorldController.playerPoints += ptsValue; 
            Destroy(gameObject);
        }
        //Fish should despawn
        else
        {
            Destroy(gameObject);
        }
    }
}
