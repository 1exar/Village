using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jobs;
using UnityEngine;
using UnityEngine.AI;

public class BallLumberJob : MonoBehaviour
{
    //
    //Lumber job
    public void LumberTrees(MineTrees job)
    {
        lumberJob = job;
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
                GetComponent<BallAi>().EndJob();
                List<DropedItem> items = new List<DropedItem>();
                foreach (var item in WorldResourceManager.I.dropedItems)
                {
                    if (item.Name == GameManager.I.items.wood.Name || item.Name == GameManager.I.items.BrushWood.Name)
                    {
                        items.Add(item);
                    }
                }
                CollectDrop nextJob = new CollectDrop(1, items);
                yield break;
            }
        }

        if (_myTree)
        {
            if (Vector2.Distance(transform.position, _myTree.transform.position) > 1)
            {
                Debug.Log("go to tree");
                GetComponent<NavMeshAgent>().destination = _myTree.transform.position;
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
}