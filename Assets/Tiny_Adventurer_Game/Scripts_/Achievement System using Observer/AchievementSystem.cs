using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : Observer //This class is an Observer
{
    public AchievementSO achievementSO_;
    public AchievementNotificationController achievementNotificationController_;
    private void Start()
    {
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    public override void OnNotify(object value, AchievementsEnum achievementsType)
    {
        Achievement achievement = achievementSO_.achievements_[(int)achievementsType];

        if (achievementsType == AchievementsEnum.Ferocious)
        {
            string achievementKey = "Achievement - " + value;
            Debug.Log("Unlocked " + value);
            achievementNotificationController_.ShowAchivement(achievement);
        }
        else if(achievementsType == AchievementsEnum.Killer)
        {
            string achievementKey = "Achievement - " + value;
            Debug.Log("Unlocked " + value);
            achievementNotificationController_.ShowAchivement(achievement);
        }
        else if (achievementsType == AchievementsEnum.Warrior)
        {
            string achievementKey = "Achievement - " + value;
            Debug.Log("Unlocked " + value);
            achievementNotificationController_.ShowAchivement(achievement);
        }
    }
}
