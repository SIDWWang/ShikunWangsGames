using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
private AudioScript audioManager;
    public GameObject ladderPrefab;
    public GameObject ladderMain;
    Rigidbody2D playerBody;
    Dictionary <GameObject, float> maxHeight;
    float speed;
    [SerializeField] float maxVel;
    bool colliding;
    [SerializeField] private LayerMask platformMask;
    [SerializeField] private float ladderHeight;
    [SerializeField] private float ladderClimbSpeed;
    [SerializeField] private GameObject eIcon;
    bool isHolding;
    bool isPickingUp;

    enum Direction {left, right};
    Direction dir;

    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
audioManager = AudioScript.Instance;
        playerBody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        speed = 0.0f;
        maxVel = 8.0f;
        colliding = false;
        maxHeight = new Dictionary<GameObject, float>();
        isHolding = false;
        isPickingUp = false;
        dir = Direction.left;
    }

    // Update is called once per frame
    void Update()
    {
        if(!colliding){
            if(Input.GetButtonDown("Climb") && isGrounded()){
                createNewLadder();
audioManager.play(5);
            }
        }
        if(Input.GetKey("d") || Input.GetKey("a")){
            if(Input.GetKey("d") && Input.GetKey("a")){
                m_Animator.SetBool("IsWalking", false);
                speed -= 0.5f;
                if (speed <= 0.0f){
                    speed = 0.0f;
                    m_Animator.SetBool("IsSliding", false);
                }
                else
                {
                    m_Animator.SetBool("IsSliding", true);
                }
            }
            else if(Input.GetKey("d")){
                m_Animator.SetBool("IsWalking", true);
                speed += 0.5f;
                if(speed > maxVel){
                    speed = maxVel;
                }
                dir = Direction.right;
                m_SpriteRenderer.flipX = false;
            }
            else{
                m_Animator.SetBool("IsWalking", true);
                speed -= 0.5f;
                if (speed < -1.0f * maxVel){
                    speed = -1.0f * maxVel;
                }
                dir = Direction.left;
                m_SpriteRenderer.flipX = true;
            }
        }
        else{
            m_Animator.SetBool("IsWalking", false);
            speed -= 0.5f;
            if (speed < 0.0f){
                    speed = 0.0f;
                m_Animator.SetBool("IsSliding", false);
            }
            else
            {
                m_Animator.SetBool("IsSliding", true);
            }
        }
        if(isHolding && Input.GetKeyDown("e") && !isPickingUp){
            placeDown(gameObject.transform.GetChild(0).gameObject);
audioManager.play(7);
        }
        isPickingUp = false;
        playerBody.velocity = new Vector2(speed, playerBody.velocity.y);
        if(gameObject.transform.childCount > 0){
            gameObject.transform.GetChild(0).position = transform.position + Vector3.up;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "LadderMain(Clone)"){
            colliding = true;
        }

        if (col.gameObject.tag == "Collectible")
        {
            eIcon.transform.position = col.transform.position;
            eIcon.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.transform.parent != null && col.gameObject.transform.parent.gameObject.name == "LadderMain(Clone)"){
            if(Input.GetButton("Climb")){
                playerBody.velocity = new Vector2(playerBody.velocity.x, ladderClimbSpeed);
                if(transform.position.y >= maxHeight[col.gameObject.transform.parent.gameObject] - ladderHeight){
                    GameObject piece = Instantiate(ladderPrefab, col.gameObject.transform.position + Vector3.up, col.gameObject.transform.rotation);
                    piece.transform.SetParent(col.gameObject.transform.parent.gameObject.transform);
                    maxHeight[col.gameObject.transform.parent.gameObject] += 1;
                }
            }
            colliding = true;
            if(Input.GetKeyDown("s")){
                Destroy(col.gameObject.transform.parent.gameObject);
                colliding = false;
            }
        }

        if(col.gameObject.tag == "Collectible"){
            
            if (eIcon.activeSelf)
                eIcon.transform.position = col.transform.position;
            if(Input.GetKeyDown("e") && !isHolding){
                pickUp(col.gameObject);
audioManager.play(6);
                isHolding = true;
                isPickingUp = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.transform.parent != null && col.gameObject.transform.parent.name == "LadderMain(Clone)"){
            colliding = false;
            //Debug.Log("Not colliding");
        }

        if (col.gameObject.tag == "Collectible")
        {
            eIcon.SetActive(false);
        }
    }

    private bool isGrounded(){
        float off = 0.1f;
        RaycastHit2D groundCheck = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center, Vector2.down, GetComponent<BoxCollider2D>().bounds.extents.y + off, platformMask);
        return groundCheck.collider != null;
    }

    void createNewLadder(){
        GameObject lad = Instantiate(ladderMain, transform.position - new Vector3(0.0f, 0.5f, 0.0f), transform.rotation);
audioManager.play(5);
        GameObject piece = Instantiate(ladderPrefab, lad.transform.position, lad.transform.rotation);
        piece.transform.SetParent(lad.transform);
        maxHeight.Add(lad, transform.position.y);
        colliding = true;
    }

    void pickUp(GameObject collect){
        collect.transform.position = transform.position + Vector3.up;
        collect.transform.SetParent(this.gameObject.transform);
    }

    public void placeDown(GameObject hold){
        if(dir == Direction.right){
            hold.transform.position = hold.transform.parent.position + Vector3.right;
        }
        else{
            hold.transform.position = hold.transform.parent.position + Vector3.left;
        }
        hold.transform.parent = null;
        isHolding = false;
    }
}
