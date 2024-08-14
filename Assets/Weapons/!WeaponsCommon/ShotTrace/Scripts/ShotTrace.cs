using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrace : MonoBehaviour
{
    
    LineRenderer lineRenderer;
    private float lifeTime = 0.25f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void Init(Vector3 origin, Vector3 destination)
    {
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, destination);
        DOTween.To(
                () => lineRenderer.widthMultiplier,
                (x) => lineRenderer.widthMultiplier = x,
                0f,
                lifeTime).
            OnComplete(() => Destroy(gameObject));
    }

}
