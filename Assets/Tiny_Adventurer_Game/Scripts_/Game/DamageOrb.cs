using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOrb : MonoBehaviour
{
    [SerializeField] private float speed_ = 2f;
    [SerializeField] private int damage_ = 10;
    [SerializeField] private ParticleSystem hitVFX_;
    private Rigidbody rb_;

    private void Awake()
    {
        rb_ = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb_.MovePosition(transform.position + transform.forward * speed_ * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character characterController = other.gameObject.GetComponent<Character>();

        if(characterController != null && characterController.isPlayer_)
        {
            characterController.HealthDamage(damage_, transform.position);
        }

        Instantiate(hitVFX_, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
