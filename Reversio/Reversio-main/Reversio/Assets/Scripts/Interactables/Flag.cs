using System;
using System.Collections;
using UnityEngine;

public class Flag : Interactable
{
    [SerializeField] private Transform upPos;
    [SerializeField] private float riseSecs, riseSpeed;

    [SerializeField] int pointValue = 5000;
    [SerializeField] GameObject m_FlagScoreUI;

    [SerializeField] AudioScript audioScript;

    private bool isPlaying;

    UIController m_UIController;

    private void Awake()
    {
        m_UIController = GameObject.FindObjectOfType<UIController>();
        m_UIController.UpdateScore(pointValue);
    }

    protected override void Execute()
    {
        if (currentState != State.Completed)
        {
            if (!isPlaying)
            {
                audioScript.play(1);
                StartCoroutine(DoAnimation());
            }

        }
    }

    protected virtual IEnumerator DoAnimation()
    {
        isPlaying = true;
        float timePassed = 0;
        
        while (timePassed < riseSecs && transform.position.y < upPos.transform.position.y)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
        
        isPlaying = false;
        if (transform.position.y > upPos.transform.position.y)
        {
            m_UIController.UpdateScore(-pointValue);
            m_FlagScoreUI.SetActive(false);
            SetState(State.Completed);
        }

    }
}