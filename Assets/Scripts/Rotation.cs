using UnityEngine;
using UnityEngine.InputSystem; //BAK BUNU UNUTMA    

public class Rotation : MonoBehaviour
{
    [SerializeField] InputAction rotationInput; //Input alýnacak tuþlarýn bilgisini tutan
    [SerializeField] float rotationSensitivity; //Dönme hareketinin hassasiyetinin belirlenmesini saðlar

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
        float InputValue = rotationInput.ReadValue<float>(); //Yönü belirler

        rb.freezeRotation = true; //Yönümüzde sapma olmamasý için rigidbody'nin dönmesini engelliyoruz.

        if(InputValue > 0)
        {
            ProccessRotation(rotationSensitivity); //Input deðeri sýfýrdan büyükse saða döner.
        }

        else if (InputValue < 0)
        {
            ProccessRotation(-rotationSensitivity); //Input deðeri sýfýrdan küçükse sola döner.
        }

        rb.freezeRotation = false; //Dönme iþlemi bittikten sonra rigidbody'nin dönme özelliði tekrar aktif edilir.

    }



    void ProccessRotation(float sensitivity)
    { 
        transform.Rotate(Vector3.right * sensitivity * Time.fixedDeltaTime);                             
    }
}
