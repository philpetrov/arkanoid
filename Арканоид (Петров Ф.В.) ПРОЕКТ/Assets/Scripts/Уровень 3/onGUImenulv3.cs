//ПАУЗА ИГРЫ уровень 3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGUImenulv3 : MonoBehaviour
{
    bool paused = false; //пауза игры во время игры
    bool paused2 = false; // пазуа игры до и после игры
    public GameObject Textlv3; // Текст "Уровень 3"
    public bool pause = false; // пазуа отжата
    public GameObject ball; //объект мячик
    public GameObject bonusball2; //бонусный мяч
    public GameObject Textlev3; // Текст уровень 3

    void Update()
    {
        //ПАЗУА ИГРЫ во время игры, если нажал escape, игра не проиграна и текст "уровень 3" активен
        if (Input.GetKeyDown("escape") && ball.GetComponent<lv3ballmove>().gameover == false && !Textlev3.activeSelf)
        {
            paused = togglePause();
        }

        //ПАЗУА ИГРЫ до и после игры
        if (Input.GetKeyDown("escape") && Textlev3.activeSelf || Input.GetKeyDown("escape") && ball.GetComponent<lv3ballmove>().gameover == true)
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 3");
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
                UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 3");
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
            Debug.Log("пауза 1 во время игры нажата");
            Time.timeScale = 1f;
            ball.GetComponent<lv3ballmove>().enabled = true;
            bonusball2.GetComponent<lv3ballmove>().enabled = true; //включаем скрипт движения бонусного мяча 2
            return (false);
            pause = true; // пауза нажата
        }
        else
        {
            Debug.Log("пауза 1 во время игры отжата");
            Time.timeScale = 0f;
            ball.GetComponent<lv3ballmove>().enabled = false;
            bonusball2.GetComponent<lv3ballmove>().enabled = false; //отключаем скрипт движения бонусного мяча 2
            return (true);
            pause = false; // пауза отжата;

        }
    }

    //функция паузы до и после игры
    bool togglePause2()
    {
        if (Time.timeScale == 0f)
        {
            Debug.Log("пауза 2 нажата");
            Time.timeScale = 1f;
            ball.GetComponent<lv3ballstartmove>().enabled = true;
            return (false);
           
        }
        else
        {
            Debug.Log("пауза 2 отжата");
            Time.timeScale = 0f;
            ball.GetComponent<lv3ballstartmove>().enabled = false;
            return (true);
    

        }
    }

}
    
   




