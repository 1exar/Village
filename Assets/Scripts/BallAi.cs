using System;
using System.Collections;
using System.Collections.Generic;
using Jobs;
using UnityEngine;
using UnityEngine.AI;

public class BallAi : MonoBehaviour
{

    private NavMeshAgent _agent;
    private float castRadius = 2;
    private SpriteRenderer _spriteRenderer;
    private int _startOrder;
    [SerializeField] private bool followMouse;

    public bool haveTask = false;

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
                if (cast.transform.position.y - 0.3f < transform.position.y)
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

    public void MakeJob(Task job)
    {

        currentJob = job;
        
        if (job.GetType() == typeof(MineTrees))
        {
            followMouse = false;
            haveTask = true;
            StartCoroutine(ProccesLumberJob());
        }

        if (job.GetType() == typeof(CollectAllDrops))
        {
            
        }
        
    }

    private GameObject _myTree;
    private IEnumerator ProccesLumberJob()
    {
        if (!_myTree)
        {
            _myTree = GameObject.FindGameObjectWithTag("Tree");
        }

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
            _myTree.GetComponent<Tree>().TakeHurt(2);
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(ProccesLumberJob());
    }
}
