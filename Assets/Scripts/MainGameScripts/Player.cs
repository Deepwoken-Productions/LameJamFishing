using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform armRotPivot;
    [SerializeField] private float z = -3;

    private bool facingRight = true;
    private Camera camera;
    private GrapplingHook grapplingHook;


    private void Awake()
    {
        camera = Camera.main;
        grapplingHook = FindObjectOfType<GrapplingHook>();
    }

    private void Update()
    {
        if (grapplingHook.state != GrappleStates.Idle)
            return;

        Vector3 pt = Input.mousePosition;
        pt.z = -z;
        pt = camera.ScreenToWorldPoint(pt);
        pt.z = z;

        Vector2 dir = (pt - transform.position).normalized;
        armRotPivot.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        

        if (dir.x >= 0 && !facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = true;
        }
        else if (dir.x < 0 && facingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            facingRight = false;
        }
    }
}
