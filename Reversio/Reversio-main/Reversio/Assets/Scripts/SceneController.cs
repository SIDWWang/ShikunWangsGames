using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().name == "EndScreen"){
                SceneManager.LoadScene("MainMenu");
            }
            else{
                SceneManager.LoadScene(sceneNum);
            }
        }
    }
}
