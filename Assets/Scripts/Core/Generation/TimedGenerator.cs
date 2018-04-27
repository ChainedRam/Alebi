using ChainedRam.Core.Generation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Generation
{
    public abstract class TimedGenerator : OnceGenerator
    {
        [Header("Timed Generator")]

        [ContextMenuItem("Sync", "SyncWaitTime")]
        public float WaitTime = 1;

        private float CurrentTime;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (CurrentTime > 0)
                CurrentTime -= Time.fixedDeltaTime;
        }

        protected override bool ShouldTerminate()
        {
            return CurrentTime <= 0;
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            CurrentTime = WaitTime;
        }

        protected override void Start()
        {
            base.Start();
            CurrentTime = WaitTime;
        }

        private void SyncWaitTime()
        {
            WaitTime = GetSyncedWaitTime();
        }

        protected virtual float GetSyncedWaitTime()
        {
            return 1f;
        }
    }

}