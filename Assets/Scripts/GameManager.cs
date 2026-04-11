using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Request> requests;
    public Request activeRequest;
    
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
        Debug.Log(activeRequest.startPoint + " to " + activeRequest.endPoint + " with " + activeRequest.obstacles.Count + " obstacles and " + activeRequest.trafficJams.Count + " traffic jams.");

    }
}
