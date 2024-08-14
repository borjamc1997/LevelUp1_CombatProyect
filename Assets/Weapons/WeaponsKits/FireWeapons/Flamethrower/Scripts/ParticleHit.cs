using UnityEngine;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleHit : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        other.GetComponent<HurtBox>()?.NotifyHit();
    }

}
