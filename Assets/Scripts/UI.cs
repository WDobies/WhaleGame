using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject Instructions;
    [SerializeField] Button button;
    public void LoadGame()
    {
            
         Time.timeScale = 1;
         SceneManager.LoadScene(1);
            
    }

    public void ShowInstructions()
    {
        MenuCanvas.SetActive(false);
        Instructions.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
