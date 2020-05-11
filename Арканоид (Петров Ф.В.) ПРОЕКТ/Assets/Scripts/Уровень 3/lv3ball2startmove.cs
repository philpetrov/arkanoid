//скрипт начального движения 2 бонусного мяча (level 3)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv3ball2startmove : MonoBehaviour
{

    public GameObject Textprobelzapusk; // Текст запуск мяча пробелом
    public GameObject childball2; // Дочерний объект (бонусный мяч 2) ракетки
    public GameObject maincamera; // Главная камера
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        //если нажал пробел, чтобы запустить мячик с платформы
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Textprobelzapusk.SetActive(false);
            GameObject varGameObject = GameObject.Find("Мячик 2 бонус");
            varGameObject.GetComponent<lv3ball2startmove>().enabled = false;
            varGameObject.GetComponent<lv3ballmove>().enabled = true;
            childball2.transform.SetParent(null); // перестать быть дочерним объектом ракетки
        }
    }


       
}
