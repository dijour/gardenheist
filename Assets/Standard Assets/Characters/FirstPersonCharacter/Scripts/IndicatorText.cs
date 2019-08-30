using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;

namespace Indicator{
    public class IndicatorText : MonoBehaviour{
        public Text text;
        // Start is called before the first frame update

        private bool isWin = false;

        void Start()
        {
            StartCoroutine(ExecuteAfterTime(5));
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            text.text = "";
            // Code to execute after the delay
        }

        public void PickUp(string name){
            if (!isWin) {
                text.text = name + " picked up!";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
                SoundManager.S.PlayPickup();
            }
        }

        public void Drop(string name){
            if (!isWin) {
                text.text = name + " dropped!";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
                SoundManager.S.PlayDrop();
            }
        }

        public void Score(string name){
            if (!isWin) {
                text.text = name + " Scored!";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
                SoundManager.S.PlayRabbitScore();
            }
        }

        public void Caught(){
            if (!isWin) {
                text.text = "Caught by Farmer! Respawning in 5...";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
                SoundManager.S.PlayOof();
            }
        }

        public void BurrowHide(){
            if (!isWin) {
                text.text = "Hidden in burrow!";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
            }
        }

        public void BurrowReveal(){
            if (!isWin) {
                text.text = "Came out of burrow!";
                StopAllCoroutines();
                StartCoroutine(ExecuteAfterTime(3));
            }
        }
        public void rabbitWin(){
            isWin = true;
            text.text = "Rabbits Win!";
            StopAllCoroutines();
            StartCoroutine(ExecuteAfterTime(5));
            MusicManager.M.StopGame1();
            SoundManager.S.StopAmbientWind();
        }
        public void farmerWin(){
            isWin = true;
            text.text = "Farmer Win!";
            StopAllCoroutines();
            StartCoroutine(ExecuteAfterTime(5));
            MusicManager.M.StopGame1();
            SoundManager.S.StopAmbientWind();
        }
    }

}
