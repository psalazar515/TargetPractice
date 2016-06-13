using UnityEngine;
using System.Collections;

public class GregController : MonoBehaviour
{
    AudioSource audioSource;
    Animation gregAnimation;
    Animator anim;
    public AudioClip ewh;

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
            EwhAnimation();
        }
    }

    //Audio/Animation functions

    void EwhAnimation()
    {
        anim.SetTrigger("greg_ewh");
        audioSource.PlayOneShot(ewh, volume);
    }
}
