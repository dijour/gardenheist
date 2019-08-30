using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Events;
using Sound;

public class CountRabbit : NetworkBehaviour
{
    // Start is called before the first frame update
    public static int NumKilled = 0;
    public static int winningCount = 20;
    public static bool isWin;
    //public GameObject SSystem;

    public UnityEvent onFinish;
 
     void Awake() {
         if (onFinish == null)
             onFinish = new UnityEvent();
     }

    void Start()
    {
        //SSystem = GameObject.Find("ScoringSystem");
    }

    // Update is called once per frame
    void Update()
    {
        //ScoringSystem ss = (ScoringSystem) SSystem.GetComponent<ScoringSystem>();
    }
    public void KillRabbit(){
        print("CALLED ");
        if (this.isServer) {
            NumKilled+=1;
            TextMeshPro textmeshPro = this.gameObject.GetComponent<TextMeshPro>();
            textmeshPro.SetText("Rabbits caught: {0}",NumKilled);
            Rpc_Sync(NumKilled);

            SoundManager.S.PlayFarmerScore();

            if (NumKilled >= winningCount){
                print("Farmers win!");
                isWin = true;
                textmeshPro.SetText("Farmer Win!");
                StartCoroutine(ExecuteAfterTime(5));
            }
        }
    }
    public void rabbitWin(){
        TextMeshPro textmeshPro = this.gameObject.GetComponent<TextMeshPro>();
        textmeshPro.SetText("Rabbits Win!");
        StartCoroutine(ExecuteAfterTime(5));
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        onFinish.Invoke();
    }


    [ClientRpc]
    public void Rpc_Sync(int num) {
        CountRabbit.NumKilled = num;
        this.gameObject.GetComponent<TextMeshPro>().SetText("Rabbits caught: {0}",NumKilled);
    }
}
