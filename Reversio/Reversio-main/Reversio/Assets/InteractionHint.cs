using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private Animation anim;
    private void OnEnable()
    {
        if (!anim.isPlaying)
            anim.Play();
    }
}
