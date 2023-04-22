using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth_;
    [SerializeField] private int currHealth_;

    public float currentHealthPercentage
    {
        get
        {
            return (float)currHealth_ / (float)maxHealth_;
        }
    }

    //private Character characterController_;
    private Character character_;

    private void Awake()
    {
        currHealth_ = maxHealth_;
        character_ = GetComponent<Character>();
    }

    public void HealthDamage(int damage_)
    {
        currHealth_ -= damage_;
        Debug.Log(gameObject.name + " took Damage: " + damage_);
        Debug.Log(gameObject.name + " Current Health is: " + currHealth_);
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(currHealth_ <= 0)
        {
            character_.SwitchStateTo(Character.CharacterState.Dead);
        }
    }

    public void AddHealth(int health)
    {
        currHealth_ += health;

        if(currHealth_ > maxHealth_)
        {
            currHealth_ = maxHealth_;
        }
    }

}
