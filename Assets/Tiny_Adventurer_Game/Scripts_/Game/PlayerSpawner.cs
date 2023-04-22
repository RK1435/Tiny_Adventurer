using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab_;

    private void Awake()
    {
        Instantiate(playerPrefab_, transform.position, Quaternion.identity);
    }
}
