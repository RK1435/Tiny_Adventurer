using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool_1
    {
        public string visualEffectTag;
        public VisualEffect visualEffectPrefab;
        public int sizeOfPool_1;
    }

    [System.Serializable]
    public class Pool_2
    {
        public string enemyDropItemTag;
        public GameObject enemyDropItemPrefab;
        public int sizeOfPool_2;
    }

    #region Singleton
    public static ObjectPooler objectPoolerInstance;

    private void Awake()
    {
        if (objectPoolerInstance == null)
        {
            objectPoolerInstance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }
    #endregion

    public List<Pool_1> poolList_1;
    public Dictionary<string, Queue<VisualEffect>> visualEffectDictionary;

    public List<Pool_2> poolList_2;
    public Dictionary<string, Queue<GameObject>> enemyDropItemDictionary;

    // Start is called before the first frame update
    void Start()
    {
        visualEffectDictionary = new Dictionary<string, Queue<VisualEffect>>();

        enemyDropItemDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool_1 pool in poolList_1)
        {
            Queue<VisualEffect> visualEffectPool = new Queue<VisualEffect>();

            for (int i = 0; i < pool.sizeOfPool_1; i++)
            {
                VisualEffect visualEffect = Instantiate(pool.visualEffectPrefab);
                visualEffect.enabled = false;
                visualEffectPool.Enqueue(visualEffect);
            }

            visualEffectDictionary.Add(pool.visualEffectTag, visualEffectPool);
        }

        foreach (Pool_2 pool in poolList_2)
        {
            Queue<GameObject> enemyDropItemPool = new Queue<GameObject>();

            for (int i = 0; i < pool.sizeOfPool_2; i++)
            {
                GameObject enemyItemDrop = Instantiate(pool.enemyDropItemPrefab);
                enemyItemDrop.SetActive(false);
                enemyDropItemPool.Enqueue(enemyItemDrop);
            }

            enemyDropItemDictionary.Add(pool.enemyDropItemTag, enemyDropItemPool);
        }
    }

    public VisualEffect SpawnFromPool_1_VFX(string visualEffectTag, Vector3 splashPos, Quaternion splashRotation)
    {
        if(!visualEffectDictionary.ContainsKey(visualEffectTag))
        {
            Debug.LogWarning("Pool with Visual Effect Tag " + visualEffectTag + " doesn't Exist");
            return null;
        }

        VisualEffect splashVFXToSpawn = visualEffectDictionary[visualEffectTag].Dequeue();
        splashVFXToSpawn.enabled = true;
        splashVFXToSpawn.transform.position = splashPos;
        splashVFXToSpawn.transform.rotation = splashRotation;

        IPooledObject_1 pooledObj = splashVFXToSpawn.GetComponent<IPooledObject_1>();

        if(pooledObj != null)
        {
            pooledObj.OnSplashVFXSpawn();
        }

        visualEffectDictionary[visualEffectTag].Enqueue(splashVFXToSpawn);

        return splashVFXToSpawn;
    }

    public GameObject SpawnFromPool_2_GameObject(string enemyDropItemTag, Vector3 enemyDropItemPos, Quaternion enemyDropItemRotation)
    {
        if (!enemyDropItemDictionary.ContainsKey(enemyDropItemTag))
        {
            Debug.LogWarning("Pool with Enemy Drop Item Tag " + enemyDropItemTag + " doesn't Exist");
            return null;
        }

        GameObject enemyDropItemToSpawn = enemyDropItemDictionary[enemyDropItemTag].Dequeue();
        enemyDropItemToSpawn.SetActive(true);
        enemyDropItemToSpawn.transform.position = enemyDropItemPos;
        enemyDropItemToSpawn.transform.rotation = enemyDropItemRotation;

        IPooledObject_2 pooledObj = enemyDropItemToSpawn.GetComponent<IPooledObject_2>();

        if(pooledObj != null)
        {
            pooledObj.EnemyDropItem();
        }

        enemyDropItemDictionary[enemyDropItemTag].Enqueue(enemyDropItemToSpawn);

        return enemyDropItemToSpawn;
    }

}
