using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;
using UnityEngine.AI;

public class BallCollectJob : MonoBehaviour
{
    public void CollectAllItems(CollectDrops job)
    {
        collectDropsJob = job;
        StartCoroutine(ProccesCollectDropJob(job));
    }

    [SerializeField] private CollectDrops collectDropsJob;
    private GameObject _myDrop;
    private bool _pickupDrop;

    private IEnumerator ProccesCollectDropJob(CollectDrops job)
    {
        if (!_myDrop)
        {
            List<DropedItem> avaibleDropedItems = job.itemsToCollect.Where(i => !i.occuped).ToList();
            if (avaibleDropedItems.Count > 0)
            {
                int dropId = Random.Range(0, avaibleDropedItems.Count);
                avaibleDropedItems[dropId].occuped = true;
                _myDrop = avaibleDropedItems[dropId].gameObject;
            }
            else if (avaibleDropedItems.Count == 1)
            {
                avaibleDropedItems[0].occuped = true;
                _myDrop = avaibleDropedItems[0].gameObject;
            }
            else
            {
                Debug.Log("No drop");
                GetComponent<BallAi>().EndJob();
                yield break;
            }
        }
        else
        {
            if (!_pickupDrop)
            {
                if (Vector2.Distance(transform.position, _myDrop.transform.position) > 0.2f)
                {
                    Debug.Log("go to drop");
                    GetComponent<NavMeshAgent>().destination = _myDrop.transform.position;
                    yield return new WaitForSeconds(1);
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
                if (Vector2.Distance(transform.position, BuildingManager.I.mainStorage.transform.position) > 1f)
                {
                    GetComponent<NavMeshAgent>().destination = BuildingManager.I.mainStorage.transform.position;
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    _myDrop.GetComponent<DropedItem>().MoveToStorage();
                    _pickupDrop = false;
                    _myDrop = null;
                }
            }
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(ProccesCollectDropJob(job));
    }
}