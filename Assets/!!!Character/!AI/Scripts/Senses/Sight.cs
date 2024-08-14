using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{

    [SerializeField] private Transform sightPoint;
    [SerializeField] public List<IVisible> visiblesInSight = new List<IVisible>();
    [SerializeField] private float checksPerSecond = 5;
    private double lastCheckTime = 0;

    [Header("Range Sight")]
    [SerializeField] private float range = 40f;
    [SerializeField] float horizontalAngle = 120f;
    [SerializeField] float verticalAngle = 90f;
    [SerializeField] private LayerMask occludersLayerMask = Physics.DefaultRaycastLayers;

    private void Awake()
    {
        lastCheckTime = Time.time + Random.Range(0f,1f /checksPerSecond);
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > 1f / checksPerSecond)
        {
            lastCheckTime = Time.time;
            CheckSight();
        }
    }

    private void CheckSight()
    {
        Vector3 halfExtends = CalcHalfExtents();
        Collider[] colliderInBox = 
            Physics.OverlapBox(
                sightPoint.position + sightPoint.forward * range / 2f,
                halfExtends,
                sightPoint.rotation
            );

        visiblesInSight.Clear();
        foreach (Collider collider in colliderInBox)
        {
            if(collider.TryGetComponent<IVisible>(out IVisible visible))
            {
                Vector3 direcctionToVisible = collider.transform.position - sightPoint.position;

                Vector3 direcctionOnXZPlane = Vector3.ProjectOnPlane(direcctionToVisible, Vector3.up);
                Vector3 forwardOnXZPlane = Vector3.ProjectOnPlane(sightPoint.forward, Vector3.up);

                Vector3 direcctionOnLocalYZPlane = Vector3.ProjectOnPlane(direcctionToVisible, sightPoint.right);
                Vector3 forwardOnLocalYZPlane = Vector3.ProjectOnPlane(sightPoint.forward, sightPoint.right);

                if (    (Vector3.Angle(direcctionOnXZPlane, forwardOnXZPlane) < horizontalAngle / 2f) &&
                        (Vector3.Angle(direcctionOnLocalYZPlane, forwardOnLocalYZPlane) < verticalAngle / 2f))
                {
                    bool anyPointsVisible = false;
                    int i = 0;
                    while (!anyPointsVisible && i < visible.GetCheckPoints().Length)
                    {
                        if (Physics.Linecast(sightPoint.position, visible.GetCheckPoints()[i], out RaycastHit hit, occludersLayerMask))
                        {
                            anyPointsVisible = hit.collider == collider;
                        }
                        i++;
                    }
                    if (anyPointsVisible)
                    { visiblesInSight.Add(visible); }
                }
            }
        }
    }

    private Vector3 CalcHalfExtents()
    {
        float halfSightWidth = range / Mathf.Tan((90f - (horizontalAngle / 2f)) * Mathf.Deg2Rad);
        float halfSightHeight = range / Mathf.Tan((90f - (verticalAngle / 2f)) * Mathf.Deg2Rad);
        Vector3 halfExtents = new Vector3(halfSightWidth, halfSightHeight, range / 2f);
        return halfExtents;
    }

    private void OnDrawGizmos()
    {
        Vector3 halfExtents = CalcHalfExtents();

        Vector3 origin = sightPoint.position;
        Vector3 up = sightPoint.up;
        Vector3 right = sightPoint.right;
        Vector3 forward = sightPoint.forward;
        Vector3 topLeftCorner = origin +(forward * (halfExtents.z * 2f)) + (-right * halfExtents.x) + (up * halfExtents.y);
        Vector3 topRightCorner = origin +(forward * (halfExtents.z * 2f)) + (right * halfExtents.x) + (up * halfExtents.y);
        Vector3 bottomLeftCorner = origin +(forward * (halfExtents.z * 2f)) + (-right * halfExtents.x) + (-up * halfExtents.y);
        Vector3 bottomRightCorner = origin +(forward * (halfExtents.z * 2f)) + (right * halfExtents.x) + (-up * halfExtents.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, topLeftCorner);
        Gizmos.DrawLine(origin, topRightCorner);
        Gizmos.DrawLine(origin, bottomLeftCorner);
        Gizmos.DrawLine(origin, bottomRightCorner);

        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
        Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner);
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
    }
}
