using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitHintEffect : MonoBehaviour
{
    [SerializeField] GameObject ParticleEffectObject;
    [SerializeField] float m_YThreshold;

    private Camera m_Camera;

    private void Start()
    {
        m_Camera = Camera.main;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            ParticleEffectObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if (m_Camera.transform.position.y >= m_YThreshold)
            {
                ParticleEffectObject.SetActive(true);
            }
    }
}
