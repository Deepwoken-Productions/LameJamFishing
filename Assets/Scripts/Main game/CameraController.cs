using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    private Transform target = null;

    private void Update()
    {
        if (target == null)
            return;

        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smoothing);
    }

    public void SetTarget(Transform target) => this.target = target;
}
