using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    #region Cached references

    private Rigidbody rb;
    private AudioSource audioSource;

    #endregion

    #region Serialized fields

    [SerializeField] float thrustForce = 500f;
    [SerializeField] float rotationForce = 10f;

    [SerializeField] AudioClip _aud_Thrust;
    [SerializeField] AudioClip _aud_Success;
    [SerializeField] AudioClip _aud_Death;

    [SerializeField] ParticleSystem _vfx_Thrust;
    [SerializeField] ParticleSystem _vfx_Success;
    [SerializeField] ParticleSystem _vfx_Death;
    #endregion

    #region Local variables

    private Vector3 _rotation, _thrust;
    private float _transitionTimeScenes;
    private State _currentState;

    private enum State
    {
        Alive,
        Dying,
        Transcending
    }


    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        _transitionTimeScenes = 2f;
        _currentState = State.Alive;

        

    }

    void Update()
    {
        if (_currentState == State.Alive)
        {
            Thrust();
            Rotate();
            ApplyEffects();
        }
        

    }

    private void Thrust()
    {
        
        _thrust = new Vector3(0f, Input.GetAxis("Fire1"), 0f);
        rb.AddRelativeForce(_thrust * thrustForce * Time.deltaTime);
        
        

    }

    // TODO create and move this function to AudioManager class
    private void ApplyEffects()
    {
        if (_thrust == Vector3.zero)
        {

            _vfx_Thrust.Stop();

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                
            }
        }
        else
        {
            _vfx_Thrust.Play();

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(_aud_Thrust);
            }

        }

    }

    private void Rotate()
    {
        rb.freezeRotation = true;
        _rotation = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(_rotation * rotationForce * Time.deltaTime);
        rb.freezeRotation = false;
    }

    // TODO create a CollisionManager class
    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {

            case "Friendly":
                print("Friendly");
                break;
                
            case "Launchpad":
                print("Launchpad");
                break;

            case "Finish":
                Invoke("LoadNextScene", _transitionTimeScenes);
                audioSource.PlayOneShot(_aud_Success);
                _currentState = State.Transcending;
                _vfx_Success.Play();
                Debug.Log("Finish");
                break;
            case "Obstacle":
                _currentState = State.Dying;
                audioSource.PlayOneShot(_aud_Death);
                Debug.Log("Dead");
                _vfx_Death.Play();
                Invoke("RestartGame", _transitionTimeScenes);
                break;
        }
    }

    private void LoadNextScene()
    {
        //SceneManager.LoadScene(1);
        print("Passing level");
    }

    private void RestartGame()
    {
        print("Game Over");
        SceneManager.LoadScene(0);
    }
}
