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

    public void LumberTrees(MineTrees job)
    {
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
            if (avaibleTrees.Count() > 0)
            {
                int treeId = Random.Range(0, avaibleTrees.Count);
                _myTree = avaibleTrees[treeId].gameObject;
                avaibleTrees[treeId].ocuped = true;
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
                yield return false;
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

    public void EndJob()
    {
        lumberJob = null;
        currentJob = null;
        haveTask = false;
    }
}
