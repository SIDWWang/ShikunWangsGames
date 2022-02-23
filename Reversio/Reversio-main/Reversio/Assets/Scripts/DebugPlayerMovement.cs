using System;
using UnityEngine;

public class DebugPlayerMovement : MonoBehaviour
{
    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }
}