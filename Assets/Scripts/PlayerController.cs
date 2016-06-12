using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{    
    //Public Vars
    public float mouseSensitivity = 3f;
    public float mouseRangeX = 45f;
    public float mouseRangeY = 30f;
    public float throwSpeed = 50f;
    public float throwDelay = 0.5f;

    public GameObject ball_prefab;

    //Local Vars
    float rotMouseX = 0;
    float rotMouseY = 0;
    float throwDelayRemaining;

    GameObject ball;
    
	//Initialization
	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        throwDelayRemaining = throwDelay;
	}
	
    //Updates
	void Update()
    {
        Aim();
        throwDelayRemaining -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && throwDelayRemaining < 0)
        {
            Fire();
            throwDelayRemaining = throwDelay;
        }
	}

    //Helper Functions
    void Aim()
    {
        rotMouseX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotMouseY += Input.GetAxis("Mouse X") * mouseSensitivity;

        rotMouseX = Mathf.Clamp(rotMouseX, -mouseRangeY, mouseRangeY);
        rotMouseY = Mathf.Clamp(rotMouseY, -mouseRangeX, mouseRangeX);

        Camera.main.transform.localRotation = Quaternion.Euler(rotMouseX, rotMouseY, 0);
    }

    void Fire()
    {
        ball = (GameObject)Instantiate(ball_prefab, Camera.main.transform.position, Camera.main.transform.rotation);
        ball.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwSpeed, ForceMode.Impulse);
    }
}
