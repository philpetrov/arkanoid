//движение ракетки (level 2)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lv2raketkamove : MonoBehaviour
{

    public float movementSpeed = 10; //скорость движения
    public GameObject ball; // объект мячик

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
            transform.position = new Vector3(8.5f, -6, 0);
        }

        //ограничения влево (упор в левую стенку)
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, -6, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ball.GetComponent<lv2ballmove>().gameover == true) // если игра проиграна и нажат пробел перезапуск сцены
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("перезапуск");
        }
    }
}