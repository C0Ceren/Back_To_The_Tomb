using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //�arp��ma sonucu ger�ekle�en yeniden do�ma, bir sonraki levele ge�me, ses oynatma ve efekt i�lemleri bu classta ger�ekle�ir.

    [SerializeField] AudioClip Scream;
    [SerializeField] AudioClip EvilLaugh;
    [SerializeField] float delay = 3f; //Gecikme s�resinin belirlenmesi
    [SerializeField] ParticleSystem StartingParticles;
    [SerializeField] ParticleSystem CollisionParticles;

    AudioSource audioSource;

    int currentSceneIndex;
    bool isControllable = true;
    bool isCollidable = true;
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //bulunulan sahnenin indeksinin bulunmas�
    }

    private void Update()
    {
       // RespondToDebugKey();
       Debug.Log("hey hey oldu muuu");
       
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (isControllable && isCollidable) //Bu if ko�ulunu ses efektlerinin iki kez �al��mamas� ve debugkeyleri kullanabilmek i�in olu�turduk.
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

                default: //Burada yeniden do�ma i�lemleri ger�ekle�ir

                    GetComponent<Thrust>().enabled = false;
                    GetComponent<Rotation>().enabled = false; //Nesnenin hareket kontrollerini kapat�yoruz.

                    Invoke("reloadTheScene", delay);
                    PlayTheAudio(Scream);
                    playThePartcileSystem(CollisionParticles);

                    break;
            }
        }
    }



    private void PlayTheAudio(AudioClip clip) //Parametre olarak verilen ses klibini �al��t�r�r.
    {
        isControllable = false; //Fonksiyon �al��t���nda iscontrollable de�eri false olaca�� i�in if ko�ulunu kar��lamaz ve kod blo�u �al��maz.

        audioSource.Stop(); 
        audioSource.PlayOneShot(clip);
    }




    private void reloadTheScene() //�u anki sahneyi tekrar y�kler.
    {
        SceneManager.LoadScene(currentSceneIndex);
    }




    private void LoadTheNextScene()
    {
        if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings) //Bir sonraki sahnenin indeksi oyundaki sahne say�s�na e�itse, yani bir sonraki sahne yoksa oyunun 1. sevyesine d�n.
        {
            SceneManager.LoadScene(2);
        }
        else //Bir sonraki sahne varsa ona git.
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    private void playThePartcileSystem(ParticleSystem particles) //Parametr olarak ald��� particle system� �al��t�r�r.
    {
        if (!particles.isPlaying) //Ayn� efekti iki kez oynatmaz
        {
            particles.Play();
        }
    }

    void RespondToDebugKey() //Buraya oyun geli�tirme s�ras�nda kullanmak i�in baz� toollar ekliyoruz.
    {


        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadTheNextScene();  //L tu�una bas�ld���nda direkt bir sonraki levele atlar.

        }

        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; //c tu�una bas�ld���nda collision �zelli�ini kapat�r.
        }
    }
}
