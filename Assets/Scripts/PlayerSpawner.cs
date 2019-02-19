using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject thePlayer = Instantiate(player, new Vector3(spawner.transform.position.x, spawner.transform.position.y, 
                                                    spawner.transform.position.z), Quaternion.identity);
    }
}
