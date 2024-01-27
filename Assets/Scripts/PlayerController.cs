using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform RightLimit;
    [SerializeField] private Transform LeftLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var X = Input.GetAxis("Horizontal");

        if (X >= 0)
        {
            if (RightLimit.position.x > playerCamera.gameObject.transform.position.x)
                playerCamera.gameObject.transform.position += new Vector3(moveSpeed * X * Time.deltaTime, 0, 0);
        }
        else if (X < 0)
        {
            if (LeftLimit.position.x < playerCamera.gameObject.transform.position.x)
            playerCamera.gameObject.transform.position += new Vector3(moveSpeed * X * Time.deltaTime, 0, 0);
        }
    }
}
