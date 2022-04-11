using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public float splashMass;
    
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private GrappleCollision _hookProperties;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        DrawDebug();
        
        _rigidBody = GetComponent<Rigidbody2D>();

        //Launch the fish
        Vector2 direction;
        float rot = Random.Range(jumpAngleMin, jumpAngleMax) *  Mathf.Deg2Rad;
        direction.x = Mathf.Cos(rot) - Mathf.Sin(rot);
        direction.y = Mathf.Sin(rot) + Mathf.Cos(rot);
        
        Debug.DrawRay(transform.position, direction * verticalForce, Color.red, 5f);
        _rigidBody.AddForce(direction * verticalForce, ForceMode2D.Impulse);
        transform.eulerAngles = new Vector3(0, 0, rot * Mathf.Rad2Deg);
    }

    void DrawDebug()
    {
        Vector2 direction;
        float jumpMin = jumpAngleMin * Mathf.Deg2Rad;
        float jumpMax = jumpAngleMax * Mathf.Deg2Rad;
        direction.x = Mathf.Cos(jumpMin) - Mathf.Sin(jumpMin);
        direction.y = Mathf.Sin(jumpMin) + Mathf.Cos(jumpMin);
        Debug.DrawRay(transform.position, direction * verticalForce, Color.green, 5f);
        direction.x = Mathf.Cos(jumpMax) - Mathf.Sin(jumpMax);
        direction.y = Mathf.Sin(jumpMax) + Mathf.Cos(jumpMax);
        Debug.DrawRay(transform.position, direction * verticalForce, Color.green, 5f);
    }

    private void Update()
    {
        if(_hookProperties != null)
        {
            transform.position = _hookProperties.grapple.GetEndPosition();
        }
    }

    //Begin grapple
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Fish has it something that should catch it.
        if (other.gameObject.layer == catchLayer)
        {
            WorldController.playerPoints += ptsValue; 
   //         Destroy(gameObject);
        }
        //Fish should despawn
        else
        {
     //       Destroy(gameObject);
        }
    }

    public void OnHooked(GrappleCollision collision)
    {
        if (collision == null || (_hookProperties = collision))
            return;

        _hookProperties = collision;
        _rigidBody.isKinematic = true;
    }

    public void DestroyFish()
    {
        WorldController.playerPoints += ptsValue;
        Destroy(gameObject);
    }
}
