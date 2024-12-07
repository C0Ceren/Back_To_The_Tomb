using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Start()
    {
        
    }
  
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed == true)
        {
            Application.Quit();
            Debug.Log("okay, you escaped.");
        }
    }
}
