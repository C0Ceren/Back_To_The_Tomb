using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{    

    /*Oyuna eklemedim ama dursun belki ilerde iþe yarar açar bakarým.*/
    /*Bir sonraki sahneye geçme ve varolan sahneyi tekrar yükleme iþlemleri bu classta tutulacak. CollisionHandler sýnýfý, yeniden doðma ve bir sonraki levele geçme
     iþlemleri için metodu buradan çaðýracak.*/

    public static SceneManagement Instance { get; private set; } //Instance sayesinde bu sýnýfa her yerden eriþilebilir.
    
    int currentSceneIndex;
    Vector3 startPosition;
    GameObject player;

   private void Awake()
    {    
        //Singleton yapýsýný oluþtur
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Sahneler arasýnda bu nesneyi yok etme
        }

        else
        {
            Destroy(gameObject);
        }

    }   

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Þu anki sahnenin indexsinin bilgisini tutar.

        GameObject player = GameObject.FindGameObjectWithTag("Player"); //Oyuncunun baþlnagýç pozisyonunu al
        if(player != null)
        {
            startPosition = player.transform.position;
        }
    }

    public void reloadTheScene(float delay)
    {
        //Coroutine baþlatarak sahne yeniden yükleme iþleminin geciktir
        StartCoroutine(reloadTheSceneCoroutine(delay));
        
    }


    private System.Collections.IEnumerator reloadTheSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); //Gecikme süresi


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Sahne tekrar yüklenirken oyuncu baþlangýç pozisyona döner, thrust ve rotation tekrar aktif edilir.
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
        if (currentSceneIndex + 1 < SceneManager.loadedSceneCount) //Maksimum sahne sayýsýna ulaþýlmamýþsa bir sonraki sahneye ilerle
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        else //Eðer maksimum sahne sayýsýna ulaþýlmýþsa, yani bir sonraki sahne yoksa tekrar ilk sahneye döner.
        {
            SceneManager.LoadScene(2);
        }
    }


    /* Singleton Nedir?
     
       Singleton, bir sýnýfýn yalnýzca bir örneðinin (instance) oluþturulmasýný ve bu örneðe global 
    olarak eriþilmesini garanti eden bir tasarým desenidir. Oyun geliþtirme ve yazýlým dünyasýnda genellikle 
    paylaþýlan verileri, genel ayarlarý veya sistem yöneticilerini yönetmek için kullanýlýr.
     */


    /*Coroutine Nedir?
          Coroutine, Unity'de bir fonksiyonun bir süre bekleyip çalýþmaya devam etmesini saðlayan özel bir yapýdýr. Normal bir fonksiyon, çaðrýldýðýnda 
          iþlemlerini tamamlayana kadar çalýþýr ve kontrolü geri verir. Ancak bir coroutine, çalýþma akýþýný duraklatabilir ve belirli koþullar yerine
          getirildiðinde devam edebilir.Unity'de coroutineler, genellikle zaman gecikmeleri, çerçeve bazlý iþlemler 
          veya uzun süren iþlemleri parçalara bölmek için kullanýlýr.*/

    /*IEnumerator, C# dilinde bir dönüþ türüdür ve Unity'de özellikle Coroutine iþlemlerinde kullanýlýr. Bu tür, bir dizi öðe arasýnda gezinmek 
     için kullanýlýr ve adým adým iþlem yapmayý mümkün kýlar. Unity'de ise IEnumerator, iþlemleri duraklatýp devam ettirmenin
    (örneðin zaman gecikmeleri) temelini oluþturur.*/

}
