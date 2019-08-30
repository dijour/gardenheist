using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    public Text ipText;

    public Animator fadeBack;

    public CanvasGroup mainGroup;

    public void JoinFarmer() {
        CustomNetworkManager.instance.controller = this;
        CustomNetworkManager.instance.networkAddress = "localhost";
        CustomNetworkManager.instance.chosenCharacter = 0;
        //CustomNetworkManager.instance.StopHost();
        //CustomNetworkManager.instance.StopClient();
        connecting = true;
        mainGroup.interactable = false;
        CustomNetworkManager.instance.StartHost();
        StartCoroutine(Timeout(10));
    }

    public void FarmerEndGame() {
        CustomNetworkManager.instance.StopHost();
    }

    public bool connecting;

    public void JoinRabbit() {
        print("Rabbit");
        CustomNetworkManager.instance.controller = this;
        string text = ipText.text == "" ? "128.237.235.125" : ipText.text;
        if (text == "1") {
            CustomNetworkManager.instance.networkAddress = "localhost";
            CustomNetworkManager.instance.chosenCharacter = 1;
            //CustomNetworkManager.instance.StopHost();
            //CustomNetworkManager.instance.StopClient();
            mainGroup.interactable = false;
            connecting = true;
            mainGroup.interactable = false;
            CustomNetworkManager.instance.StartHost();
            fadeBack.SetTrigger("FadeIn");
            StartCoroutine(Timeout(10));
        } else {
            CustomNetworkManager.instance.networkAddress = text;
            //CustomNetworkManager.instance.StopHost();
            //CustomNetworkManager.instance.StopClient();
            connecting = true;
            mainGroup.interactable = false;
            NetworkClient client = CustomNetworkManager.instance.StartClient();
            /*if (client == null || !client.isConnected) {
                fadeBack.SetTrigger("FadeIn");
            }*/
            StartCoroutine(Timeout(10));
        }
    }

    public IEnumerator Timeout(float time) {
        yield return new WaitForSeconds(time);
        if (connecting) {
            CustomNetworkManager.instance.StopHost();
        }
    }

    public void OnDisconnect() {
        if (connecting) {
            if (fadeBack) {
                fadeBack.SetTrigger("FadeIn");
            }
            connecting = false;
            if (mainGroup) {
                mainGroup.interactable = true;
            }
        }
    }

    public void OnConnect() {
        connecting = false;
        if (mainGroup) {
            mainGroup.interactable = true;
        }
    }
}
