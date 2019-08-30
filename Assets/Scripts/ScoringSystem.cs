using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Sound;
using Indicator;

public class ScoringSystem : NetworkBehaviour
{
	public static int score;
    public static int winningScore = 100;
    public static bool isWin;
    public GameObject textMeshP;
    //public GameObject textBox;
    //public IndicatorText indText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        textMeshP = GameObject.Find("NumRabbitsKilled");
        //indText = (IndicatorText) textBox.GetComponent(typeof(IndicatorText));
        //MusicManager.M.PlayGame1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int worth, GameObject obj){
        if (this.isServer) {
            score += worth;
            Rpc_SyncScore(score);
            Destroy(obj);
            if (score >= winningScore){
                print("Rabbits Win");
                isWin = true;
                CountRabbit cr = (CountRabbit) textMeshP.GetComponent<CountRabbit>();
                cr.rabbitWin();
                //indText.rabbitWin();
                //Rpc_SyncGameEnd();
                //Time.timeScale = 0;
            }
        }
    }

    [ClientRpc]
    public void Rpc_SyncScore(int score) {
        ScoringSystem.score = score;
    }
    [ClientRpc]
    public void Rpc_SyncGameEnd(){
        Time.timeScale = 0;
    }
}
