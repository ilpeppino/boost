using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource audio;

    void Awake()
    {

        audio = GetComponent<AudioSource>();

    }

    private void Start()
    {
        if (this != null)
        {
            Destroy(this);
            return;
        }

        audio.Play();
    }

}
