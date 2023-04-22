using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    private Collider damagerCollider_;
    [SerializeField] private int damage_ = 30;
    [SerializeField] private string targetTag_;
    private List<Collider> damagedTargets_;

    private void Awake()
    {
        damagerCollider_ = GetComponent<Collider>();
        damagerCollider_.enabled = false;
        damagedTargets_ = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == targetTag_ && !damagedTargets_.Contains(other))
        {
            Character targetCharacter_ = other.GetComponent<Character>();

            if (targetCharacter_ != null)
            {
                targetCharacter_.HealthDamage(damage_, transform.parent.position);

                PlayerVFXManager playerVFXManager_ = transform.parent.GetComponent<PlayerVFXManager>();

                if(playerVFXManager_ != null)
                {
                    RaycastHit hit_;

                    Vector3 originalPos_ = transform.position + (-damagerCollider_.bounds.extents.z) * transform.forward;

                    bool isHit_ = Physics.BoxCast(originalPos_, damagerCollider_.bounds.extents / 2, transform.forward, out hit_, transform.rotation, damagerCollider_.bounds.extents.z, 1 << 6);

                    if(isHit_)
                    {
                        playerVFXManager_.PlaySlash(hit_.point + new Vector3(0, 0.5f, 0));
                    }

                }
            }

            damagedTargets_.Add(other);
        }
    }

    public void EnableDamager()
    {
        damagedTargets_.Clear();
        damagerCollider_.enabled = true;
    }

    public void DisableDamager()
    {
        damagedTargets_.Clear();
        damagerCollider_.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if(damagerCollider_ == null)
        {
            damagerCollider_ = GetComponent<Collider>();
        }

        RaycastHit hit_;

        Vector3 originalPos_ = transform.position + (- damagerCollider_.bounds.extents.z) * transform.forward;

        bool isHit_ = Physics.BoxCast(originalPos_, damagerCollider_.bounds.extents / 2, transform.forward, out hit_, transform.rotation, damagerCollider_.bounds.extents.z, 1 << 6);

        if(isHit_)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit_.point, 0.3f);
        }
    }
}
