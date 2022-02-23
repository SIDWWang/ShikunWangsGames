using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool enter = true;
    public GameObject mc;
    public GameObject brc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(enter){
            col.gameObject.transform.position = new Vector2(218.5f, -2.9f);
            mc.SetActive(false);
            brc.SetActive(true);
        }
        else{
            col.gameObject.transform.position = new Vector2(194.5f, -2.9f);
            mc.SetActive(true);
            brc.SetActive(false);
        }
    }
    
}
