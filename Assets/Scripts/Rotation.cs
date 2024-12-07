using UnityEngine;
using UnityEngine.InputSystem; //BAK BUNU UNUTMA    

public class Rotation : MonoBehaviour
{
    [SerializeField] InputAction rotationInput; //Input al�nacak tu�lar�n bilgisini tutan
    [SerializeField] float rotationSensitivity; //D�nme hareketinin hassasiyetinin belirlenmesini sa�lar

    Rigidbody rb;

    private void OnEnable()
    {
        rotationInput.Enable();
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    
    void FixedUpdate()
    {
        float InputValue = rotationInput.ReadValue<float>(); //Y�n� belirler

        rb.freezeRotation = true; //Y�n�m�zde sapma olmamas� i�in rigidbody'nin d�nmesini engelliyoruz.

        if(InputValue > 0)
        {
            ProccessRotation(rotationSensitivity); //Input de�eri s�f�rdan b�y�kse sa�a d�ner.
        }

        else if (InputValue < 0)
        {
            ProccessRotation(-rotationSensitivity); //Input de�eri s�f�rdan k���kse sola d�ner.
        }

        rb.freezeRotation = false; //D�nme i�lemi bittikten sonra rigidbody'nin d�nme �zelli�i tekrar aktif edilir.

    }



    void ProccessRotation(float sensitivity)
    { 
        transform.Rotate(Vector3.right * sensitivity * Time.fixedDeltaTime);                             
    }
}
