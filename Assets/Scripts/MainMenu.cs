using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

   public void PlayButtonPressed()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void HowToPlayButtonPressed()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
   


}
