using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LinkedLadder : MonoBehaviour
{
    private LinkedLadder nextLadder;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (nextLadder == null)
        {
            nextLadder = Instantiate(this, transform.position + Vector3.up, transform.rotation);
        }
    }
}
