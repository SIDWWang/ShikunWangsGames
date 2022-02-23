using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public bool isFilled;
    public bool Mushroom;
    public bool Flower;
    [SerializeField] private Sprite completedSprite;
    [SerializeField] GameObject ParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        if(ParticleSystem)
            ParticleSystem.SetActive(true);
        isFilled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillUp(){
        isFilled = true;
        GetComponent<SpriteRenderer>().sprite = completedSprite;
        if (ParticleSystem)
            ParticleSystem.SetActive(false);
    }
}
