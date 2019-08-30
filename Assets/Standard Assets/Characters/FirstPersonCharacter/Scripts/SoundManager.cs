using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {

        public static SoundManager S;

        public GameObject yeet;
        public GameObject hide;
        public GameObject jump;
        public GameObject swoosh1;
        public GameObject dig;
        public GameObject oof;
        public GameObject rabbitScore;
        public GameObject pickup;
        public GameObject drop;
        public GameObject lose;
        public GameObject farmerScore;
        public GameObject rabbitStep;
        public GameObject ambientWind;
        public GameObject chime1;
        public GameObject chime2;
        public GameObject chime3;
        public GameObject leafRustle1;
        public GameObject leafRustle2;
        public GameObject leafRustle3;
        public GameObject leafRustle4;

        private AudioSource yeetSound;
        private AudioSource hideSound;
        private AudioSource jumpSound;
        private AudioSource swoosh1Sound;
        private AudioSource digSound;
        private AudioSource oofSound;
        private AudioSource rabbitScoreSound;
        private AudioSource pickupSound;
        private AudioSource dropSound;
        private AudioSource loseSound;
        private AudioSource farmerScoreSound;
        private AudioSource rabbitStepSound;
        private AudioSource ambientWindSound;
        private AudioSource chime1Sound;
        private AudioSource chime2Sound;
        private AudioSource chime3Sound;
        private AudioSource leafRustle1Sound;
        private AudioSource leafRustle2Sound;
        private AudioSource leafRustle3Sound;
        private AudioSource leafRustle4Sound;




        // Use this for initialization
        void Start()
        {
            // assign the singleton
            S = this;

            yeetSound = yeet.GetComponent<AudioSource>();
            hideSound = hide.GetComponent<AudioSource>();
            jumpSound = jump.GetComponent<AudioSource>();
            swoosh1Sound = swoosh1.GetComponent<AudioSource>();
            digSound = dig.GetComponent<AudioSource>();
            oofSound = oof.GetComponent<AudioSource>();
            rabbitScoreSound = rabbitScore.GetComponent<AudioSource>();
            pickupSound = pickup.GetComponent<AudioSource>();
            dropSound = drop.GetComponent<AudioSource>();
            loseSound = lose.GetComponent<AudioSource>();
            farmerScoreSound = farmerScore.GetComponent<AudioSource>();
            rabbitStepSound = rabbitStep.GetComponent<AudioSource>();
            ambientWindSound = ambientWind.GetComponent<AudioSource>();
            chime1Sound = chime1.GetComponent<AudioSource>();
            chime2Sound = chime2.GetComponent<AudioSource>();
            chime3Sound = chime3.GetComponent<AudioSource>();
            leafRustle1Sound = leafRustle1.GetComponent<AudioSource>();
            leafRustle2Sound = leafRustle2.GetComponent<AudioSource>();
            leafRustle3Sound = leafRustle3.GetComponent<AudioSource>();
            leafRustle4Sound = leafRustle4.GetComponent<AudioSource>();
        }

        //list of commands
        /*
        SoundManager.S.PlayYeet();
        SoundManager.S.PlayHide();   
        SoundManager.S.PlayJump(); 
        SoundManager.S.PlaySwoosh();
        SoundManager.S.PlayDig(); 
        SoundManager.S.PlayOof();
        SoundManager.S.PlayPickup();
        SoundManager.S.PlayDrop();
        SoundManager.S.PlayLose();
        SoundManager.S.PlayFarmerScore();
        SoundManager.S.PlayRabbitStep();
        SoundManager.S.PlayAmbientWind();
        SoundManager.S.PauseAmbientWind();
        SoundManager.S.StopAmbientWind();
        SoundManager.S.PlayChime1();
        SoundManager.S.PlayChime2();
        SoundManager.S.PlayChime3();
        SoundManager.S.PlayLeafRustle1();
        SoundManager.S.PlayLeafRustle2();
        SoundManager.S.PlayLeafRustle3();
        SoundManager.S.PlayLeafRustle4();
        */

        public void PlayYeet()
        {
            yeetSound.Play();
        }

        public void PlayHide()
        {
            hideSound.Play();
        }

        public void PlayJump()
        {
            jumpSound.Play();
        }

        public void PlaySwoosh()
        {
            swoosh1Sound.Play();
        }

        public void PlayDig()
        {
            digSound.Play();
        }

        public void PlayOof()
        {
            oofSound.Play();
        }

        public void PlayRabbitScore()
        {
            rabbitScoreSound.Play();
        }

        public void PlayPickup()
        {
            pickupSound.Play();
        }

        public void PlayDrop()
        {
            dropSound.Play();
        }

        public void PlayLose()
        {
            loseSound.Play();
        }

        public void PlayFarmerScore()
        {
            farmerScoreSound.Play();
        }

        public void PlayRabbitStep()
        {
            rabbitStepSound.Play();
        }

        public void PlayAmbientWind()
        {
            ambientWindSound.Play();
        }

        public void StopAmbientWind()
        {
            ambientWindSound.Stop();
        }

        public void PlayChime1()
        {
            chime1Sound.Play();
        }

        public void PlayChime2()
        {
            chime2Sound.Play();
        }

        public void PlayChime3()
        {
            chime3Sound.Play();
        }

        public void PlayLeafRustle1()
        {
            leafRustle1Sound.Play();
        }

        public void PlayLeafRustle2()
        {
            leafRustle2Sound.Play();
        }

        public void PlayLeafRustle3()
        {
            leafRustle3Sound.Play();
        }

        public void PlayLeafRustle4()
        {
            leafRustle4Sound.Play();
        }
    }
}