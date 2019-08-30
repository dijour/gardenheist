using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class FakeRabbits : NetworkBehaviour
{
    public int numFakes;

    public GameObject fakeRabbitPrefab;

    public Text rabbitsLeftText;
    
    public Transform cameraTransform;

    public float launchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (this.isLocalPlayer) {
            rabbitsLeftText.text = "Fake Rabbits Left: " + numFakes;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer) {
            if (Input.GetKeyDown(KeyCode.C)) {
                if (numFakes > 0) {
                    Vector3 launch = cameraTransform.forward;
                    Cmd_CreateRabbit(launch.x, launch.y, launch.z);
                    numFakes--;
                    rabbitsLeftText.text = "Fake Rabbits Left: " + numFakes;
                }
            }
        }
    }

    [Command]
    void Cmd_CreateRabbit(float vx, float vy, float vz) {
        Vector3 pos = transform.position + new Vector3(vx,vy + 0.4f,vz) * 0.2f;

        GameObject fake = Instantiate(fakeRabbitPrefab, pos, Quaternion.identity);
        fake.GetComponent<Rigidbody>().velocity = new Vector3(vx, vy, vz) * launchSpeed;

        NetworkServer.Spawn(fake);
    }
}
