//скрипт начального движения мяча вместе с платформой (level 2)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv2ballstartmove : MonoBehaviour
{

    public float movementSpeed = 10; //скорость движения
    public GameObject Textlev2; // Текст уровень 2
    public float movementSpeedright = 0f; //скорость движения мяча вправо
    // Start is called before the first frame update
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Textlev2.SetActive(false);
            GameObject varGameObject = GameObject.Find("Мячик");
            varGameObject.GetComponent<lv2ballstartmove>().enabled = false;
            varGameObject.GetComponent<lv2ballmove>().enabled = true;
            movementSpeedright = Random.Range(-0.05f, 0.05f); // рандомные значения движения при запуске при старте
          
        }

    }
}