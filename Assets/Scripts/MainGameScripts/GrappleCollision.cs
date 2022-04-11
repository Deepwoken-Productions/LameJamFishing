using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCollision : MonoBehaviour
{
    private BoxCollider2D col
    {
        get
        {
            return GetComponent<BoxCollider2D>();
        }
    }

    public GrapplingHook grapple { get; private set; }
    public Fish fish { get; private set; }


    public void Initialize(GrapplingHook grapple)
    {
        gameObject.layer = LayerMask.NameToLayer("Catch Layer");
        this.grapple = grapple;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Don't need a layer cast, can only collider with fish.
        if(fish == null)
        {
            grapple.OnHooked();
            fish = collision.GetComponent<Fish>();
            fish.OnHooked(this);
            
        }

    }
}
