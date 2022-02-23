using System;
using UnityEngine;

public class FakePlayer : MonoBehaviour
{
    [SerializeField] private float interactRadius;
    [SerializeField] private GameObject interactIcon;
    private void Update()
    {
        bool shouldSetActive = false;
        if (FindNearestInteractable(out var nearest))
        {
            var interact = nearest.GetComponentInParent<Interactable>();
            if (!interact)
                return;
            if (interact.currentState == Interactable.State.InProgress)
            {
                shouldSetActive = true;
                // var closestPoint = nearest.ClosestPoint(transform.position);
                // interactIcon.transform.position =
                //     closestPoint + ((Vector2) transform.position - closestPoint).normalized * interactIconPadding;
                interactIcon.transform.position = nearest.transform.position;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interact.OnInteract();
                }
            }
        }
        
        interactIcon.SetActive(shouldSetActive);
    }

    bool FindNearestInteractable(out Collider2D nearest)
    {
        nearest = null;
        
        var results = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        float min = int.MaxValue;
        bool foundMin = false;
        foreach (var result in results)
        {
            if (result.CompareTag("Interactable") && result.GetComponentInParent<Interactable>().currentState != Interactable.State.Completed)
            {
                var closestPoint = result.ClosestPoint(transform.position);
                var delta = (closestPoint - (Vector2) transform.position).sqrMagnitude;
                if (delta < min)
                {
                    nearest = result;
                    min = delta;
                    foundMin = true;
                }
            }
        }

        return foundMin;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}