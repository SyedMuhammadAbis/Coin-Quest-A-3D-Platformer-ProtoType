using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] wayPoints;
    int currentWaypointIndex = 0; // 0 first waypoint ta weelay shee zakka sa most prog langu 0 na counting start kee
    [SerializeField] float speed = 1f;




    void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
                           //this method calculates a new position between2 game objects.
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }//da platform wala trans.              //platforms current pos                                                   // ka dazeee kay sirf speed waleekoo no da platform ba eeway freame kay yo unit movement kee no da der zyat day no bya zakka moo warsa time . delta time walagawal
}
