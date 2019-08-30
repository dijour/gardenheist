using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
 
    public int chosenCharacter = 0;
    public GameObject[] characters;
    public Transform[] spawnPositions;
    public GameObject carrot;

    public NetworkController controller;

    public string myLobbyScreen;
 
    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }
 
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);
 
        GameObject player;
        Transform startPos = spawnPositions[chosenCharacter];
        /*
        if (chosenCharacter == 0) {
            for (int i=0; i < 20; i++) {
                GameObject mycarrot = Instantiate(carrot, startPos.position, startPos.rotation);
                NetworkServer.Spawn(mycarrot);
            }
        }*/
 
        if(startPos != null)
        {
            player = Instantiate(characters[chosenCharacter], startPos.position,startPos.rotation)as GameObject;
        }
        else
        {
            player = Instantiate(characters[chosenCharacter], Vector3.zero, Quaternion.identity) as GameObject;
 
        }

        chosenCharacter = 1;
 
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
 
    }
 
    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = 1;
 
        ClientScene.AddPlayer(conn, 0, test);

        if (controller != null) {
            controller.OnConnect();
        }
    }
 
 
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public static CustomNetworkManager instance = null;
 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            StopHost();
            StopClient();
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        if (controller != null) {
            controller.OnDisconnect();
        }
    }

    public override void OnStopServer() {
        print("onstopserver");
        if (controller != null && controller.connecting) {
            controller.OnDisconnect();
        } else {
            SceneManager.LoadScene(myLobbyScreen);
        }
    }

    public override void OnStopClient() {
        print("onstopclient");
        if (controller != null && controller.connecting) {
            controller.OnDisconnect();
        } else {
            SceneManager.LoadScene(myLobbyScreen);
        }
    }
    
}
