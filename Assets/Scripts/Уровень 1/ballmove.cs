using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ballmove : MonoBehaviour
{
    public float movementSpeedright = 0.05f; //скорость движения мяча вправо
    public float movementSpeedup = 0.05f; //скорость движения мяча вверх
    public GameObject rightwall; //объект правая стенка
    public GameObject lefttwall; //объект левая стенка
    public GameObject ball; //объект мячик
    public GameObject upwall; // объект верхняя стена
    public GameObject raketka; // объект ракетка
    public GameObject celone; //цель 1
    public GameObject celtwo; //цель 2
    public GameObject celthree; //цель 3
    public GameObject celfour; //цель 4
    public GameObject celfive; //цель 5
    public GameObject celsix; //цель 6
    public GameObject GameOver; //Текст GameOver
    public GameObject GameOver2; //Текст GameOver2
    public bool gameover = false; // когда игра проиграна



    // Start is called before the first frame update
    void Start()
    {
        //рандомный массив
        float[] randomspeedright = new float[] { -0.1f, 0.1f };
        //выбираем рандомное число из массива
        movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];

        //скорость вверх
        movementSpeedup = 0.1f;

    }



    // Update is called once per frame
    void Update()
    {

        //Если все цели ращрушены 
        if (!celone.activeSelf && !celtwo.activeSelf && !celthree.activeSelf && !celfour.activeSelf && !celfive.activeSelf && !celsix.activeSelf)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 2");
        }

        //Если мяч упал в бездну

        if (ball.transform.position.x >= raketka.transform.position.x + 2f && ball.transform.position.y < -7.5 || ball.transform.position.x <= raketka.transform.position.x && ball.transform.position.y < -7.5)
        {
            ball.SetActive(false); // исчезает мяч
            GameOver.SetActive(true); // появляется текст игра проиграна
            GameOver2.SetActive(true);
            gameover = true;
            raketka.GetComponent<raketkadvijenie>().enabled = false; // отключаем скрипт движения ракетки
        }


        //СТОЛКНОВЕНИЯ СО СТЕНКАМИ И РАКЕТКОЙ
        transform.Translate(movementSpeedright, movementSpeedup, 0);


        //если столкнулся с правой стеной

        if (ball.transform.position.x > rightwall.transform.position.x - 1f)

        {
            movementSpeedright = -0.1f;

        }

        //если столкнулся с левой стеной

        if (ball.transform.position.x < lefttwall.transform.position.x + 1f)
        {
            float[] randomspeedup = new float[] { -0.1f, -0.07f, 0.07f, 0.1f };
            movementSpeedup = randomspeedup[Random.Range(0, randomspeedup.Length)];
            movementSpeedright = 0.1f;
        }

        //если столкнулся с верхней стеной
        if (ball.transform.position.y > upwall.transform.position.y - 1f)
        {
            movementSpeedright = Random.Range(-0.1f, 0.1f);
            movementSpeedup = -0.1f;
        }


        //если столкнулся с ракеткой в правом крае полет мяча в право
        if (ball.transform.position.x <= raketka.transform.position.x + 2f && ball.transform.position.x >= raketka.transform.position.x && ball.transform.position.y <= -5 && ball.transform.position.y >= -6) //&& ball.transform.position.y == raketka.transform.position.y
        {
            movementSpeedright = 0.1f;
            movementSpeedup = 0.1f;
        }

        //если столкнулся с ракеткой в левом крае - полет мяча в лево
        if (ball.transform.position.x <= raketka.transform.position.x && ball.transform.position.x >= raketka.transform.position.x - 2f && ball.transform.position.y <= -5 && ball.transform.position.y >= -6) //&& ball.transform.position.y == raketka.transform.position.y
        {
            movementSpeedright = -0.1f;
            movementSpeedup = 0.1f;
        }

        //УНИЧТОЖЕНИЕ ОБЪЕКТОВ

        //уничтожение цели 1
        if (ball.transform.position.x <= celone.transform.position.x + 2f && ball.transform.position.x >= celone.transform.position.x - 2f && ball.transform.position.y < 3 && ball.transform.position.y > 1 && celone.activeSelf)
        {

        if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
        {
            celone.SetActive(false);
            float[] randomspeedright = new float[] { -0.1f, 0.1f };
            movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
            movementSpeedup = -0.1f;
            }

        else  // отскакивание вверх при ударе сверху
        {
            celone.SetActive(false);
            float[] randomspeedright = new float[] { -0.1f, 0.1f };
            movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
            movementSpeedup = 0.1f;
            }
    }

        //уничтожение цели 2
        if (ball.transform.position.x <= celtwo.transform.position.x + 2f && ball.transform.position.x >= celtwo.transform.position.x - 2f && ball.transform.position.y < 3 && ball.transform.position.y > 1 && celtwo.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celtwo.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celtwo.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
            }
        }
        //уничтожение цели 3
        if (ball.transform.position.x <= celthree.transform.position.x + 2f && ball.transform.position.x >= celthree.transform.position.x - 2f && ball.transform.position.y < 3 && ball.transform.position.y > 1 && celthree.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celthree.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celthree.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
            }
            }

        //уничтожение цели 4
        if (ball.transform.position.x <= celfour.transform.position.x + 2f && ball.transform.position.x >= celfour.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfour.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celfour.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celfour.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
            }
        }
            //уничтожение цели 5
            if (ball.transform.position.x <= celfive.transform.position.x + 2f && ball.transform.position.x >= celfive.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfive.activeSelf)
            {
                if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
                {
                    celfive.SetActive(false);
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = -0.1f;
            }
                else  // отскакивание вверх при ударе сверху
                {
                    celfive.SetActive(false);
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = 0.1f;
            }
            }
        //уничтожение цели 6
        if (ball.transform.position.x <= celsix.transform.position.x + 2f && ball.transform.position.x >= celsix.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celsix.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celsix.SetActive(false); //удаление цели
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celsix.SetActive(false); //удаление цели
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
            }
        }


    }
}
