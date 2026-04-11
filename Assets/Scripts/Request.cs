using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public List<GameObject> obstacles;
    public List<GameObject> trafficJams;
    public List<GameObject> wayPoints;
    public string dialogue;
    public Sprite portrait;
    public float requestPrice;
}
