using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public float ballTTL = 3f;
	
	// Updates
	void Update()
    {
        ballTTL -= Time.deltaTime;
        if (ballTTL < 0)
        {
            Object.Destroy(this.gameObject);
        }
	}
}
