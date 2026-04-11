using UnityEngine;
using TMPro; // TextMeshPro için gerekli

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; // UI üzerindeki yazı objesi
    public float elapsedTime = 0f;
    private bool isTimerRunning = false;

    void Update()
    {
        if (isTimerRunning)
        {
            // Zamanı her karede geçen süre kadar artırıyoruz
            elapsedTime += Time.deltaTime;
            
            // UI'ı güncelle
            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        // Saniyeyi Dakika:Saniye formatına çeviriyoruz
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // String.Format ile 00:00 şeklinde yazdırıyoruz
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void ResetTimer()
    {
        isTimerRunning = false; // Sayacı durdurur
        elapsedTime = 0f;       // Zamanı sıfırlar
        UpdateTimerDisplay();   // Ekrandaki yazıyı 00:00 yapar
    }
}