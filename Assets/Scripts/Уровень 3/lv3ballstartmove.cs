//скрипт начального движения мяча вместе с платформой (level 3)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv3ballstartmove : MonoBehaviour
{

    public float movementSpeed = 10; //скорость движения
    public GameObject Textlev3; // Текст уровень 3
    public GameObject maincamera; // главная камера

    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        //если нажал вправо
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        }

        // если нажал влево
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        //ограничения вправо (упор в правую стенку)
        if (transform.position.x > 8.5f)
        {
            transform.position = new Vector3(8.5f, -5, 0);
        }

        //ограничения влево (упор в левую стенку)
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, -5, 0);
        }

        //если нажал пробел, чтобы запустить мячик с платформы
        if (Input.GetKeyDown("space"))
        {
            Textlev3.SetActive(false);
            GameObject varGameObject = GameObject.Find("Мячик");
            varGameObject.GetComponent<lv3ballstartmove>().enabled = false;
            varGameObject.GetComponent<lv3ballmove>().enabled = true;

        }

    }
}