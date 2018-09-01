using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainedRam.Core.Puzzle
{
    public class ParallelBoxContent : TileContent
    {
        private static bool ShouldAnnounce = true;
        public int GroupID = 1;
        private static Dictionary<int, List<ParallelBoxContent>> BoxDictionary = new Dictionary<int, List<ParallelBoxContent>>();

        void Start()
        {
            if (!BoxDictionary.ContainsKey(GroupID))
            {
                BoxDictionary.Add(GroupID, new List<ParallelBoxContent>());
            }
            BoxDictionary[GroupID].Add(this);
        }

        public override bool Move(NeighborDirection dire)
        {
            if (base.Move(dire))
            {
                if (ShouldAnnounce)
                {
                    ShouldAnnounce = false;
                    for (int i = 0; i < BoxDictionary[GroupID].Count; i++)
                    {
                        if (BoxDictionary[GroupID][i] == this)
                        {
                            continue;
                        }
                        BoxDictionary[GroupID][i].Move(dire);
                    }
                    ShouldAnnounce = true;
                }
                return true;
            }
            return false;
        }
    }
}
