using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float viewRange;
    [Range(0, 360)]
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewDelay = 0.1f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private Transform bodyViewTransform;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private List<Collider> visibleTargets;
    private float currentTimer = 0;
    [SerializeField] private bool hasVisualTarget = false;
    private Vector3 lastKnownPosition;

    public bool HasVisualTarget { get => hasVisualTarget; }
    public Vector3 LastKnownPosition { get => lastKnownPosition; }
    public List<Collider> VisibleTargets { get => visibleTargets; }

    private void Start()
    {
        visibleTargets = new List<Collider>();
    }
    private void Update()
    {
        if (currentTimer < viewDelay) currentTimer += Time.deltaTime;
        else 
        {
            currentTimer = 0;
            hasVisualTarget = FindVisibleTargets();
            if (!hasVisualTarget) visibleTargets.Clear();
        }
    }

    public bool FindVisibleTargets()
    {
        bool tempbool = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(bodyViewTransform.position, viewRange, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Vector3 targetPoint = targetsInViewRadius[i].bounds.center;
            float distToTarget = Vector3.Distance(bodyViewTransform.position, targetPoint);
            Vector3 dirToTarget = (targetPoint - bodyViewTransform.position).normalized;
            if (Vector3.Angle(bodyViewTransform.forward, dirToTarget) < viewAngle / 2 || distToTarget <= minDistance)
                {
                if (!Physics.Raycast(bodyViewTransform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    if (!visibleTargets.Contains(targetsInViewRadius[i]))
                    {
                        visibleTargets.Add(targetsInViewRadius[i]);
                    }
                    tempbool = true;
                    lastKnownPosition = visibleTargets[0].transform.position;
                }
            }
        }
        return tempbool;
    }
    public Collider GetTarget() 
    {
        return visibleTargets[0];
    }
}
