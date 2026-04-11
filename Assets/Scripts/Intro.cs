using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    private List<string> sentences = new List<string>
    {
        "Selam Bee Anca, Noodle Maps'teki ilk is gunune hosgeldin.",
        "Uygulamanin adindan da anlayacagin uzere, su anda harita uygulamasinin icindeyiz.",
        "Ancak insanlarin zannettigi gibi en iyi rotayi veren bir algoritmamiz yok, hicbir zaman da olmadi.",
        "Bu yuzden, her rota istegini sen olusturup, kullanicilara yollayacaksin.",
        "Senin gibi bir ucuz isci varken, kimin gelismis algoritmalara ihtiyaci var ki?"
    };
    
    void Start()
    {
        StartCoroutine(WaitSomeTimeAndDisplayNextSentence());
    }
    
    IEnumerator WaitSomeTimeAndDisplayNextSentence()
    {
        foreach (var sentence in sentences)
        {
            dialogueText.text = sentence;
            yield return new WaitForSeconds(4f);
        }
        SceneManager.LoadScene(1);
    }
}
