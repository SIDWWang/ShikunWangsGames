using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject m_PlayerToFollow;
    [SerializeField] float m_XMin;
    [SerializeField] float m_XMax;
    [SerializeField] float m_CamSpeed;
    [SerializeField] float m_PixelsPerUnitTiles;
    
    float posYP = -4.0f;
    [SerializeField] AudioScript m_AudioScript;
    // Start is called before the first frame update
    void Start()
    {
        float playerXPos = m_PlayerToFollow.transform.position.x;
        transform.position = new Vector3(Mathf.Clamp(playerXPos, m_XMin, m_XMax), transform.position.y,transform.position.z);
        StartCoroutine(m_AudioScript.PlayDelayed(6.5f, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerXPos = m_PlayerToFollow.transform.position.x;
        float playerYPos = m_PlayerToFollow.transform.position.y;
        float posY = transform.position.y;
        //Debug.Log(posY);
        
        if(playerYPos<posYP){
            posY = playerYPos+(-posYP+1.3f);
        }
        
        //Vector3 goalPosition = new Vector3(Mathf.Clamp(playerXPos, m_XMin,m_XMax),transform.position.y,transform.position.z);
        Vector3 goalPosition = new Vector3(Mathf.Clamp(playerXPos, m_XMin,m_XMax),posY,transform.position.z);
        Vector3 lerpPosition = Vector3.Lerp(transform.position, goalPosition, m_CamSpeed);
        transform.position = lerpPosition;
    }
}
