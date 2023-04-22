using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput_;
    public float VerticalInput_;
    public bool mouseButtonDown_;
    public bool spaceKeyDown_;

    // Update is called once per frame
    void Update()
    {
        if (!mouseButtonDown_ && Time.timeScale != 0)
        {
            mouseButtonDown_ = Input.GetMouseButtonDown(0);
        }

        if(!spaceKeyDown_ && Time.timeScale != 0)
        {
            spaceKeyDown_ = Input.GetKeyDown(KeyCode.Space);
        }

        HorizontalInput_ = Input.GetAxisRaw("Horizontal");
        VerticalInput_ = Input.GetAxisRaw("Vertical");
    }

    private void OnDisable()
    {
        ClearCache();
    }

    public void ClearCache()
    {
        mouseButtonDown_ = false;
        spaceKeyDown_ = false;
        HorizontalInput_ = 0;
        VerticalInput_ = 0;
    }
}
