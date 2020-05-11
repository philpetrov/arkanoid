using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv3reload : MonoBehaviour
{
    public GameObject GameOver1; //Текст GameOver

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // активируем время при старте сцены
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameOver1.activeSelf) // если игра проиграна и нажат пробел перезапуск сцены
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("перезапуск");
        }
    }


}
