using UnityEngine;
using System.Collections;

public class GregController : MonoBehaviour
{
    AudioSource audioSource;
    Animation gregAnimation;
    Animator anim;
    public AudioClip ewh;
    public AudioClip pew;

    float volume = 0.2f;

    void Start()
    {
        gregAnimation = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "ball")
        {
            if (Random.value < 0.5f)
            {
                PewAnimation();
            }
            else
            {
                EwhAnimation();
            }
        }
    }

    //Audio/Animation functions

    void EwhAnimation()
    {
        anim.SetTrigger("greg_ewh");
        audioSource.PlayOneShot(ewh, volume);
    }

    void PewAnimation()
    {
        anim.SetTrigger("greg_ewh");
        audioSource.PlayOneShot(pew, volume);
    }
}
