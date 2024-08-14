using UnityEngine;

public class HitBox : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        HurtBox hurtBox = collision.collider.GetComponent<HurtBox>();
        if (hurtBox)
        {
            hurtBox.NotifyHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HurtBox hurtBox = other.GetComponent<HurtBox>();
        if (hurtBox)
        {
            hurtBox.NotifyHit();
        }
    }

}
