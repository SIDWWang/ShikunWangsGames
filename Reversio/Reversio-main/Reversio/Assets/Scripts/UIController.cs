using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Text m_ScoreCounter;
    [SerializeField] Text m_CoinCounter;
    [SerializeField] Text m_TimeCounter;

    [SerializeField] int m_MarioSecondLength = 24;
    [SerializeField] int m_TimerStartTime = 160;
    [SerializeField] int m_MaxTime = 400;
    [Header("Does not count time cuz that's tedious, handled in Start")]
    [SerializeField] int m_StartScore;

    [SerializeField] private int m_StartCoins;

    [SerializeField] GameObject m_PlayerObject;

    float m_Timer;
    int m_SecondCount;
    int m_ScoreCount;
    private int m_CoinCount;

    public bool m_StartDecrementingTimer = false;
    public bool m_TimerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0.0f;
        m_SecondCount = m_TimerStartTime;
        UpdateTimer();
        UpdateScore(m_StartScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_StartDecrementingTimer)
        {
            if(m_SecondCount > 0)
            {
                m_SecondCount--;
                UpdateTimer();
                UpdateScore(50);
            }
            else
            {
                StartGame();
            }
        }
        if(m_SecondCount < m_MaxTime && m_TimerStarted)
        {
            m_Timer += Time.deltaTime;
            int newTime = (int)(m_Timer % m_MarioSecondLength) + (m_MarioSecondLength * (int)(m_Timer / m_MarioSecondLength));
            if(newTime != m_SecondCount)
            {
                m_SecondCount = newTime;
                UpdateTimer();
                if(m_SecondCount <= m_TimerStartTime)
                    UpdateScore(-50);
            }

        }
        if(m_ScoreCount <= 0 && m_CoinCount <= 0){
            SceneManager.LoadScene("EndScreen");
        }
    }

    void UpdateTimer()
    {
        m_TimeCounter.text = "Time\n" + m_SecondCount.ToString("000");
    }

    public void UpdateScore(int scoreModifier)
    {
        m_ScoreCount += scoreModifier;
        m_ScoreCounter.text = "Mario\n" + m_ScoreCount.ToString("000000");
    }

    public void UpdateCoins(int coinMod)
    {
        m_CoinCount += coinMod;
        m_CoinCounter.text = "\n   " + m_CoinCount.ToString("00");
    }

    void StartGame()
    {
        m_StartDecrementingTimer = false;
        m_TimerStarted = true;
        m_PlayerObject.SetActive(true);
    }
}
