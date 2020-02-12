using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    #region Cached references

    private Rigidbody rb;
    private AudioSource audioSource;

    #endregion

    #region Local variables

    [SerializeField] float thrustForce = 500f;
    [SerializeField] float rotationForce = 10f;
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
            PlayAudio();
        }
        

    }

    private void Thrust()
    {
        
        _thrust = new Vector3(0f, Input.GetAxis("Fire1"), 0f);
        rb.AddRelativeForce(_thrust * thrustForce * Time.deltaTime);

    }

    // TODO create and move this function to AudioManager class
    private void PlayAudio()
    {
        if (_thrust == Vector3.zero)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
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
                _currentState = State.Transcending;
                break;
            case "Obstacle":
                _currentState = State.Dying;
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
    }
}
