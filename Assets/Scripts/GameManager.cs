using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    
    public TextMeshProUGUI currentRequestPriceText;
    public TextMeshProUGUI currentRequestDialogueText;

    public GameObject player;
    
    void Start()
    {
        NewRequest();
    }
    
    public void NewRequest()
    {
        // Source - https://stackoverflow.com/a/2312911
        // Posted by Ers, modified by community. See post 'Timeline' for change history
        // Retrieved 2026-04-11, License - CC BY-SA 2.5
        int randomIndex = UnityEngine.Random.Range(0, requests.Count);
        activeRequest = requests[randomIndex];
        activeRequest.gameObject.transform.gameObject.SetActive(true);
        currentRequestPriceText.text = $"{activeRequest.requestPrice}$";
        currentRequestDialogueText.text = activeRequest.dialogue;
        player.transform.position = activeRequest.startPoint.position;
        timer.StartTimer();
    }

    private void Update()
    {
        // detect space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(movement.isOnEndPoint)
                ProcessRecommendedWay();
        }
    }
    
    public void ProcessRecommendedWay()
    {
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
        
        // Start a new request
        NewRequest();
    }
    
    public void AddMoney(float amount)
    {
        totalMoney += amount;
        totalMoneyText.text = $"{totalMoney}";
    }
}
