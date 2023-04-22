using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickUpType
    {
        Heal,
        Coin
    }

    public PickUpType pickUpType_;
    public int value_ = 20;
    [SerializeField] private ParticleSystem coinCollectedVFX_;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Character>().PickUpItem(this);

            if(coinCollectedVFX_ != null)
            {
                Instantiate(coinCollectedVFX_, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        
    }
}
