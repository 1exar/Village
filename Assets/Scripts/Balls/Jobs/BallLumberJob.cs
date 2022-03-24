using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;
using UnityEngine.AI;

public class BallLumberJob : MonoBehaviour
{
    
    public float doDelay;

    public void LumberTrees(LumberTreesTask job)
    {
        lumberJob = job;
        GetComponent<Ball>().currentJob = Ball.jobVariant.lumber;
        GetComponent<Ball>().haveTask = true;
        GetComponent<Ball>().jobCoroutine = StartCoroutine(ProccesLumberJob(job));
    }
    private GameObject _myTree;
    [SerializeField] private LumberTreesTask lumberJob;
    private IEnumerator ProccesLumberJob(LumberTreesTask job)
    {
        if (!_myTree)
        {
            List<Tree> avaibleTrees = job.trees.Where(t => !t.ocuped).ToList();
            if (avaibleTrees.Count > 0)
            {
               // int treeId = Random.Range(0, avaibleTrees.Count);
               _myTree = FindClosestEnemy(avaibleTrees).gameObject;
               _myTree.GetComponent<Tree>().ocuped = true;
            }
            else if (avaibleTrees.Count() == 1)
            {
                _myTree = avaibleTrees[0].gameObject;
                avaibleTrees[0].ocuped = true;
            }
            else
            {
                GetComponent<Ball>().EndJob();
                yield break;
            }
        }

        if (_myTree)
        {
            if (Vector2.Distance(transform.position, _myTree.transform.position + _myTree.GetComponent<Tree>().frontPosOffset) > 1)
            {
                GetComponent<NavMeshAgent>().destination = _myTree.transform.position + _myTree.GetComponent<Tree>().frontPosOffset;
                yield return new WaitForSeconds(doDelay);
            }
            else
            {
                yield return new WaitForSeconds(doDelay);
                if(_myTree.GetComponent<Tree>())
                    if (!_myTree.GetComponent<Tree>().TakeHurt(10))
                    {
                        job.ChopOneTree();
                        _myTree = null;
                    }
            }
        }
        
        yield return new WaitForSeconds(doDelay);
        GetComponent<Ball>().jobCoroutine = StartCoroutine(ProccesLumberJob(job));
    }
    
    Tree FindClosestEnemy(List<Tree> objects)
    {
        Tree closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Tree go in objects) {
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
        if (_myTree)
        {
            Tree tree = _myTree.GetComponent<Tree>();
            tree.ocuped = false;
            tree.jobWithMe = null;
            _myTree = null;
        }
        GetComponent<NavMeshAgent>().ResetPath();
    }
    
}