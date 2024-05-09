using Mirror;
using UnityEngine;

public class GameReferee : NetworkBehaviour
{
    void Start()
    {
        Debug.Log($"{name} | (Debug.Log) Game Server is running");
    }
}
