using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Vegetable : NetworkBehaviour
{
    public int score = 100;
    public HoldItems thrower = null;

    public string vegName;
}
