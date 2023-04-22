using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayEnemy : MonoBehaviour
{
    public EnemySO enemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy.enemyPrefab);
    }

}
