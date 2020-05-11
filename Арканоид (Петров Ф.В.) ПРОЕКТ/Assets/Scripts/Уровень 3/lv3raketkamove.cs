//движение ракетки (level 3)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lv3raketkamove : MonoBehaviour
{

    public float movementSpeed = 10; //скорость движения
    public float lenghtraketka = 8.5f; //площадь ракетки (для увеличения при бонусе, увеличения ширины ракетки)



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
        if (transform.position.x > lenghtraketka)
        {
            transform.position = new Vector3(lenghtraketka, -6, 0);
        }

        //ограничения влево (упор в левую стенку)
        if (transform.position.x < -lenghtraketka)
        {
            transform.position = new Vector3(-lenghtraketka, -6, 0);
        }

        //если активен бонус расширения площадки
        if (GameObject.Find("Мячик").GetComponent<lv3ballmove>().bonuswidthplusactive == true)
        {
            lenghtraketka = 8f;

        }//для сигнала в скрипт ракетки

        //иначе обычная ширина ракетки
        if (GameObject.Find("Мячик").GetComponent<lv3ballmove>().bonuswidthplusactive == false)
        {
            lenghtraketka = 8.5f;

        }//для сигнала в скрипт ракетки


    }




    }
    