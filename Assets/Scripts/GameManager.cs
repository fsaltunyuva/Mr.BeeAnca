using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Request> requests;
    public Request activeRequest;
    public float totalMoney = 0f;
    
    [Header ("Script References")]
    [SerializeField] private Timer timer;
    [SerializeField] private Movement movement;
    
    [Header ("Scoring Constants")]
    public float timeConstant = 1f;
    public float distanceConstant = 0.1f;

    public TextMeshProUGUI calculationText;
    public TextMeshProUGUI calculationText2;
    public TextMeshProUGUI totalMoneyText;
    
    [Header("Request Dependent")]
    public TextMeshProUGUI currentRequestPriceText;
    public TextMeshProUGUI currentRequestDialogueText;
    public Image currentRequestPortrait; 
    public int waypointPassCounterForCurrentRequest = 0;

    public GameObject player;

    public int currentRequestCounter = 0;
    
    [Header("Trail Renderer")]
    public TrailRenderer outerTrail;
    public TrailRenderer innerTrail;
    
    public GameObject tutorialEndScreen;
    public GameObject anlasildiButton;
    
    void Start()
    {
        NewRequest();
    }
    
    public void NewRequest()
    {
        // int randomIndex = UnityEngine.Random.Range(0, requests.Count); // TODO: Consider randomization later
        activeRequest = requests[currentRequestCounter++];
        
        activeRequest.gameObject.transform.gameObject.SetActive(true);
        currentRequestPortrait.sprite = activeRequest.portrait;
        currentRequestPriceText.text = $"{activeRequest.requestPrice}$";
        currentRequestDialogueText.text = activeRequest.dialogue;
        player.transform.position = activeRequest.startPoint.position;
        movement.canMove = true;

        // TODO: Maybe some additional info can be added to these because they are empty at the moment when drawing for a request
        calculationText.text = "";
        calculationText2.text = "";
        
        timer.StartTimer();
        
        // Start trail when a new request is generated
        ClearTrail();
        StartTrail();
        
        SingletonMusic.Instance.PlaySFX("start_SFX");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movement.isOnEndPoint)
            {
                ProcessRecommendedWay();
            }
            else
            {
                calculationText.text = "<color=red>Henuz varis noktasina varmadin!</color>";
                SingletonMusic.Instance.PlaySFX("waypointNotPassed_SFX");
            }
        }
    }

    public void ActivateAnlasildiButton()
    {
        anlasildiButton.SetActive(true);
    }
    
    public void ProcessRecommendedWay()
    {
        if (waypointPassCounterForCurrentRequest != activeRequest.wayPoints.Count)
        {
            currentRequestDialogueText.text = "<color=red>Gecmen gereken duraklardan gecmedin!</color>";
            SingletonMusic.Instance.PlaySFX("waypointNotPassed_SFX");
            StartCoroutine(WaitSomeTimeResetDialogue(3f));
            return;
        }
        
        SingletonMusic.Instance.PlaySFX("arrival_SFX");
        movement.canMove = false;
        timer.StopTimer();
        
        float timeTaken = timer.elapsedTime;
        float distancePoint = movement.movementCounter;
        float moneyEarned = Mathf.Max(0, activeRequest.requestPrice - timeTaken * timeConstant - distancePoint * distanceConstant);
        AddMoney(moneyEarned);
        
        calculationText.text = $"<color=white>Rota Ucreti: {activeRequest.requestPrice:F1}</color>\n" +
                               $"<color=red>Zaman Cezasi: -{timeTaken * timeConstant:F1}</color>\n" +
                               $"<color=red>Mesafe Cezasi: -{distancePoint * distanceConstant:F1}</color>";
        calculationText2.text = $"<color=green>Kazanilan Para: {moneyEarned}</color>";
        
        // Deactivate current request
        activeRequest.gameObject.SetActive(false);
        timer.ResetTimer();
        movement.ResetMovementCounter();
        waypointPassCounterForCurrentRequest = 0;
        
        // Stop trail when request is completed
        StopTrail();

        if (currentRequestCounter == 4)
            StartCoroutine(EndTutorial());
        else
            StartCoroutine(WaitSomeTimeAndStartNewRequest(3f));

    }
    
    IEnumerator WaitSomeTimeAndStartNewRequest(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NewRequest();
    }
    
    IEnumerator WaitSomeTimeResetDialogue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentRequestDialogueText.text = activeRequest.dialogue;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
        tutorialEndScreen.SetActive(true);
    }
    
    public void AcceptEndTutorial()
    {
        Time.timeScale = 1f;
        tutorialEndScreen.SetActive(false);
        StartCoroutine(WaitSomeTimeAndStartNewRequest(1f));
    }
    
    public void AddMoney(float amount)
    {
        totalMoney += amount;
        totalMoneyText.text = $"{totalMoney}";
    }
    
    public void StartTrail()
    {
        outerTrail.emitting = true;
        innerTrail.emitting = true;
    }
    
    public void StopTrail()
    {
        outerTrail.emitting = false;
        innerTrail.emitting = false;
    }
    
    public void ClearTrail()
    {
        outerTrail.Clear();
        innerTrail.Clear();
    }
}
