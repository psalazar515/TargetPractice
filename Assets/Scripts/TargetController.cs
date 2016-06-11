using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
    Animation targetAnimation;

    bool hit = false;
    bool initialized = false;

    float speed;

    Vector3 spawnPoint;
    Vector3 targetPoint;

    void Start()
    {
        targetAnimation = GetComponent<Animation>();
    }
	
	// Updates
	void Update()
    {
        if (initialized)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
	}

    //Gets hit by ball
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ball" && !hit)
        {
            targetAnimation.Play("TargetHit");
            GameController.Score();
            hit = true;
        }
    }

    //Public Methods
    public void InitializeTarget(Vector3 spawnPoint, Vector3 targetPoint, float speed)
    {
        this.spawnPoint = spawnPoint;
        this.targetPoint = targetPoint;
        this.speed = speed;
        initialized = true;
    }

    public void ChangeTargetDirection()
    {
        Vector3 temp = spawnPoint;
        spawnPoint = targetPoint;
        targetPoint = temp;
    }
}
