using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIbuttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


        void OnGUI()
        {
        GUI.skin.button.fontSize = 30;
        GUI.skin.label.fontSize = 60;

        GUI.Label(new Rect(620, 150, 800, 100), "Добро пожаловать в игру!");
        GUI.Label(new Rect(1100, 600, 800, 100), "Разработчик Петров Ф.В.");

        if (GUI.Button(new Rect(300,800, 250, 100), "Начать игру"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 1");
            }

        if (GUI.Button(new Rect(1400, 800, 250, 100), "Выход"))
        {
            Application.Quit();
        }

    }
    
       


}
