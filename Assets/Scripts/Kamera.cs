using UnityEngine;
// yapayzeka ile yazıldı
public class KameraKontrol : MonoBehaviour
{ 
    [Header("Kamera Ayarları")]
    public float fareHassasiyeti = 100f; 
    
    [Header("Referanslar")]
    public Transform oyuncuGovdesi; 

    private float xRotasyonu = 0f; 

    void Start()
    {
        // Oyun başladığında fare imlecini ekranın ortasına kilitle ve gizle
        // Bu sayede oyun oynarken fare yanlışlıkla Unity penceresinin dışına çıkmaz
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Farenin X (Sağ-Sol) ve Y (Yukarı-Aşağı) hareketlerini al
        // Time.deltaTime ile çarparak FPS dalgalanmalarından etkilenmesini önlüyoruz
        float mouseX = Input.GetAxis("Mouse X") * fareHassasiyeti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * fareHassasiyeti * Time.deltaTime;

        // 2. Yukarı/Aşağı bakma işlemleri (Kamera)
        // Neden eksi (-) kullanıyoruz? Çünkü fareyi yukarı ittiğimizde açının azalması gerekir, aksi takdirde kontroller ters (Inverted) olur.
        xRotasyonu -= mouseY;
        
        // Boyun kırılmasını engelle! Kameranın -90 (tam tepe) ve 90 (tam zemin) dereceden fazla dönmesini engeller
        xRotasyonu = Mathf.Clamp(xRotasyonu, -90f, 90f);

        // Kameranın kendi rotasyonunu (Local Rotation) sadece X ekseninde (yukarı/aşağı) güncelleyelim
        transform.localRotation = Quaternion.Euler(xRotasyonu, 0f, 0f);

        // 3. Sağa/Sola bakma işlemleri (Karakter Gövdesi)
        // Farenin X hareketini alıp, karakterin Y ekseni (dik eksen) etrafında dönmesini sağlıyoruz
        oyuncuGovdesi.Rotate(Vector3.up * mouseX);
    }
}
