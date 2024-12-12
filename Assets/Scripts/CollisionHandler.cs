using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Çarpışma sonucu gerçekleşen yeniden doğma, bir sonraki levele geçme, ses oynatma ve efekt işlemleri bu classta gerçekleşir.

    [SerializeField] AudioClip Scream;
    [SerializeField] AudioClip EvilLaugh;
    [SerializeField] float delay = 3f; //Gecikme süresinin belirlenmesi
    [SerializeField] ParticleSystem StartingParticles;
    [SerializeField] ParticleSystem CollisionParticles;

    AudioSource audioSource;

    int currentSceneIndex;
    bool isControllable = true;
    bool isCollidable = true;
    void Start()
    {
        Debug.Log("Mustafadan Merhabalar");
        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //bulunulan sahnenin indeksinin bulunması
    }

    private void Update()
    {
       // RespondToDebugKey();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (isControllable && isCollidable) //Bu if koşulunu ses efektlerinin iki kez çalışmaması ve debugkeyleri kullanabilmek için oluşturduk.
        {

            switch (other.gameObject.tag)
            {
                case "Start":

                    playThePartcileSystem(StartingParticles);

                    break;

                case "Finish":

                    Invoke("LoadTheNextScene", delay);
                    PlayTheAudio(EvilLaugh);

                    break;

                case "EndOfScene":
                    Debug.Log("You need to finish this level.");
                    break;

                default: //Burada yeniden doğma işlemleri gerçekleşir

                    GetComponent<Thrust>().enabled = false;
                    GetComponent<Rotation>().enabled = false; //Nesnenin hareket kontrollerini kapatıyoruz.

                    Invoke("reloadTheScene", delay);
                    PlayTheAudio(Scream);
                    playThePartcileSystem(CollisionParticles);

                    break;
            }
        }
    }



    private void PlayTheAudio(AudioClip clip) //Parametre olarak verilen ses klibini çalıştırır.
    {
        isControllable = false; //Fonksiyon çalıştığında iscontrollable değeri false olacağı için if koşulunu karşılamaz ve kod bloğu çalışmaz.

        audioSource.Stop(); 
        audioSource.PlayOneShot(clip);
    }




    private void reloadTheScene() //Şu anki sahneyi tekrar yükler.
    {
        SceneManager.LoadScene(currentSceneIndex);
    }




    private void LoadTheNextScene()
    {
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings) //Bir sonraki sahnenin indeksi oyundaki sahne sayısına eşitse, yani bir sonraki sahne yoksa oyunun 1. sevyesine dön.
        {
            SceneManager.LoadScene(2);
        }
        else //Bir sonraki sahne varsa ona git.
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    private void playThePartcileSystem(ParticleSystem particles) //Parametr olarak aldığı particle systemı çalıştırır.
    {
        if (!particles.isPlaying) //Aynı efekti iki kez oynatmaz
        {
            particles.Play();
        }
    }

    void RespondToDebugKey() //Buraya oyun geliştirme sırasında kullanmak için bazı toollar ekliyoruz.
    {


        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadTheNextScene();  //L tuşuna basıldığında direkt bir sonraki levele atlar.

        }

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; //c tuşuna basıldığında collision özelliğini kapatır.
        }
    }
}
