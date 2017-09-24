using ChainedRam.Alebi.Core;
using ChainedRam.Alebi.Interface.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Alebi.Battle
{
    /// <summary>
    /// Fires a projectile in a pattenr 
    /// </summary>
    //TODO abstract 
    public abstract class Pattern : Runnable
    {
        [Header("Pattern")]
        public bool IsGenerating; 
        /// <summary>
        /// Projectlie to clone.
        /// </summary>
        public Projectile projectilePrefab;

        [Header("Global Settings")]
        [Range(.1f, 2f)]
        /// <summary>
        /// Value to enhave all projectile speeds.
        /// </summary>
        public float Acceleration;

        [Header("Optimizations")]
        [Range(0f, 5f)]
        /// <summary>
        /// Time between each generated projectile 
        /// </summary>
        public float ReloadTime; 

        [Range(0, 5)]
        /// <summary>
        /// Time to live before getting deleted. 
        /// </summary>
        public float ProjectileTTL;
       
        /// <summary>
        /// Maximum number of projectiles to hold.
        /// </summary>
        public int QueueSize;
       
        /// <summary>
        /// Keeps track of projected objects. 
        /// </summary>
        private Queue<Projectile> ProjectileQueue;

        private float CurrentTime; 


        void Start()
        {
            IsGenerating = false; 
            ProjectileQueue = new Queue<Projectile>(QueueSize);
            CurrentTime = 0;
        }

        /// <summary>
        /// Generate projectiles 
        /// </summary>
        private void Update()
        {
            if((CurrentTime -= Time.deltaTime) < 0)
            {
                GenerateProjectile();
                CurrentTime = ReloadTime; 
            }
        }

        /// <summary>
        /// Creates and projects a new projectile. 
        /// </summary>
        protected void GenerateProjectile()
        {
            Projectile pro; 

            if(ProjectileQueue.Count >= QueueSize)
            {
                pro = ProjectileQueue.Dequeue();
            }
            else
            {
                pro = Instantiate(projectilePrefab);
            }

            ProjectileQueue.Enqueue(pro);

            pro.Run(); 
            Project(pro);
        }

        /// <summary>
        /// Projects a generated object. 
        /// </summary>
        /// <param name="pro"></param>
        public abstract void Project(Projectile pro);
       
        /// <summary>
        /// Enables creating projectiles. 
        /// </summary>
        public override void Run()
        {
            base.Run();
            IsGenerating = true;
        }

        /// <summary>
        /// Stops projecting and destroy all created projectiles. -TODO queue and reload. 
        /// </summary>
        public override void Stop()
        {
            base.Stop(); 
            IsGenerating = false;

            foreach (Projectile p in ProjectileQueue)
            {
                p.Stop();
                Destroy(p.gameObject, ProjectileTTL);
            }

            ProjectileQueue.Clear();
        }
    }
}
