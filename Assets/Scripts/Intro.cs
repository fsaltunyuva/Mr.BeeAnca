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
        "Ancak insanlarin zannettigi gibi en iyi rotayi veren bir <shear>algoritmamiz</shear> yok, hicbir zaman da olmadi.",
        "Bu yuzden, her rota istegi geldiginde, rota olusturup kullanicilara yollayacaksin.",
        "Senin gibi bir <sketchy>ucuz isci</sketchy> varken, kimin gelismis algoritmalara ihtiyaci var ki?",
        "Ancak unutma, kazandigimiz para senin <color=red>rotayi olusturma suren ve rotanin uzunluguna</color> bagli olacak, kafana gore rota olusturamazsin.",
        "Bazi kullanicilar rotalarina duraklar eklemek isteyebilir, bu yuzden onlari da dusunmen lazim.",
        "Simdi senin kalin kafan icin biraz pratik yapma zamani."
    };
    
    void Start()
    {
        StartCoroutine(WaitSomeTimeAndDisplayNextSentence());
    }
    
    IEnumerator WaitSomeTimeAndDisplayNextSentence()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            dialogueText.text = sentences[i];
            
            if (i == 5)
                yield return new WaitForSeconds(6f);
            else
                yield return new WaitForSeconds(4.2f);
        }
        SceneManager.LoadScene(1);
    }
}
