using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour
{
    Animation targetAnimation;

    AudioSource audioSource;
    public AudioClip target_hit;
    float audioVolume;

    bool hit = false;
    bool initialized = false;

    int scoreMultiplier;
    float speed;
    float deathDestroyDelay = 1f;

    Vector3 spawnPoint;
    Vector3 targetPoint;

    void Start()
    {
        targetAnimation = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Updates
	void Update()
    {
        if (initialized)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f || deathDestroyDelay < 0)
            {
                Destroy(gameObject);
            }

            if (hit)
                deathDestroyDelay -= Time.deltaTime;
        }
	}

    //Gets hit by ball
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "ball" && !hit)
        {
            targetAnimation.Play("TargetHit");
            audioSource.PlayOneShot(target_hit, audioVolume);
            GameController.Score(scoreMultiplier);
            hit = true;
        }
    }

    //Public Methods
    public void InitializeTarget(Vector3 spawnPoint, Vector3 targetPoint, float speed, int scoreMultiplier)
    {
        this.spawnPoint = spawnPoint;
        this.targetPoint = targetPoint;
        this.speed = speed;
        this.scoreMultiplier = scoreMultiplier;

        audioVolume = 1 - (scoreMultiplier * 0.3f);

        initialized = true;
    }

    public void ChangeTargetDirection()
    {
        Vector3 temp = spawnPoint;
        spawnPoint = targetPoint;
        targetPoint = temp;
    }
}
