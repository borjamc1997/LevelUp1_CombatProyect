using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleDamageByRaycast : MonoBehaviour
{

    [SerializeField] private Transform startPoint, endPoint;
    Vector3 lastStartPoint, lastEndPoint;
    [SerializeField] private float rayDistance = 0.005f;

    private void Start()
    {
        StoreLastPoints();
    }

    private void Update()
    {
        float distance = Mathf.Max(
            Vector3.Distance(startPoint.position, lastStartPoint), 
            Vector3.Distance(endPoint.position, lastEndPoint));

        int numRays = Mathf.CeilToInt(distance / rayDistance) + 1;

        for (int i = 0; i < numRays; i++)
        {
            float t = (float)i / (float)numRays;
            Vector3 actualStartPoint = Vector3.Lerp(startPoint.position, lastStartPoint, t);
            Vector3 actualEndPoint = Vector3.Lerp(endPoint.position, lastEndPoint, t);

            if (Physics.Linecast(actualStartPoint, actualEndPoint, out RaycastHit hit))
            {
                hit.collider.GetComponent<HurtBox>()?.NotifyHit();
            }
            Debug.DrawLine(actualStartPoint, actualEndPoint, Color.red, 1f);
        }
        
        StoreLastPoints();
    }

    private void StoreLastPoints()
    {
        lastStartPoint = startPoint.position;
        lastEndPoint = endPoint.position;
    }

}
