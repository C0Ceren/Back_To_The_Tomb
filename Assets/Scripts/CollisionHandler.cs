using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Çarpýþma sonucu gerçekleþen yeniden doðma, bir sonraki levele geçme, ses oynatma ve efekt iþlemleri bu classta gerçekleþir.

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

        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //bulunulan sahnenin indeksinin bulunmasý
    }

    private void Update()
    {
       // RespondToDebugKey();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (isControllable && isCollidable) //Bu if koþulunu ses efektlerinin iki kez çalýþmamasý ve debugkeyleri kullanabilmek için oluþturduk.
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

                default: //Burada yeniden doðma iþlemleri gerçekleþir

                    GetComponent<Thrust>().enabled = false;
                    GetComponent<Rotation>().enabled = false; //Nesnenin hareket kontrollerini kapatýyoruz.

                    Invoke("reloadTheScene", delay);
                    PlayTheAudio(Scream);
                    playThePartcileSystem(CollisionParticles);

                    break;
            }
        }
    }



    private void PlayTheAudio(AudioClip clip) //Parametre olarak verilen ses klibini çalýþtýrýr.
    {
        isControllable = false; //Fonksiyon çalýþtýðýnda iscontrollable deðeri false olacaðý için if koþulunu karþýlamaz ve kod bloðu çalýþmaz.

        audioSource.Stop(); 
        audioSource.PlayOneShot(clip);
    }




    private void reloadTheScene() //Þu anki sahneyi tekrar yükler.
    {
        SceneManager.LoadScene(currentSceneIndex);
    }




    private void LoadTheNextScene()
    {
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings) //Bir sonraki sahnenin indeksi oyundaki sahne sayýsýna eþitse, yani bir sonraki sahne yoksa oyunun 1. sevyesine dön.
        {
            SceneManager.LoadScene(2);
        }
        else //Bir sonraki sahne varsa ona git.
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    private void playThePartcileSystem(ParticleSystem particles) //Parametr olarak aldýðý particle systemý çalýþtýrýr.
    {
        if (!particles.isPlaying) //Ayný efekti iki kez oynatmaz
        {
            particles.Play();
        }
    }

    void RespondToDebugKey() //Buraya oyun geliþtirme sýrasýnda kullanmak için bazý toollar ekliyoruz.
    {


        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadTheNextScene();  //L tuþuna basýldýðýnda direkt bir sonraki levele atlar.

        }

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; //c tuþuna basýldýðýnda collision özelliðini kapatýr.
        }
    }
}
