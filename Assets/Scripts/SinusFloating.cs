using UnityEngine;

public class SinusFloating : MonoBehaviour
{
    [Header("Yüzme Ayarları")]
    [Tooltip("Objenin ne kadar yukarı-aşağı gideceği (Genlik).")]
    [SerializeField] private float amplitude = 0.5f;

    [Tooltip("Objenin ne kadar hızlı yüzeceği (Saniyedeki döngü hızı/Frekans).")]
    [SerializeField] private float frequency = 1f;

    [Header("Seçenekler")]
    [Tooltip("Eğer obje bir parent'ın içindeyse Local Position kullanmak daha güvenlidir.")]
    [SerializeField] private bool useLocalPosition = true;

    // Orijinal pozisyonu saklamak için
    private Vector3 startPosition;

    void Start()
    {
        // Oyun başladığında objenin durduğu yeri kaydet
        if (useLocalPosition)
            startPosition = transform.localPosition;
        else
            startPosition = transform.position;
    }

    void Update()
    {
        /* * Matematiksel Mantık:
         * Sin(x) fonksiyonu -1 ile 1 arasında pürüzsüz bir değer döner.
         * Time.time sürekli arttığı için x ekseni olarak kullanılır.
         * * yeniPos = orijinalPos + Sin(zaman * hız) * mesafe
         */

        // Sinüs değerini hesapla
        float sineCalculation = Mathf.Sin(Time.time * frequency) * amplitude;

        // Yeni pozisyon vektörünü oluştur (Sadece Y'yi değiştir)
        Vector3 newPosition = startPosition;
        newPosition.y += sineCalculation; 

        // Objeye uygula
        if (useLocalPosition)
            transform.localPosition = newPosition;
        else
            transform.position = newPosition;
    }
}