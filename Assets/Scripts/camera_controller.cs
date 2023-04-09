using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame, the Late means that it will be 
    // called after all the other update fxns
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
