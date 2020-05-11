//ПАУЗА ИГРЫ уровень 2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGUImenulv2 : MonoBehaviour
{
    bool paused = false; //пауза игры во время игры
    bool paused2 = false; // пазуа игры до и после игры
    public GameObject ball; //объект мячик
    public GameObject Textlev2; // Текст уровень 1

    void Start()
    {
       
    }


    void Update()
    {
        //ПАЗУА ИГРЫ во время игры, если нажал escape, игра не проиграна и текст "уровень 1" активен
        if (Input.GetKeyDown("escape") && ball.GetComponent<lv2ballmove>().gameover == false && !Textlev2.activeSelf)
        {
            paused = togglePause();
        }

        //ПАЗУА ИГРЫ до и после игры
        if (Input.GetKeyDown("escape") && Textlev2.activeSelf || Input.GetKeyDown("escape") && ball.GetComponent<lv2ballmove>().gameover == true)
        {
            paused2 = togglePause2();
        }

    }


    void OnGUI()
    {
        if (paused)
        {
            GUI.Box(new Rect(610, 120, 700, 550), "Игра остановлена!");
            if (GUI.Button(new Rect(810, 250, 300, 50), "Продолжить игру"))
            {
                paused = togglePause();
            }

            if (GUI.Button(new Rect(810, 350, 300, 50), "Главное меню"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Главное меню");
            }

            if (GUI.Button(new Rect(810, 450, 300, 50), "Уровень сначала"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 2");
            }

            if (GUI.Button(new Rect(810, 550, 300, 50), "Выход"))
            {
                Application.Quit();
            }
        }

        if (paused2)
        {
            GUI.Box(new Rect(610, 120, 700, 550), "Игра остановлена!");
            if (GUI.Button(new Rect(810, 250, 300, 50), "Продолжить игру"))
            {
                paused2 = togglePause2();
            }

            if (GUI.Button(new Rect(810, 350, 300, 50), "Главное меню"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Главное меню");
            }

            if (GUI.Button(new Rect(810, 450, 300, 50), "Уровень сначала"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 2");
            }

            if (GUI.Button(new Rect(810, 550, 300, 50), "Выход"))
            {
                Application.Quit();
            }

        }



    }


    //функция пазуы игры во время игры
    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            ball.GetComponent<lv2ballmove>().enabled = true;
            return (false);

        }
        else
        {
            Time.timeScale = 0f;
            ball.GetComponent<lv2ballmove>().enabled = false;
            return (true);

        }
    }

    //функция паузы до и после игры
    bool togglePause2()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            ball.GetComponent<lv2ballstartmove>().enabled = true;
            return (false);

        }
        else
        {
            Time.timeScale = 0f;
            ball.GetComponent<lv2ballstartmove>().enabled = false;
            return (true);


        }
    }

}