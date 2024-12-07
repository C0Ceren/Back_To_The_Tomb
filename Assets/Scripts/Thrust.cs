using UnityEngine;
using UnityEngine.InputSystem;

public class Thrust : MonoBehaviour
{
    [SerializeField] InputAction thrustInput; //Yukar� hareket i�in input tu�u se�ilir.
    [SerializeField] float thruststrength; //Y�kselik hassiyetinin bilgisini tutar
    [SerializeField] float maxHeight = 15f;

    Rigidbody rb;

    private void OnEnable()
    {
        thrustInput.Enable();
    }





    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }




 
    void FixedUpdate()
    {
        if (transform.position.y > maxHeight)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = maxHeight;
            transform.position = newPosition;

            // Yüksekliği arttığında dikey hareketi durdur
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
        else
        {
            ProccessThrust();
        }
    }





    void ProccessThrust()
    {
        if (thrustInput.IsPressed()) //Belirlenen tu�a bas�ld���nda karakter yukar� hareket eder.
        {
            rb.AddRelativeForce(Vector3.up * thruststrength * Time.fixedDeltaTime); //Time.fixedDeltaTime, Time.deltaTime ile ayn� i�e yarar. FixedUpdate metodu i�inde bunu kullan�yoruz.
        }
    }



}
