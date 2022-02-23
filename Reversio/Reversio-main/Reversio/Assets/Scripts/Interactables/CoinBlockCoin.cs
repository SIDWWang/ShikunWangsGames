using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinBlockCoin : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem sprayFx;
    public CoinBlock blockParent;
    private Vector3 spawnPoint;
    public bool isOverlap;
    private SpriteRenderer sprite;
    private Color origColor;

    private void Start()
    {
        spawnPoint = transform.position;
        speed = Random.Range(speed / 2, speed * 2);
        sprite = GetComponent<SpriteRenderer>();
        origColor = sprite.color;
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed;
        if (transform.position.y < blockParent.endPos.position.y)
        {
            transform.position = spawnPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CoinLine"))
        {
            isOverlap = true;
            sprite.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CoinLine"))
        {
            isOverlap = false;
            sprite.color = origColor;
        }
    }

    public void Collect()
    {
        Instantiate(sprayFx, transform.position, sprayFx.transform.rotation);
        Destroy(this.gameObject);
    }
}