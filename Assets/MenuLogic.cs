using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public bool startpress = false;
    public GameObject logotext;
    public GameObject startext;
    public GameObject playbutton;
    public GameObject optbutton;
    public GameObject exitbutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            logotext.transform.position += (Vector3.up * 120);
            Destroy(startext);
            playbutton.SetActive(true);
            optbutton.SetActive(true);
            exitbutton.SetActive(true);
        }
    }

    public void to_game() {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void to_exit() {
        Application.Quit();
    }
}
