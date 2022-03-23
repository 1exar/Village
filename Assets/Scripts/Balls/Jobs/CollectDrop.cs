using System;
using System.Collections.Generic;
using System.Linq;

namespace Jobs
{
    public class CollectDrop
    {
        public CollectDrop(int balls, List<DropedItem> type)
        {
            CollectDrops job = new CollectDrops();
            job.progressMax = type.Count;
            job.name = "Искать мусор";
            List<BallAi> avaibleBalls = GameManager.I.balls.Where(b => !b.haveTask).ToList();
            List<BallAi> usedBalls = new List<BallAi>();

            if (type.Count == 0)
            {
                job.itemsToCollect.Clear();
                job.itemsToCollect = WorldResourceManager.I.dropedItems;
            }
            else
            {
                job.itemsToCollect.Clear();
                foreach (var dropedItem in WorldResourceManager.I.dropedItems)
                {
                    foreach (var item in type)
                    {
                        if(dropedItem.Item.Name == item.Name) job.itemsToCollect.Add(dropedItem);
                    }
                }
            }

        
            for (int i = 0; i < balls; i++)
            {
                avaibleBalls[i].GetComponent<BallCollectJob>().CollectAllItems(job);
                usedBalls.Add(avaibleBalls[i]);
            }

            job.balls = usedBalls;
            JobsManager.I.collectAllDropTasks.Add(job);

        }
    }
    
    [Serializable]
    public class CollectDrops : Task
    {

        public List<DropedItem> itemsToCollect = new List<DropedItem>();

    }
}