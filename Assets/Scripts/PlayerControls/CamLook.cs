using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamLook : MonoBehaviour
{
    //variables
    public Transform orientation;
    public Transform player;
    public Transform playerPhy;
    public Rigidbody rb;

    private PlayerMovement playerMovement;
    private CinemachineFreeLook freeLook; 

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InitValues();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * vInput + orientation.right * hInput;

        if (inputDir != Vector3.zero)
        {
            playerPhy.forward = Vector3.Slerp(playerPhy.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

    }

    /// <summary>
    /// Load in values from player movement
    /// </summary>
    void InitValues()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        player = playerMovement.gameObject.transform;
        rb = player.gameObject.GetComponent<Rigidbody>();
        orientation = playerMovement.orientation;
        playerPhy = playerMovement.playerPhy;

        freeLook.Follow = player;
        freeLook.LookAt = player;
    }
}
