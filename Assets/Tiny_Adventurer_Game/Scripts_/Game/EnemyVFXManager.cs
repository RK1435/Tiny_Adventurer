using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class EnemyVFXManager : MonoBehaviour, IPooledObject_1
{
    [SerializeField] private VisualEffect footStep_;
    [SerializeField] private VisualEffect attackVFX;
    [SerializeField] private ParticleSystem beingHitVFX_;
    #region Being Hit Splash VFX - Old Variable
    //[SerializeField] private VisualEffect beingHitSplashVFX;
    #endregion

    public void PlayAttackVFX()
    {
        attackVFX.SendEvent("OnPlay");
    }

    public void BurstFootStep()
    {
        footStep_.SendEvent("OnPlay");
    }

    public void PlayerBeingHitVFX(Vector3 attackerPos_)
    {
        Vector3 forceForward_ = transform.position - attackerPos_;
        forceForward_.Normalize();
        forceForward_.y = 0f;
        beingHitVFX_.transform.rotation = Quaternion.LookRotation(forceForward_);
        beingHitVFX_.Play();

        OnSplashVFXSpawn();

        #region SplashVFX - Old Code
        //Vector3 splashPos_ = transform.position;
        //splashPos_.y += 2f;
        ////VisualEffect newSplashVFX_ = Instantiate(beingHitSplashVFX, splashPos_, Quaternion.identity);
        //VisualEffect newSplashVFX_ = objectPooler.SpawnFromPool("BeingHitSplashVFX", splashPos_, Quaternion.identity);
        //newSplashVFX_.SendEvent("OnPlay");
        //Destroy(newSplashVFX_.gameObject, 10f);
        #endregion
    }

    public void OnSplashVFXSpawn()
    {
        Vector3 splashPos_ = transform.position;
        splashPos_.y += 2f;
        #region SplashVFX - Old Implementation
        //VisualEffect newSplashVFX_ = Instantiate(beingHitSplashVFX, splashPos_, Quaternion.identity);
        #endregion
        VisualEffect newSplashVFX_ = ObjectPooler.objectPoolerInstance.SpawnFromPool_1_VFX("BeingHitSplashVFX", splashPos_, Quaternion.identity);
        newSplashVFX_.SendEvent("OnPlay");
        //Destroy(newSplashVFX_.gameObject, 10f);
    }
}
