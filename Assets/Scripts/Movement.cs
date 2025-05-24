using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float thrustRotation = 3f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem rightEngineParticle;
    [SerializeField] ParticleSystem leftEngineParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotating();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);//* mainThrust * Time.deltaTime
            Debug.Log("Pressed Space------Thrusting");
            //audioSource.Play();

           if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            mainEngineParticle.Play();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();

        }
    }
    void ProcessRotating()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating Left");
            ApplyRotation(thrustRotation);
            leftEngineParticle.Play();
            

            //   transform.Rotate(Vector3.forward * thrustRotation *Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating Right");
            ApplyRotation(-thrustRotation);
            rightEngineParticle.Play();

            //   transform.Rotate(Vector3.forward * thrustRotation *Time.deltaTime);
        }
        else
        {
            leftEngineParticle.Stop();
            rightEngineParticle.Stop();
        }
    }
     void ApplyRotation(float rotatingThisFrame)
    {
        rb.freezeRotation = true;//freezing rotation so we manually rotating
        transform.Rotate(Vector3.forward * rotatingThisFrame * Time.deltaTime);
        rb.freezeRotation = false;//unfreezing rotation so the physics system can take over
    }
}
