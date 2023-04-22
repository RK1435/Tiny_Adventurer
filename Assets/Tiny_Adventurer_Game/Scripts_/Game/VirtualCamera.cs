using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    [TagField]
    public string Tag = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCameraBase>();
        if (vcam != null && Tag.Length > 0)
        {
            var targets = GameObject.FindGameObjectsWithTag(Tag);
            if (targets.Length > 0)
                vcam.LookAt = vcam.Follow = targets[0].transform;
        }
    }
}
