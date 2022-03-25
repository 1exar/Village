using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;
using UnityEngine.AI;

public class BallCollectJob : MonoBehaviour
{
    
    public float doDelay;

    public void CollectAllItems(CollectDropTask job)
    {
        collectDropsJob = job;
        GetComponent<Ball>().haveTask = true;
        GetComponent<Ball>().currentJob = Ball.jobVariant.collect;
        GetComponent<Ball>().jobCoroutine = StartCoroutine(ProccesCollectDropJob(job));
    }

    [SerializeField] private CollectDropTask collectDropsJob;
    private GameObject _myDrop;
    private bool _pickupDrop;

    private IEnumerator ProccesCollectDropJob(CollectDropTask job)
    {
        GetComponent<Ball>().haveTask = true;
        if (!_myDrop)
        {
            List<DropedItem> avaibleDropedItems = job.itemsToCollect.Where(i => !i.occuped).ToList();
            if (avaibleDropedItems.Count > 0)
            {
                _myDrop = FindClosestEnemy(avaibleDropedItems).gameObject;
                _myDrop.GetComponent<DropedItem>().occuped = true;
            }
            else if (avaibleDropedItems.Count == 1)
            {
                avaibleDropedItems[0].occuped = true;
                _myDrop = avaibleDropedItems[0].gameObject;
            }
            else
            {
                GetComponent<Ball>().EndJob();
                yield break;
            }
        }
        else
        {
            if (!_pickupDrop)
            {
                if (Vector2.Distance(transform.position, _myDrop.transform.position) > 1f)
                {
                    GetComponent<NavMeshAgent>().destination = _myDrop.transform.position;
                    yield return new WaitForSeconds(doDelay);
                }
                else
                {
                    _myDrop.GetComponent<DropedItem>().ballPos = transform;
                    _myDrop.GetComponent<DropedItem>().moveToBall = true;
                    _pickupDrop = true;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, BuildingManager.I.mainStorage.transform.position + BuildingManager.I.mainStorage.GetComponent<Building>().fronPosOffset) > 1f)
                {
                    GetComponent<NavMeshAgent>().destination = BuildingManager.I.mainStorage.transform.position + BuildingManager.I.mainStorage.GetComponent<Building>().fronPosOffset;
                    yield return new WaitForSeconds(doDelay);
                }
                else
                {
                    _myDrop.GetComponent<DropedItem>().MoveToStorage(BuildingManager.I.mainStorage.GetComponent<Building>().storage);
                    _pickupDrop = false;
                    _myDrop = null;
                }
            }
        }

        yield return new WaitForSeconds(doDelay);
        GetComponent<Ball>().jobCoroutine = StartCoroutine(ProccesCollectDropJob(job));
    }
    
    DropedItem FindClosestEnemy(List<DropedItem> objects)
    {
        DropedItem closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (DropedItem go in objects) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance< distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void CancelJob()
    {
        StopAllCoroutines();
        if (_myDrop)
        {
            if (_pickupDrop)
            {
                DropedItem item = _myDrop.GetComponent<DropedItem>();
                item.occuped = false;
                item.used = false;
                item.jobWithMe = null;
                item.moveToBall = false;
                _pickupDrop = false;
            }

            _myDrop = null;
            
            GetComponent<NavMeshAgent>().ResetPath();
        }
    }
    
}