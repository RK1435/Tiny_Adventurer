using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] private float speed_ = 80f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, speed_ * Time.deltaTime, 0f), Space.World); 
    }
}
