using System.Collections;
using UnityEngine;
using Jobs;
using UnityEditor;

public class Ball : MonoBehaviour
{

    public bool haveTask = false;

    public Coroutine jobCoroutine = null;

    public enum jobVariant
    {
        lumber, collect
    }

    public jobVariant currentJob;
    
    public void EndJob()
    {
        haveTask = false;
    }

    public void CancelJob()
    {
        haveTask = false;
        if (jobCoroutine != null)
        {
            switch (currentJob)
            {
                case jobVariant.collect:
                    GetComponent<BallCollectJob>().CancelJob();
                    break;
                case jobVariant.lumber:
                    GetComponent<BallLumberJob>().CancelJob();
                    break;
            }
        }
    }
    
}