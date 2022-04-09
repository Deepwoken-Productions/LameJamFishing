using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class FishDespawnSection : MonoBehaviour
{
    [SerializeField] private GameObject splashEffect;
    [SerializeField] private LayerMask fishLayer;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if colliding with the fish layer,
        if (1 << other.gameObject.layer == fishLayer)
        {
            Fish fish = other.GetComponent<Fish>();
            GameObject go = Instantiate(splashEffect, other.transform.position, Quaternion.identity);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();

            ps.emission.SetBurst(0, new ParticleSystem.Burst(0, fish.splashMass * 3));
            ParticleSystem.MainModule mainModule = ps.main;
            mainModule.startSpeedMultiplier += fish.splashMass;
            ps.Play();
            
            Destroy(go, 10);
        }
        Destroy(other.gameObject);
    }
}
