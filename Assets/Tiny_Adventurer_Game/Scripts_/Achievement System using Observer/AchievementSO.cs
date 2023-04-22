using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee;

[CreateAssetMenu(menuName = "Create Achievement" ,fileName = "New Achievement")]
public class AchievementSO : ScriptableObject
{
    [Reorderable(sortable = false, paginate = false)]
    public AchievementsArray achievements_;

    [System.Serializable]
    public class AchievementsArray : ReorderableArray<Achievement>{ }
}
