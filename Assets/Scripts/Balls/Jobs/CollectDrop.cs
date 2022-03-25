using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jobs
{
    public class CollectDrop
    {
        public CollectDrop(List<Ball> balls, List<DropedItem> type)
        {
            CollectDropTask job = new CollectDropTask();
            job.name = "Собирать рессурсы";

            if (type.Count == 0)
            {
                job.itemsToCollect.Clear();
                job.itemsToCollect = WorldResourceManager.I.dropedItems;
                job.progressMax = WorldResourceManager.I.dropedItems.Count;
                foreach (var item in WorldResourceManager.I.dropedItems)
                {
                    item.jobWithMe = job;
                }
            }
            else
            {
                job.itemsToCollect.Clear();
                foreach (var dropedItem in WorldResourceManager.I.dropedItems)
                {
                    foreach (var item in type)
                    {
                        if (dropedItem.Item.Name == item.Name && dropedItem.used == false)
                        {
                            dropedItem.used = true;
                            job.itemsToCollect.Add(dropedItem);
                            dropedItem.jobWithMe = job;
                            job.progressMax++;
                        }
                    }
                }

                job.name = "Собирать " + type[0].Name;
            }

            foreach (var ball in balls)
            {
                ball.GetComponent<BallCollectJob>().CollectAllItems(job);
            }

            job.balls = balls;
            JobsManager.I.collectAllDropTasks.Add(job);
            UIManager.I.AddNewTaskToTaskList(job);
        }
    }
    
    [Serializable]
    public class CollectDropTask : Task
    {

        public List<DropedItem> itemsToCollect = new List<DropedItem>();

        public void OnItemMovedToStorage()
        {
            progressCurrent++;
            if (progressCurrent >= progressMax)
            {
                JobsManager.I.collectAllDropTasks.Remove(this);
            }
        }

    }
}