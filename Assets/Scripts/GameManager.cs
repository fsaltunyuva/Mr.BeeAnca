using System;
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
    public float distanceConstant = 1f;

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
                calculationText.text = "<color=red>You haven't reached the destination yet!</color>";
            }
        }
    }
    
    public void ProcessRecommendedWay()
    {
        if (waypointPassCounterForCurrentRequest != activeRequest.wayPoints.Count)
        {
            currentRequestDialogueText.text = "<color=red>You haven't passed through all the waypoints yet!</color>";
            StartCoroutine(WaitSomeTimeResetDialogue(3f));
            return;
        }
        
        movement.canMove = false;
        timer.StopTimer();
        
        float timeTaken = timer.elapsedTime;
        float distancePoint = movement.movementCounter;
        float moneyEarned = Mathf.Max(0, activeRequest.requestPrice - timeTaken * timeConstant - distancePoint * 0.1f);
        AddMoney(moneyEarned);
        
        calculationText.text = $"<color=white>Actual Price: {activeRequest.requestPrice:F1}</color>\n" +
                               $"<color=red>Time Penalty: {timeTaken * timeConstant:F1}</color>\n" +
                               $"<color=red>Distance Penalty: {distancePoint * 0.1f:F1}</color>";
        calculationText2.text = $"<color=green>Money Earned: {moneyEarned}</color>";
        
        // Deactivate current request
        activeRequest.gameObject.SetActive(false);
        timer.ResetTimer();
        movement.ResetMovementCounter();
        waypointPassCounterForCurrentRequest = 0;
        
        // Start a new request
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
    
    public void AddMoney(float amount)
    {
        totalMoney += amount;
        totalMoneyText.text = $"{totalMoney}";
    }
}
