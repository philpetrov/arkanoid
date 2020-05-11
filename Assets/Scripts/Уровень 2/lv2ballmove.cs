using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lv2ballmove : MonoBehaviour
{
    public float movementSpeedright = 0f; //скорость движения мяча вправо
    public float movementSpeedup = 0f; //скорость движения мяча вверх
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
    public bool firstkicktwo = false; // первый удар по цели 2
    public bool firstkickfour = false; // первый удар по цели 4
    public bool secondkickfour = false; // второй удар по цели 4
    public bool firstkicksix = false; // первый удар по цели 6
    public float waitingTime = 0.0f;
    private float timer = 1.0f;
    private bool delaytwo = false; // переменная для задержки после 1-ого удара по 2 цели
    private bool delayfour = false; // переменная для задержки после 2-ого удара 4 цели
    private bool delayfour2 = false; // переменная для задержки после 3-ого удара 4 цели
    private bool delaysix = false; // переменная для задержки после удара 6 цели


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

        //Если все цели разрушены 
        if (!celone.activeSelf && !celtwo.activeSelf && !celthree.activeSelf && !celfour.activeSelf && !celfive.activeSelf && !celsix.activeSelf)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("уровень 3");
        }

        //Если мяч упал в бездну

        if (ball.transform.position.x >= raketka.transform.position.x + 2f && ball.transform.position.y < -7.5 || ball.transform.position.x <= raketka.transform.position.x && ball.transform.position.y < -7.5)
        {
            ball.SetActive(false); // исчезает мяч
            GameOver.SetActive(true); // появляется текст игра проиграна
            GameOver2.SetActive(true);
            gameover = true;
            raketka.GetComponent<lv2raketkamove>().enabled = false; // отключаем скрипт движения ракетки
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
        //первый удар по цели 2
        if (ball.transform.position.x <= celtwo.transform.position.x + 2f && ball.transform.position.x >= celtwo.transform.position.x - 2f && ball.transform.position.y < 3 && ball.transform.position.y > 1 && celtwo.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
                StartCoroutine(delaytwofunc()); //начало задержки времени
            }
            else  // отскакивание вверх при ударе сверху
            {
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                StartCoroutine(delaytwofunc()); //начало задержки времени
            }
        }

        //второй удар по цели 2
        if (ball.transform.position.x <= celtwo.transform.position.x + 2f && ball.transform.position.x >= celtwo.transform.position.x - 2f && ball.transform.position.y < 3 && ball.transform.position.y > 1 && celtwo.activeSelf && firstkicktwo == true)
        {
            if (movementSpeedup > 0) // отскакивание 
            {
                celtwo.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                firstkicktwo = false;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celtwo.SetActive(false);
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
                firstkicktwo = false;
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
        //уничтожение цели 4, имеющей 3 жизни
        //первый удар по цели 4
        if (ball.transform.position.x <= celfour.transform.position.x + 2f && ball.transform.position.x >= celfour.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfour.activeSelf && secondkickfour == false)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
                StartCoroutine(delayfourfunc()); //начало задержки времени
            }
            else  // отскакивание вверх при ударе сверху
            {
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                StartCoroutine(delayfourfunc()); //начало задержки времени
            }
        }

            // второй удар по цели 4, имеющей 3 жизни
            if (ball.transform.position.x <= celfour.transform.position.x + 2f && ball.transform.position.x >= celfour.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfour.activeSelf && firstkickfour == true)
            {
                if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
                {
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = 0.1f;
                    firstkickfour = false;
                    StartCoroutine(delayfourfunc2());
                }
                else  // отскакивание вверх при ударе сверху
                {
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = -0.1f;
                    firstkickfour = false;
                    StartCoroutine(delayfourfunc2());
            }

            }

        // третий удар по цели 4, имеющей 3 жизни
        if (ball.transform.position.x <= celfour.transform.position.x + 2f && ball.transform.position.x >= celfour.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfour.activeSelf && secondkickfour == true)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celfour.SetActive(false); //
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
                secondkickfour = false;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celfour.SetActive(false); //
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                secondkickfour = false;
            }

        }

        //уничтожение цели 5
        if (ball.transform.position.x <= celfive.transform.position.x + 2f && ball.transform.position.x >= celfive.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6  && celfive.activeSelf)
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

        //уничтожение цели 6, имеющей 2 жизни
        //первый удар по цели 6
        if (ball.transform.position.x <= celsix.transform.position.x + 2f && ball.transform.position.x >= celsix.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celsix.activeSelf)
        {
                if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
                {
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = -0.1f;
                    StartCoroutine(delaysixfunc()); //для задержки времени
                }
                else  // отскакивание вверх при ударе сверху
                {
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = 0.1f;
                    StartCoroutine(delaysixfunc()); //для задержки времени
                }
            }

            //второй удар по цели 6
            if (ball.transform.position.x <= celsix.transform.position.x + 2f && ball.transform.position.x >= celsix.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celsix.activeSelf && firstkicksix == true)
            {
                if (movementSpeedup > 0) // отскакивание 
                {
                    celsix.SetActive(false); //удаление цели
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = 0.1f;
                    firstkicksix = false;
              
            }
                else  // отскакивание вверх при ударе сверху
                {
                    celsix.SetActive(false); //удаление цели
                    float[] randomspeedright = new float[] { -0.1f, 0.1f };
                    movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                    movementSpeedup = -0.1f;
                    firstkicksix = false;
             
            }
    }
    }
           
       
    


    private IEnumerator delaytwofunc() //задержка после 1 удара по 2 цели
    {
        delaytwo = true;
        // перед началом ожидания
        yield return new WaitForSeconds(0.1f);
        // после ожидания
        delaytwo = false;
        firstkicktwo = true; // цель готова ко 2-ому удару
    }

    private IEnumerator delayfourfunc() //задержка после 1 удара по 4 цели
    {
        delayfour = true;
        // перед началом ожидания
        yield return new WaitForSeconds(0.1f);
        // после ожидания
        delayfour = false;
        firstkickfour = true; // цель готова ко 2-ому удару
    }

    private IEnumerator delayfourfunc2() //задержка после 2-ого удара по 4 цели
    {
        delayfour2 = true;
        // перед началом ожидания
        yield return new WaitForSeconds(0.1f);
        // после ожидания
        delayfour2 = false;
        secondkickfour = true; // цель готова к 3-ему удару
    }

    private IEnumerator delaysixfunc() //задержка после 1 удара по 6 цели
    {
        delaysix = true;
        // перед началом ожидания
        yield return new WaitForSeconds(0.1f);
        // после ожидания
        delaysix = false;
        firstkicksix = true; // цель готова ко 2-ому удару
    }

    


}






                

