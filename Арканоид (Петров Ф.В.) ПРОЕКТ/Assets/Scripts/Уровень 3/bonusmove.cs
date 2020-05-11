//движения бонуса вниз

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusmove : MonoBehaviour
{

    public GameObject ball; //мяч
    public GameObject raketka; // объект ракетка
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime); // падение бонуса

    }
}
