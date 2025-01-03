using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] float timeToWait = 1f;
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag != "Friendly" && other.gameObject.tag != "Finish") //Oyuncu, tagi "Friendly"  ve "Finish" olmayan bir objeye çarptığında sahne tekrar yüklenecek.
        {
            GetComponent<Thrust>().enabled = false;
            GetComponent<Rotation>().enabled = false; //Oyuncu nesneye çarptığından thrust ve rotation hareketlerini durdurur.

            Invoke("ReloadTheScene", timeToWait); //Invoke metodu çalıştrımak istediğimiz metodun çalışmasını ertelememizi sağlar.
                                          /*Burada "ReloadTheScene" metodunun çalışmasını 2f erteledik bu sayede respawn olmadan önce bir süre bekleyecek.
                                           Çarpışma efekleri eklemek istediğimizde nundan faydalanacağız.*/
        }
    }

    private void ReloadTheScene()
    {
        int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNumber);
    }

    
}
