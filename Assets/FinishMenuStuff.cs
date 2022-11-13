using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenuStuff : MonoBehaviour
{

    public void to_menu() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
