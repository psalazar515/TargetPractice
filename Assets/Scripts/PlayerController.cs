using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{    
    //Public Vars
    public float mouseSensitivity = 3f;
    public float mouseRangeX = 45f;
    public float mouseRangeY = 30f;
    public float throwSpeed = 50f;

    public GameObject ball_prefab;

    //Local Vars
    float rotMouseX = 0;
    float rotMouseY = 0;

    GameObject ball;
    
	//Initialization
	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
    //Updates
	void Update()
    {
        Aim();

        if (Input.GetButtonDown("Fire1"))
            Fire();
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
