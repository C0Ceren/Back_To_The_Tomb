using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{    

    /*Oyuna eklemedim ama dursun belki ilerde i�e yarar a�ar bakar�m.*/
    /*Bir sonraki sahneye ge�me ve varolan sahneyi tekrar y�kleme i�lemleri bu classta tutulacak. CollisionHandler s�n�f�, yeniden do�ma ve bir sonraki levele ge�me
     i�lemleri i�in metodu buradan �a��racak.*/

    public static SceneManagement Instance { get; private set; } //Instance sayesinde bu s�n�fa her yerden eri�ilebilir.
    
    int currentSceneIndex;
    Vector3 startPosition;
    GameObject player;

   private void Awake()
    {    
        //Singleton yap�s�n� olu�tur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Sahneler aras�nda bu nesneyi yok etme
        }

        else
        {
            Destroy(gameObject);
        }

    }   

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //�u anki sahnenin indexsinin bilgisini tutar.

        GameObject player = GameObject.FindGameObjectWithTag("Player"); //Oyuncunun ba�lnag�� pozisyonunu al
        if(player != null)
        {
            startPosition = player.transform.position;
        }
    }

    public void reloadTheScene(float delay)
    {
        //Coroutine ba�latarak sahne yeniden y�kleme i�leminin geciktir
        StartCoroutine(reloadTheSceneCoroutine(delay));
        
    }


    private System.Collections.IEnumerator reloadTheSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); //Gecikme s�resi


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Sahne tekrar y�klenirken oyuncu ba�lang�� pozisyona d�ner, thrust ve rotation tekrar aktif edilir.
        if (player != null)
        {
            player.transform.position = startPosition;
            player.GetComponent<Thrust>().enabled = true;
            player.GetComponent<Rotation>().enabled = true;
        }

        SceneManager.LoadScene(currentSceneIndex);

   
    }

    public void LoadTheNextScene()
    {  
        if (currentSceneIndex + 1 < SceneManager.loadedSceneCount) //Maksimum sahne say�s�na ula��lmam��sa bir sonraki sahneye ilerle
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        else //E�er maksimum sahne say�s�na ula��lm��sa, yani bir sonraki sahne yoksa tekrar ilk sahneye d�ner.
        {
            SceneManager.LoadScene(2);
        }
    }


    /* Singleton Nedir?
     
       Singleton, bir s�n�f�n yaln�zca bir �rne�inin (instance) olu�turulmas�n� ve bu �rne�e global 
    olarak eri�ilmesini garanti eden bir tasar�m desenidir. Oyun geli�tirme ve yaz�l�m d�nyas�nda genellikle 
    payla��lan verileri, genel ayarlar� veya sistem y�neticilerini y�netmek i�in kullan�l�r.
     */


    /*Coroutine Nedir?
          Coroutine, Unity'de bir fonksiyonun bir s�re bekleyip �al��maya devam etmesini sa�layan �zel bir yap�d�r. Normal bir fonksiyon, �a�r�ld���nda 
          i�lemlerini tamamlayana kadar �al���r ve kontrol� geri verir. Ancak bir coroutine, �al��ma ak���n� duraklatabilir ve belirli ko�ullar yerine
          getirildi�inde devam edebilir.Unity'de coroutineler, genellikle zaman gecikmeleri, �er�eve bazl� i�lemler 
          veya uzun s�ren i�lemleri par�alara b�lmek i�in kullan�l�r.*/

    /*IEnumerator, C# dilinde bir d�n�� t�r�d�r ve Unity'de �zellikle Coroutine i�lemlerinde kullan�l�r. Bu t�r, bir dizi ��e aras�nda gezinmek 
     i�in kullan�l�r ve ad�m ad�m i�lem yapmay� m�mk�n k�lar. Unity'de ise IEnumerator, i�lemleri duraklat�p devam ettirmenin
    (�rne�in zaman gecikmeleri) temelini olu�turur.*/

}
