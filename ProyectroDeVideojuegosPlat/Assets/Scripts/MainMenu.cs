using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void cargar(){
        //PlayerController.Load();
    }

    public void Menu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

    public void Nivel1(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }

    public void Nivel2(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }

    public void Nivel3(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }

}
