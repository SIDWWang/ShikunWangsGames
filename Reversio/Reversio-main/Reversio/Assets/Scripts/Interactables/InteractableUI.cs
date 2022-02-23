using System;
using System.Collections;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject okIcon;
    [SerializeField] private float showSecs;

    private AudioScript audioManager;

    private void Start()
    {
        audioManager = AudioScript.Instance;
        if (interactable == null)
        {
            interactable = GetComponentInParent<Interactable>();
        }
        interactable.OnStateChange += () =>
        {
            if (interactable.currentState == Interactable.State.Completed)
            {
                StartCoroutine(ShowOk());
            }
        };
    }

    IEnumerator ShowOk()
    {
        okIcon.SetActive(true);
        audioManager.play(2);
        yield return new WaitForSeconds(showSecs);
        okIcon.SetActive(false);
    }
}