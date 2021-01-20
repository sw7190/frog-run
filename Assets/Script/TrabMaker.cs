using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrabMaker : MonoBehaviour
{

    public GameObject[] traps;

    private float nowTime;

    private int size;

    public GameObject Character;

    CharacterControl characterControl;

    // Start is called before the first frame update
    void Start()
    {
        nowTime = Time.time;
        size = traps.Length;

        if (Character != null) characterControl = Character.GetComponent<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {

        float makeTime = Random.Range(1f, 3f);
        if (size > 0 && Time.time - nowTime > makeTime)
        {
            nowTime = Time.time;
            int randomIndex = Random.Range(0, size);
            GameObject targetObject = traps[randomIndex];
            GameObject temp = Instantiate(targetObject);
            if (characterControl != null)
            {
            TrapMove trapMove = temp.GetComponent<TrapMove>(); 
            trapMove.trapSpeed = Mathf.Floor(characterControl.currentTime / 5);
            }

            Vector3 tempPosition = temp.transform.localPosition;
            tempPosition.Set(10.94033f, tempPosition.y, tempPosition.x);
            temp.transform.localPosition = tempPosition;
        }
    }
}
