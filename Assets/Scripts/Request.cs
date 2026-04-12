using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public Transform startPoint, endPoint;
    // Edit these on Unity Inspector: Add obstacles, traffic jams as needed
    // public List<GameObject> obstacles;
    // public List<GameObject> trafficJams;
    public List<GameObject> wayPoints;
    [TextArea(5,10)]
    public string dialogue;
    public Sprite portrait;
    public float requestPrice;
}
