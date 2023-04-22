using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour
{
    [SerializeField] private Text achievementTitleLabel_;
    private Animator animator_;

    private void Awake()
    {
        animator_ = GetComponent<Animator>();
    }
    public void ShowAchivement(Achievement achievement)
    {
        achievementTitleLabel_.text = achievement.title_;
        animator_.SetTrigger("Appear");
    }
}
