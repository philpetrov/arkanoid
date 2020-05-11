using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lv3ballmove : MonoBehaviour
{
    public float movementSpeedright = 0f; //скорость движения мяча вправо
    public float movementSpeedup = 0f; //скорость движения мяча вверх
    public float bonusspeed = 1f; //бонусная скорость
    public float lenght = 2f; //площадь соприкосновения с ракеткой (для увеличения при бонусе увеличения ширины ракетки)
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
    public GameObject bonusspeedup; //Бонус ускорения шара
    public GameObject bonusspeeddown; // Бонус замедления шара
    public GameObject bonusplusball; // Бонус доп.шар
    public GameObject bonusball2; // 2 мяч (от бонуса)
    public GameObject bonuswidthplus; // Бонус увеличение ширины ракетки
    public GameObject Textprobelzapusk;  // Текст запуск мяча пробелом
    public GameObject Textpobeditel;  // Текст конец игры - победитель
    public bool gameover = false; // когда игра проиграна
    public bool firstkicktwo = false; // первый удар по цели 2
    public bool firstkickfour = false; // первый удар по цели 4
    public bool secondkickfour = false; // второй удар по цели 4
    public bool firstkicksix = false; // первый удар по цели 6
    public bool bonusball2active = false; //для сигнала в скрипт ракетки и для уменьшения размера дочернего объекта (2 мяча) при расширении площадки
    public bool bonuswidthplusactive = false; // вторая переменная для уменьшения размера дочернего объекта (2 мяча) при расширении площадки
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

       
        //БОНУСЫ
        //1. ускорение мяча (если активен объект бонуса активен и попал в ракетку)
        if (bonusspeedup.activeSelf && bonusspeedup.transform.position.x <= raketka.transform.position.x + 2f && bonusspeedup.transform.position.x >= raketka.transform.position.x - 2f && bonusspeedup.transform.position.y < -5)
        {
            bonusspeedup.SetActive(false); // деактивация обЪекта бонус
            StartCoroutine(bonusspeedupfunc());// активируем бонус на время 5f
        }

        //2. замедление мяча (если активен объект бонуса активен и попал в ракетку)
        if (bonusspeeddown.activeSelf && bonusspeeddown.transform.position.x <= raketka.transform.position.x + 2f && bonusspeeddown.transform.position.x >= raketka.transform.position.x - 2f && bonusspeeddown.transform.position.y < -5)
        {
            bonusspeeddown.SetActive(false); // деактивация обЪекта бонус
            StartCoroutine(bonusspeeddownfunc());// активируем бонус на время 5f
        }

        //3. добавление мяча

        if (bonusplusball.activeSelf && bonusplusball.transform.position.x <= raketka.transform.position.x + 2f && bonusplusball.transform.position.x >= raketka.transform.position.x - 2f && bonusplusball.transform.position.y < -5)
        {
            bonusplusball.SetActive(false); // деактивация обЪекта бонус
            Textprobelzapusk.SetActive(true); // активация текста "нажмите пробел для запуска бонусного мяча"
            bonusball2.GetComponent<lv3ballmove>().enabled = false; // на всякий случай отключаем скрипт его движения без пробела из-за паузы
            StartCoroutine(bonusplusballfunc());// активируем бонус на время 7f
        }

        //4. расширение ракетки

        if (bonuswidthplus.activeSelf && bonuswidthplus.transform.position.x <= raketka.transform.position.x + 2f && bonuswidthplus.transform.position.x >= raketka.transform.position.x - 2f && bonuswidthplus.transform.position.y < -5)
        {
            bonuswidthplus.SetActive(false); // деактивация обЪекта бонус
            StartCoroutine(bonuswidthplusfunc());// активируем бонус на время 5f
        }

        //Если все цели разрушены 
        if (!celone.activeSelf && !celtwo.activeSelf && !celthree.activeSelf && !celfour.activeSelf && !celfive.activeSelf && !celsix.activeSelf)
        {
            Textpobeditel.SetActive(true);  // Текст конец игры - победитель
            Textprobelzapusk.SetActive(false); // исчезает текст "для запуска нажмите пробел"
            //остановка бонусов (скриптов движения)
            bonusspeedup.GetComponent<bonusmove>().enabled = false;
            bonusspeeddown.GetComponent<bonusmove>().enabled = false;
            bonusplusball.GetComponent<bonusmove>().enabled = false;
            bonuswidthplus.GetComponent<bonusmove>().enabled = false;
            //остановка ракетки
            raketka.GetComponent<lv3raketkamove>().enabled = false;
            //остановка мяча
            movementSpeedright = 0f; //скорость движения мяча вправо
            movementSpeedup = 0f; //скорость движения мяча вверх
        }

        //Если мяч упал в бездну (КОНЕЦ ИГРЫ)

        if (ball.transform.position.x >= raketka.transform.position.x + 2f && ball.transform.position.y < -7.5 || ball.transform.position.x <= raketka.transform.position.x && ball.transform.position.y < -7.5)
        {
            ball.SetActive(false); // исчезает мяч
            bonusball2.SetActive(false); // исчезает мяч2
            Textprobelzapusk.SetActive(false); // исчезает текст "для запуска нажмите пробел"
            //остановка бонусов (скриптов движения)
            bonusspeedup.GetComponent<bonusmove>().enabled = false;
            bonusspeeddown.GetComponent<bonusmove>().enabled = false;
            bonusplusball.GetComponent<bonusmove>().enabled = false;
            bonuswidthplus.GetComponent<bonusmove>().enabled = false;
            //остановка ракетки
            raketka.GetComponent<lv3raketkamove>().enabled = false;
            // появляется текст игра проиграна
            GameOver.SetActive(true);
            GameOver2.SetActive(true);
            gameover = true;

        }

        //Если бонусный 2 мяч упал в бездну 

        if (bonusball2.transform.position.x >= raketka.transform.position.x + 2f && bonusball2.transform.position.y < -7.5 || bonusball2.transform.position.x <= raketka.transform.position.x && bonusball2.transform.position.y < -7.5)
        {
            bonusball2.SetActive(false); // исчезает мяч2
        }

        // Если активны 2 бонуса (расширение площадки и второй мяч)
        if (bonusball2active == true && bonuswidthplusactive == true) 
        {
            //уменьшаем ширину мяча
            bonusball2.gameObject.transform.localScale = new Vector3(0.27f, 1, 1);
        }

        //Если бонусный мяч запущен в воздух, когда отключен скрипт его начального движения

        if (bonusball2.GetComponent<lv3ball2startmove>().enabled == false)
        {
            //возвращаем его стандартный размер
            bonusball2.gameObject.transform.localScale = new Vector3(1f, 1, 1);
        }

        //Основной полет мяча
        transform.Translate(movementSpeedright * bonusspeed, movementSpeedup * bonusspeed, 0);

        //СТОЛКНОВЕНИЯ СО СТЕНКАМИ И РАКЕТКОЙ МЯЧА
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
        if (ball.transform.position.x <= raketka.transform.position.x + lenght && ball.transform.position.x >= raketka.transform.position.x && ball.transform.position.y <= -5 && ball.transform.position.y >= -6) //&& ball.transform.position.y == raketka.transform.position.y
        {
            movementSpeedright = 0.1f;
            movementSpeedup = 0.1f;

        }

        //если столкнулся с ракеткой в левом крае - полет мяча в лево
        if (ball.transform.position.x <= raketka.transform.position.x && ball.transform.position.x >= raketka.transform.position.x - lenght && ball.transform.position.y <= -5 && ball.transform.position.y >= -6) //&& ball.transform.position.y == raketka.transform.position.y
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
                bonusspeeddown.SetActive(true); // активация обЪекта бонус замедление мяча

                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }

            else  // отскакивание вверх при ударе сверху
            {
                //рандомный массив
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                //выбираем рандомное число из массива
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                //скорость вверх
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
                celtwo.SetActive(false); //удаление кирпичика
                bonuswidthplus.SetActive(true); // активация объекта бонуса ширина ракетки
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                firstkicktwo = false;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celtwo.SetActive(false);
                bonuswidthplus.SetActive(true); // активация объекта бонуса ширина ракетки
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
        if (ball.transform.position.x <= celfive.transform.position.x + 2f && ball.transform.position.x >= celfive.transform.position.x - 2f && ball.transform.position.y < 1.5 && ball.transform.position.y > -0.6 && celfive.activeSelf)
        {
            if (movementSpeedup > 0) // отскакивание вниз при ударе снизу
            {
                celfive.SetActive(false);
                bonusspeedup.SetActive(true); //активируем объект бонус ускорения мяча 
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = -0.1f;
            }
            else  // отскакивание вверх при ударе сверху
            {
                celfive.SetActive(false);
                bonusspeedup.SetActive(true); //активируем объект бонус ускорения мяча 
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
                bonusplusball.SetActive(true); // активация обЪекта бонус доп. мяч
                float[] randomspeedright = new float[] { -0.1f, 0.1f };
                movementSpeedright = randomspeedright[Random.Range(0, randomspeedright.Length)];
                movementSpeedup = 0.1f;
                firstkicksix = false;

            }
            else  // отскакивание вверх при ударе сверху
            {
                celsix.SetActive(false); //удаление цели
                bonusplusball.SetActive(true); // активация обЪекта бонус доп. мяч
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

    //Действие бонусов (время)
    //бонус 1
    private IEnumerator bonusspeedupfunc() //бонус ускорение 
    {
        // перед началом ожидания
        bonusspeed = 1.5f; // бонус работает
        yield return new WaitForSeconds(5f);
        // после ожидания
        bonusspeed = 1f; // бонус не работает
    }

    //бонус 2
    private IEnumerator bonusspeeddownfunc() //бонус замедление
    {
        // перед началом ожидания
        bonusspeed = 0.6f; // бонус работает
        yield return new WaitForSeconds(5f);
        // после ожидания
        bonusspeed = 1f; // бонус не работает
    }


    private IEnumerator bonusplusballfunc() //бонус добавление мяча
    {
        // перед началом ожидания
        // бонус работает
        bonusball2.SetActive(true); // появлется 2 мяч
        
        bonusball2active = true; 
        yield return new WaitForSeconds(7f);
        // после ожидания
        bonusball2.SetActive(false); // 2 мяч исчезает
        bonusball2active = false;
        Textprobelzapusk.SetActive(false); // исчезает текст "нажмите пробел для запуска"
        Debug.Log("после задержки");
    }

    private IEnumerator bonuswidthplusfunc() //бонус расширение площадки
    {
        // перед началом ожидания
        // бонус работает
        raketka.gameObject.transform.localScale += new Vector3(1, 0, 0); //увеличиваем ракетку в размерах
        lenght = 3f; // для того, чтобы мяч попал в широкую часть в Update
        bonuswidthplusactive = true;  //для сигнала в скрипт ракетки 
        yield return new WaitForSeconds(5f);
        // после ожидания
        lenght = 2f; //возвращаем
        bonuswidthplusactive = false;        //для сигнала в скрипт ракетки
        raketka.gameObject.transform.localScale += new Vector3(-1, 0, 0); // бонус не работает
    }
    

}







