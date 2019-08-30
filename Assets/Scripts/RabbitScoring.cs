using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitScoring : MonoBehaviour
{
    public GameObject ScoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col){
    	if (col.gameObject.tag == "pickable")
        {
            Vegetable v = col.gameObject.GetComponent<Vegetable>();
            if (v) {
                print("Delivery");
                v.thrower.playScore(v.vegName);
                int score = v.score;
                ScoringSystem sb = (ScoringSystem) ScoreBoard.GetComponent(typeof(ScoringSystem));
                sb.AddScore(score, col.gameObject);
            }
        }

    }
}
