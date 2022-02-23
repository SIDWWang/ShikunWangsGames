using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningController : MonoBehaviour
{
    [SerializeField] GameObject m_FlagObject;
    [SerializeField] UIController m_UIController;

    public void StartGame()
    {
        m_FlagObject.SetActive(true);
        m_UIController.m_StartDecrementingTimer = true;
    }
}
