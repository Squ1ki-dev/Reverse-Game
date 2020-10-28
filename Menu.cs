using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void START()
    {
        Application.Quit();
    }

    public void Exit()
    {
        SceneManager.LoadScene(5);
    }
}
