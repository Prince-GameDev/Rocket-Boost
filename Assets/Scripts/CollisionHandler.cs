using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LevelDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;
    AudioSource audioSourse;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSourse = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning ||collisionDisabled ) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            case "Finish":
                Debug.Log("congrats you finished");
                LoadNextLevel();
                break;
            default:
                //  ReloadLevel();
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSourse.Stop();
        successParticle.Play();
        audioSourse.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLeve", LevelDelay);
    }

        void StartCrashSequence()
    {
        isTransitioning = true;
        audioSourse.Stop();
        crashParticle.Play();
        audioSourse.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", LevelDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
  