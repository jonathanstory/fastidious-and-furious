using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip audioClipOnDestroy;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Horse")
        {
            audioSource.PlayOneShot(audioClipOnDestroy);
            Destroy(gameObject);
        }
    }
}
