using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject gateVisual_;
    private Collider gateCollider_;
    [SerializeField] private float openDuration_ = 2f;
    [SerializeField] private float openTargetY_ = -1.5f;

    private void Awake()
    {
        gateCollider_ = GetComponent<Collider>();
    }

    IEnumerator OpenGateAnimation()
    {
        float currentOpenDuration = 0;
        Vector3 startPos = gateVisual_.transform.position;
        Vector3 targetPos = startPos + Vector3.up * openTargetY_;

        while(currentOpenDuration < openDuration_)
        {
            currentOpenDuration += Time.deltaTime;
            gateVisual_.transform.position = Vector3.Lerp(startPos, targetPos, currentOpenDuration / openDuration_);
            yield return null;
        }

        gateCollider_.enabled = false;
        yield break;
    }

    public void OpenGates()
    {
        StartCoroutine(OpenGateAnimation());
    }
}
