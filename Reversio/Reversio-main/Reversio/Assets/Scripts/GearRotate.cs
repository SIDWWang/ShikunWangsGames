using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotate : MonoBehaviour
{
    public float speed=5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f,0f,speed)*Time.deltaTime);
    }
}
