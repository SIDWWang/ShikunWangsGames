using System;
using System.Collections;
using UnityEngine;

public class CoinSlot : Interactable
{
    [SerializeField] private GameObject coin, coinSlot;
    [SerializeField] private Animation anim;
    [SerializeField] float errorMargin;

    private bool isPlaying;

    private UIController m_UIController;
    private void Start()
    {
        m_UIController = GameObject.FindObjectOfType<UIController>();
        m_UIController.UpdateCoins(1);
    }

    protected override void Execute()
    {
        if (currentState != State.Completed)
        {
            if (!isPlaying)
                StartCoroutine(DoAnimation());
            else if (IsValidSize())
            {
                SetState(State.Completed);
                coinSlot.SetActive(false);
                m_UIController.UpdateCoins(-1);
            }
                
        }
    }

    bool IsValidSize()
    {
        return (coinSlot.transform.lossyScale - coin.transform.lossyScale).magnitude < errorMargin;
    }
    

    IEnumerator DoAnimation()
    {
        isPlaying = true;
        anim.Play();

        var spriteComp = coin.GetComponent<SpriteRenderer>();
        var origColor = spriteComp.color;

        while (anim.isPlaying)
        {
            if (IsValidSize())
            {
                spriteComp.color = Color.green;
            }
            else
            {
                spriteComp.color = origColor;
            }

            if (currentState == State.Completed)
            {
                anim.Stop();
                coin.transform.localScale = coinSlot.transform.localScale;
                spriteComp.color = origColor;
            }
            yield return null;
        }
    }
}