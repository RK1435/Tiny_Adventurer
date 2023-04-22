using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object value, AchievementsEnum achievementsType);
}

public abstract class Subject : MonoBehaviour
{
    private List<Observer> observers_ = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        observers_.Add(observer);
    }

    public void Notify(object value, AchievementsEnum achievementsType)
    {
        foreach (Observer observer in observers_)
        {
            observer.OnNotify(value, achievementsType);
        }
    }
}
