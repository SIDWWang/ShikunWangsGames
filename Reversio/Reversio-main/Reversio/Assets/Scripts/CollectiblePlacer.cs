using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePlacer : MonoBehaviour
{
    bool lerping;
    float startTime;
    float journey;
    public bool Mushroom;
    public bool Flower;
    [SerializeField] float speed;
    public Transform startMarker;
    public Transform endMarker;
    UIController m_UIController;
    [SerializeField] int pointValue = 0;
    GameObject filledBlock;

    // Start is called before the first frame update
    void Start()
    {
        m_UIController = GameObject.FindObjectOfType<UIController>();
        if(m_UIController)
            m_UIController.UpdateScore(pointValue);
        lerping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lerping){
            float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journey;

        // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
        if(endMarker != null && transform.position == endMarker.position){
            if (m_UIController)
                    m_UIController.UpdateScore(-pointValue);
            filledBlock.GetComponent<QuestionBlock>().FillUp();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        //Collider2D collider = col.collider;
        //print("colliding with something");
        if(collider.tag == "itemBlock" && !collider.gameObject.GetComponent<QuestionBlock>().isFilled && (Mushroom == collider.gameObject.GetComponent<QuestionBlock>().Mushroom)){
            //Debug.Log("item colliding");
            
            //Vector3 contactPoint = collider.ClosestPoint()
            // Vector3 center = collider.bounds.center;
             bool top = transform.position.y < collider.transform.position.y;
             if(top){
                 if(gameObject.transform.parent != null){
                 gameObject.transform.parent.gameObject.GetComponent<PlayerMover>().placeDown(this.gameObject);
                 gameObject.transform.position = collider.gameObject.transform.position + Vector3.down;
                 lerping = true;
                 startTime = Time.time;
                 journey = Vector3.Distance(gameObject.transform.position, collider.gameObject.transform.position);
                 startMarker = gameObject.transform;
                 endMarker = collider.gameObject.transform;
                 filledBlock = collider.gameObject;
                 }
             }
        }
    }

    void goIntoBlock(GameObject block){
        transform.position = Vector3.Lerp(transform.position, block.transform.position, speed * Time.deltaTime);
    }
}
