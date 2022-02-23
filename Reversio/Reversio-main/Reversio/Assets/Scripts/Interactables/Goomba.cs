using System;
using UnityEngine;

public class Goomba : Interactable
{
    [SerializeField] private float shrinkFactor, inflateFactor;
    [SerializeField] private float minShrink;
    [SerializeField] private SpriteRenderer goombaSpriteRenderer;
    [SerializeField] private SpriteRenderer scoreSpriteRenderer;
    [SerializeField] private Sprite goombaFlat;
    [SerializeField] private Sprite goombaFull;
    [SerializeField] private GameObject goombaParticleSystem;
    [SerializeField] int pointValue = 100;
    [SerializeField] float wanderRange = 2;
    [SerializeField] float moveSpeed = 1;

    private float currentInflation;
    private bool isInflated;
    private bool isPonging;

    private Vector3 spawnOrigin;
    private Vector3 leftGoal;
    private Vector3 rightGoal;
    private Vector3 goalPoint;

    UIController m_UIController;


    private AudioScript audioManager;

    private void Start()
    {
audioManager = AudioScript.Instance;
        m_UIController = GameObject.FindObjectOfType<UIController>();
        if(m_UIController)
            m_UIController.UpdateScore(pointValue);
        spawnOrigin = transform.position;
        leftGoal = transform.position - new Vector3(wanderRange,0,0);
        rightGoal = transform.position + new Vector3(wanderRange, 0, 0);
        goalPoint = leftGoal;
    }
    private void Update()
    {
        var scale = goombaSpriteRenderer.gameObject.transform.localScale;
        if (scale.y < .5f)
        {
            goombaSpriteRenderer.sprite = goombaFlat;
        }
        else
        {
            goombaSpriteRenderer.sprite = goombaFull;
        }
        if (!isInflated)
        {
            
            if (scale.y > minShrink)
            {
                scale.y -= shrinkFactor * Time.deltaTime;
                goombaSpriteRenderer.gameObject.transform.localScale = scale;
            }
            if (scale.y >= 1)
            {
                goombaSpriteRenderer.gameObject.transform.localScale = Vector3.one;
                isInflated = true;
                goombaParticleSystem.SetActive(false);
                if (m_UIController)
                    m_UIController.UpdateScore(-pointValue);
                GetComponent<Animator>().SetBool("IsInflated",true);
                scoreSpriteRenderer.gameObject.SetActive(false);
                SetState(State.Completed);
            }
        }
        if(isInflated)
        {
            transform.position = Vector3.Lerp(leftGoal, rightGoal, Mathf.PingPong(Time.time, 1 * moveSpeed));
        }
    }

    protected override void Execute()
    {
        goombaSpriteRenderer.gameObject.transform.localScale += transform.up * inflateFactor * Time.deltaTime;
audioManager.play(4);
    }
}