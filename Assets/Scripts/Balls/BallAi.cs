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

    public void EndJob()
    {
        haveTask = false;
    }
}
