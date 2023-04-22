using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02_Shoot : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint_;
    [SerializeField] private GameObject damageOrbPrefab_;
    private Character characterController_;

    private void Awake()
    {
        characterController_ = GetComponent<Character>();
    }

    public void ShootDamageOrb()
    {
        Instantiate(damageOrbPrefab_, shootingPoint_.position, Quaternion.LookRotation(shootingPoint_.forward));
    }

    private void Update()
    {
        characterController_.RotateToTarget();
    }

}
