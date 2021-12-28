using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentSpawnPoint;

    public void SetCurrentSpawnPoint(GameObject point)
    {
        currentSpawnPoint = point;
    }
}
