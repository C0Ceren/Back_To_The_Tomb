using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] float timeToWait = 1f;
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag != "Friendly" && other.gameObject.tag != "Finish") //Oyuncu, tagi "Friendly"  ve "Finish" olmayan bir objeye çarptýðýnda sahne tekrar yüklenecek.
        {
            GetComponent<Thrust>().enabled = false;
            GetComponent<Rotation>().enabled = false; //Oyuncu nesneye çarptýðýndan thrust ve rotation hareketlerini durdurur.

            Invoke("ReloadTheScene", timeToWait); //Invoke metodu çalýþtrýmak istediðimiz metodun çalýþmasýný ertelememizi saðlar.
                                          /*Burada "ReloadTheScene" metodunun çalýþmasýný 2f erteledik bu sayede respawn olmadan önce bir süre bekleyecek.
                                           Çarpýþma efekleri eklemek istediðimizde nundan faydalanacaðýz.*/
        }
    }

    private void ReloadTheScene()
    {
        int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNumber);
    }

    
}
