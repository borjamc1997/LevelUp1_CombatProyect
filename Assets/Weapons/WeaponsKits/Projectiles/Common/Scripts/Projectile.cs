using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Serializable]
    class PrefabToInstantiateInfo
    {
        public GameObject prefab;
        public enum OrientationMode { Identity, Forward, Backward, WorldUp };
        public OrientationMode orientationMode;
    };
    [SerializeField] private bool destroyOnCollision;
    [SerializeField] private PrefabToInstantiateInfo[] prefabsToInstantiate;

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnCollision)
            Destroy(gameObject);

        foreach(PrefabToInstantiateInfo info in prefabsToInstantiate)
        {
            Quaternion orientation = SetupOrientation(info);
            Instantiate(info.prefab, collision.contacts[0].point, orientation);
        }
    }

    private Quaternion SetupOrientation(PrefabToInstantiateInfo info)
    {
        Quaternion orientation = Quaternion.identity;
        switch (info.orientationMode)
        {
            case PrefabToInstantiateInfo.OrientationMode.Identity:
                break;
            case PrefabToInstantiateInfo.OrientationMode.Forward:
                orientation = Quaternion.LookRotation(transform.forward, transform.up);
                break;
            case PrefabToInstantiateInfo.OrientationMode.Backward:
                orientation = Quaternion.LookRotation(-transform.forward, transform.up);
                break;
            case PrefabToInstantiateInfo.OrientationMode.WorldUp:
                orientation = Quaternion.LookRotation(Vector3.up, -transform.forward);
                break;
        }

        return orientation;
    }
}
