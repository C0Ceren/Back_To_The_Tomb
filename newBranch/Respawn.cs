using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField] float timeToWait = 1f;
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag != "Friendly" && other.gameObject.tag != "Finish") //Oyuncu, tagi "Friendly"  ve "Finish" olmayan bir objeye �arpt���nda sahne tekrar y�klenecek.
        {
            GetComponent<Thrust>().enabled = false;
            GetComponent<Rotation>().enabled = false; //Oyuncu nesneye �arpt���ndan thrust ve rotation hareketlerini durdurur.

            Invoke("ReloadTheScene", timeToWait); //Invoke metodu �al��tr�mak istedi�imiz metodun �al��mas�n� ertelememizi sa�lar.
                                          /*Burada "ReloadTheScene" metodunun �al��mas�n� 2f erteledik bu sayede respawn olmadan �nce bir s�re bekleyecek.
                                           �arp��ma efekleri eklemek istedi�imizde nundan faydalanaca��z.*/
        }
    }

    private void ReloadTheScene()
    {
        int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNumber);
    }

    
}
