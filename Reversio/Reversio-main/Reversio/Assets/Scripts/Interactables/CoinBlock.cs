using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CoinBlock : Interactable
{
    [SerializeField] private int coinCount;
    [SerializeField] private CoinBlockCoin coin;
    [SerializeField] public Transform spawnPoint, endPos;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private Text coinCountText;
    [SerializeField] private GameObject spawnLine;
    [SerializeField] private Sprite completedSprite;
    private List<CoinBlockCoin> spawnedCoins = new List<CoinBlockCoin>();

    private int collected = 0;
    private bool isPlaying;
    
    private UIController m_UIController;
    private void Start()
    {
        m_UIController = GameObject.FindObjectOfType<UIController>();
        m_UIController.UpdateCoins(10);
    }
    
    protected override void Execute()
    {
        if (currentState != State.Completed)
        {
            if (!isPlaying)
                StartCoroutine(StartMinigame());
            else
                CheckCoinCollection();
        }
    }

    void CheckCoinCollection()
    {
        foreach (var spawnedCoin in spawnedCoins)
        {
            if (spawnedCoin.isOverlap)
            {
                spawnedCoin.Collect();
                collected++;
                coinCountText.text = collected + "/" + coinCount;
                if (collected >= coinCount)
                {
                    // end minigame
                    isPlaying = false;
                    SetState(State.Completed);
                    m_UIController.UpdateCoins(-10);
                    spawnLine.SetActive(false);
                    coinCountText.gameObject.SetActive(false);
                    GetComponent<SpriteRenderer>().sprite = completedSprite;
                }
            }
        }
    }
    
    IEnumerator StartMinigame()
    {
        isPlaying = true;
        spawnLine.SetActive(true);

        for (int i = 0; i < coinCount; i++)
        {
            var spawn = spawnPoint.position;
            spawn += Vector3.right * Random.Range(-spawnPoint.localScale.x, spawnPoint.localScale.x);
            var coinInst = Instantiate(coin, spawn, Quaternion.identity);
            coinInst.blockParent = this;
            
            spawnedCoins.Add(coinInst);
            
            yield return new WaitForSeconds(Random.Range(spawnFrequency / 2, spawnFrequency * 2));
        }
    }
}