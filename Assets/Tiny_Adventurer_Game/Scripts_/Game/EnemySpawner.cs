using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    private List<EnemySpawnPoint> enemySpawnPointList;
    public List<Character> spawnedCharacters_;
    private bool hasEnemySpawned_;
    public Collider collider_;
    public UnityEvent onAllSpawnedCharactersEliminated_;
    public int enemyCharacterCount_;
    public int enemySoFarDead_;
    public PointOfInterest pointOfInterest_;

    private void Awake()
    {
        var enemySpawnPointArray = transform.parent.GetComponentsInChildren<EnemySpawnPoint>();
        enemySpawnPointList = new List<EnemySpawnPoint>(enemySpawnPointArray);
        spawnedCharacters_ = new List<Character>();
        pointOfInterest_ = GetComponent<PointOfInterest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnEnemyCharacters();
        }
    }

    private void Update()
    {
        if(!hasEnemySpawned_ || spawnedCharacters_.Count == 0)
        {
            return;
        }

        bool allSpawnedAreDead = true;

        foreach(Character c in spawnedCharacters_)
        {
            if(c.currentState_ != Character.CharacterState.Dead)
            {
                allSpawnedAreDead = false;
                break;  
            }
        }

        if(allSpawnedAreDead)
        {
            if(onAllSpawnedCharactersEliminated_ != null)
            {
                onAllSpawnedCharactersEliminated_.Invoke();
                enemySoFarDead_ += enemyCharacterCount_;
                Debug.Log("ENEMY KILLED AFTER SPAWN: " + enemySoFarDead_);
                pointOfInterest_.EnemyKillNotifier();
            }

            spawnedCharacters_.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, collider_.bounds.size);
    }

    public void SpawnEnemyCharacters()
    {
        if (hasEnemySpawned_)
        {
            return;
        }

        hasEnemySpawned_ = true;

        foreach (EnemySpawnPoint spawnPoint in enemySpawnPointList)
        {
            if (spawnPoint.enemyToSpawn.enemyPrefab != null)
            {
                GameObject spawnedEnemyGameObject = Instantiate(spawnPoint.enemyToSpawn.enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                spawnedCharacters_.Add(spawnedEnemyGameObject.GetComponent<Character>());
            }
        }

        enemyCharacterCount_ = spawnedCharacters_.Count;
        Debug.Log("Enemys so far spawned:" + enemyCharacterCount_);

    }

}
