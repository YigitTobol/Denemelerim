using UnityEngine;
// yapayzeka ile yazıldı
public class OyuncuHareketi : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hareketHizi = 5f; // Karakterin yürüme hızı

    void Update()
    {
        // 1. Oyuncunun klavye girdilerini al
        // Horizontal: A ve D tuşları (veya Sol/Sağ oklar) -> X ekseninde -1 ile 1 arası değer verir
        float x = Input.GetAxis("Horizontal"); 
        
        // Vertical: W ve S tuşları (veya Yukarı/Aşağı oklar) -> Z ekseninde -1 ile 1 arası değer verir
        float z = Input.GetAxis("Vertical");

        // 2. Bu girdileri bir 3D Vektöre dönüştür (Y ekseni 0 çünkü zıplama yok)
        Vector3 hareketYonu = new Vector3(x, 0f, z);

        // 3. Karakteri hareket ettir
        // Time.deltaTime: Oyunun saniyedeki kare hızından (FPS) bağımsız, her bilgisayarda aynı hızda yürümesini sağlar
        transform.Translate(hareketYonu * hareketHizi * Time.deltaTime);
    }
}