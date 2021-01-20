using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    float DestroyX = -15.27567f;
    public float trapSpeed = 0f;
    public float defaultTrapSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.left * Time.deltaTime * (defaultTrapSpeed + (trapSpeed * 2)));
        if (Time.timeScale == 0 || transform.localPosition.x < DestroyX) Destroy(gameObject);
    }
}
