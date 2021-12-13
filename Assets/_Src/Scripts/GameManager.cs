using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<GameplayDialog>().StartStopWatch();
        }
    }
}
