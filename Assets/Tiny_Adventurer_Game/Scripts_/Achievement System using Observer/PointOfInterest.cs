using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : Subject //Inherited from Subject Class
{
    [SerializeField] private string poiName;
    public EnemySpawner enemySpawner_;

    public void EnemyKillNotifier()
    {
        if(enemySpawner_.enemySoFarDead_ == 2)
        {
            Notify("Ferocious", AchievementsEnum.Ferocious);
        }
        else if(enemySpawner_.enemySoFarDead_ == 3)
        {
            Notify("Killer", AchievementsEnum.Killer);
        }
        else if(enemySpawner_.enemySoFarDead_ == 4)
        {
            Notify("Warrior", AchievementsEnum.Warrior);
        }
    }

}
