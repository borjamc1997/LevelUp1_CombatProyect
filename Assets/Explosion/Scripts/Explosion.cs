using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    [SerializeField] float maxRadius = .5f;
    [SerializeField] float duration = 1f;
    [SerializeField] float force = 100f;
    [SerializeField] float upwardsModifier = 10f;
    [SerializeField] GameObject visualsPrefab;

    private float currentRadius = 0f;

    private void Start()
    {
        Instantiate(visualsPrefab, transform.position, Quaternion.identity);
        DOTween
            .To(
                () => currentRadius, 
                (x) => currentRadius = x, 
                maxRadius, duration
                )
            .SetEase(Ease.OutQuint)
            .OnUpdate(CheckForColliders)
            .OnComplete(() => Destroy(gameObject));
    }

    private void CheckForColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, currentRadius);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<HurtBox>()?.NotifyHit();
            collider.attachedRigidbody?.AddExplosionForce(force, transform.position, currentRadius, upwardsModifier);
        }
    }
}
