using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Name and Discription")]
    public string nameOfEnemy;
    public string discriptionOfEnemy;

    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;
}
