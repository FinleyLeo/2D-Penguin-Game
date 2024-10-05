using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScript : MonoBehaviour
{
    public GameObject caveText;

    private bool atEntrance;

    private Scene currentScene;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (atEntrance && Input.GetKeyDown("c"))
        {
            if (sceneName == "Level")
            {
                SceneManager.LoadScene("Level 2");

                print("Entered Cave");
            }

            else if (sceneName == "Level 2")
            {
                SceneManager.LoadScene("Level");

                print("Exited Cave");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atEntrance = true;
            
            caveText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atEntrance = false;
            caveText.SetActive(false);
        }
    }
}
