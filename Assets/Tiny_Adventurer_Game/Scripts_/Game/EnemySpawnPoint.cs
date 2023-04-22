using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemySO enemyToSpawn;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 centerPos = transform.position + new Vector3(0f, 0.5f, 0f);
        Gizmos.DrawWireCube(centerPos, Vector3.one);
        Gizmos.DrawLine(centerPos, centerPos + transform.forward * 2);
    }
}
