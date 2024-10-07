using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public GameObject Player;

    public float offsetX = 0.0f;
    public float CameraSpeed = 10.0f;
    Vector3 TargetPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.transform.position.x > -8.9 && Player.transform.position.x < 8.9)
        {
            TargetPos = new Vector3(Player.transform.position.x + offsetX, 0, -10);

            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
        }
    }
}
