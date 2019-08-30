using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

public class WindChime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "pushesChime")
        {
            int randomIndex = Random.Range(1, 4);
            switch (randomIndex)
            {
                case 3:
                    SoundManager.S.PlayChime3();
                    break;
                case 2:
                    SoundManager.S.PlayChime2();
                    break;
                default:
                    SoundManager.S.PlayChime1();
                    break;
            }
        }
    }
}
