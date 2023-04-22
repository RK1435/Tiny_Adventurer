using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;
using System.IO;

[CustomEditor(typeof(AchievementSO))]
public class AchievementDataBaseEditor : Editor
{
    private AchievementSO achivementDB_;

    private void OnEnable()
    {
        achivementDB_ = target as AchievementSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Enum", GUILayout.Height(30)))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "AchievementsEnum.cs");
        string code = "public enum AchievementsEnum {";
        foreach(Achievement achievement in achivementDB_.achievements_)
        {
            //ToDo: Validate ID is porper format
            code += achievement.id_ + ",";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/AchievementsEnum.cs");
    }
}
