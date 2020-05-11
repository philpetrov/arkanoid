//перезапуск уровня после проигрыша и нажатия пробел

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv1reload : MonoBehaviour
{
    public GameObject GameOver; //Текст GameOver

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // активируем время при старте сцены
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameOver.activeSelf) // если игра проиграна и нажат пробел перезапуск сцены
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }


}