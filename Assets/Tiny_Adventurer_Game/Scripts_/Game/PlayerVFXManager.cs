using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStep_;
    [SerializeField] private ParticleSystem blade_01;
    [SerializeField] private ParticleSystem blade_02;
    [SerializeField] private ParticleSystem blade_03;
    [SerializeField] private VisualEffect slash_;
    [SerializeField] private VisualEffect heal_;
    public void UpdateFootStep(bool state)
    {
        if(state)
        {
            footStep_.Play();
        }
        else
        {
            footStep_.Stop();
        }
    }

    public void PlayBlade01()
    {
        blade_01.Play();
    }

    public void PlayBlade02()
    {
        blade_02.Play();
    }

    public void PlayBlade03()
    {
        blade_03.Play();
    }

    public void StopBlade()
    {
        blade_01.Simulate(0);
        blade_01.Stop();

        blade_02.Simulate(0);
        blade_02.Stop();

        blade_03.Simulate(0);
        blade_03.Stop();
    }

    public void PlaySlash(Vector3 pos_) 
    {
        slash_.transform.position = pos_;
        slash_.Play();
    }

    public void PlayHealVFX()
    {
        heal_.Play();
    }

}
