using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BallAi : MonoBehaviour
{

    private NavMeshAgent _agent;
    private float castRadius = 2;
    private SpriteRenderer _spriteRenderer;
    private int _startOrder;
    [SerializeField] private bool followMouse;

    public bool haveTask = false;
    [SerializeField]
    private Task currentJob;
    
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startOrder = _spriteRenderer.sortingOrder;
    }

    private void FixedUpdate()
    {
        if(followMouse)
            _agent.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        foreach (var cast in Physics2D.CircleCastAll(transform.position, castRadius, Vector2.right))
        {
            if (cast.transform.GetComponent<Perforating>())
            {
                if (cast.transform.position.y - cast.transform.GetComponent<Perforating>().yOffset < transform.position.y)
                {
                    _spriteRenderer.sortingOrder = _startOrder;
                }
                else
                {
                    _spriteRenderer.sortingOrder = 10;
                }
            }
        }
    }

    //Collec tAll Items Job
    public void CollectAllItems(CollectDrops job)
    {
        haveTask = true;
        currentJob = job;
        collectDropsJob = job;
        followMouse = false;
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
                EndJob();
                followMouse = true;
                yield break;
                yield return false;
            }
        }
        else
        {
            if (!_pickupDrop)
            {
                if (Vector2.Distance(transform.position, _myDrop.transform.position) > 0.2f)
                {
                    Debug.Log("go to drop");
                    _agent.destination = _myDrop.transform.position;
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
                    _agent.destination = BuildingManager.I.mainStorage.transform.position;
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
    //
    //Lumber job
    public void LumberTrees(MineTrees job)
    {
        currentJob = job;
        lumberJob = job;
        followMouse = false;
        haveTask = true;
        StartCoroutine(ProccesLumberJob(job));
    }
    private GameObject _myTree;
    [SerializeField] private MineTrees lumberJob;
    private IEnumerator ProccesLumberJob(MineTrees job)
    {
        if (!_myTree)
        {
            List<Tree> avaibleTrees = job.trees.Where(t => !t.ocuped).ToList();
            if (avaibleTrees.Count > 0)
            {
                int treeId = Random.Range(0, avaibleTrees.Count);
                avaibleTrees[treeId].ocuped = true;
                _myTree = avaibleTrees[treeId].gameObject;
            }
            else if (avaibleTrees.Count() == 1)
            {
                _myTree = avaibleTrees[0].gameObject;
                avaibleTrees[0].ocuped = true;
            }
            else
            {
                Debug.Log("NoTree");
                EndJob();
                followMouse = true;
                yield break;
            }
        }

        if (_myTree)
        {
            if (Vector2.Distance(transform.position, _myTree.transform.position) > 1)
            {
                Debug.Log("go to tree");
                _agent.destination = _myTree.transform.position;
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("choop tree");
                yield return new WaitForSeconds(1);
                if(_myTree)
                    _myTree.GetComponent<Tree>().TakeHurt(10);
            }
        }
        
        yield return new WaitForSeconds(1);
        StartCoroutine(ProccesLumberJob(job));
    }
    //
    public void EndJob()
    {
        lumberJob = null;
        currentJob = null;
        haveTask = false;
        collectDropsJob = null;
    }
}
