using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrappleStates
{
    Shooting,
    Retracting,
    Idle
}

//gonna clean up this abomination tomorrow night lol
public class GrapplingHook : MonoBehaviour
{
    public GrappleStates state { get; private set; }

    [SerializeField] private int percision = 20;
    [SerializeField] private float waveSize = 1.4f;
    [SerializeField] private float launchSpeedMultiplier = 15f;
    [SerializeField] private float straightenLineSpeed = 10f;

    [SerializeField] private Transform shootTransform;
    [SerializeField] private Transform retractTransform;
    [SerializeField] private AnimationCurve ropeAnimationCurve;
    [SerializeField] private AnimationCurve ropeLaunchSpeedCurve;


    public float test;
    private LineRenderer lineRenderer = null;
    private Vector3 grappleTarget = Vector3.zero;
    private float moveTime;

    private float currentWaveSize = 0.0f;
    private GrappleCollision grappleCollision;
    private CameraController camController;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = percision;
        state = GrappleStates.Idle;
        currentWaveSize = waveSize;

        grappleCollision = new GameObject("GrappleCollision").AddComponent<GrappleCollision>();
        grappleCollision.Initialize(this);
        grappleCollision.gameObject.AddComponent<BoxCollider2D>();
        camController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        bool shootGrapple = Input.GetMouseButtonDown(0) && state == GrappleStates.Idle;

        if (shootGrapple)
        {
            state = GrappleStates.Shooting;
            grappleTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moveTime = 0.0f;
            lineRenderer.enabled = true;
        }

        if (state != GrappleStates.Idle)
            moveTime += Time.deltaTime;

        if (state == GrappleStates.Shooting)
        {
            if (lineRenderer.GetPosition(percision - 1).x != grappleTarget.x)
                CalculateShootPositions();
            else
            {
                if (currentWaveSize > 0)
                {
                    currentWaveSize -= Time.deltaTime * straightenLineSpeed;
                    CalculateShootPositions();
                }
                else
                {
                    currentWaveSize = waveSize;
                    grappleTarget = shootTransform.position;
                    state = GrappleStates.Retracting;
                    moveTime = 0.0f;

                    Vector3 p2 = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, p2);
                }
            }
        }
        else if (state == GrappleStates.Retracting)
        {
            CalculateRetractPositions();
        }
    }

    private void CalculateShootPositions()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(shootTransform.position - grappleTarget).normalized * ropeAnimationCurve.Evaluate(delta) * currentWaveSize;
            Vector2 targetPosition = Vector2.Lerp(shootTransform.position, grappleTarget, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(shootTransform.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(moveTime) * launchSpeedMultiplier);

            lineRenderer.SetPosition(i, currentPosition);
        }

        grappleCollision.transform.position = lineRenderer.GetPosition(percision - 1);
        camController.SetTarget(grappleCollision.transform);
    }

    private void CalculateRetractPositions()
    {
        Vector3 target = shootTransform.position;

        if((lineRenderer.GetPosition(lineRenderer.positionCount-1) - target).magnitude > 0.5f)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, Vector2.Lerp(lineRenderer.GetPosition(lineRenderer.positionCount - 1), shootTransform.position, Time.deltaTime * 5f));
        }
        else
        {
            lineRenderer.positionCount = percision;
            state = GrappleStates.Idle;
            lineRenderer.enabled = false;

            if (grappleCollision.fish != null)
                grappleCollision.fish.DestroyFish();
        }


        grappleCollision.transform.position = GetEndPosition();
        camController.SetTarget(grappleCollision.transform);
    }

    public void OnHooked()
    {
        if (state != GrappleStates.Retracting)
            state = GrappleStates.Retracting;
    }

    public Vector3 GetEndPosition() => lineRenderer.GetPosition(lineRenderer.positionCount - 1);
    public Vector3 GetOrigin() => shootTransform.position;
}
