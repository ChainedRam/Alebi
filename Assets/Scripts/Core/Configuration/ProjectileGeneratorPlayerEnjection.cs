using System.Collections;
using System.Collections.Generic;
using ChainedRam.Core.Generation;
using ChainedRam.Core.Player;
using UnityEngine;

namespace ChainedRam.Core.Configuration
{
    [RequireComponent(typeof(ProjectileGenerator))]
    public class ProjectileGeneratorPlayerEnjection : Enjection
    {
        public ProjectileGenerator ProjectileGenerator;

        public override void Enject(Player.Player p)
        {
            ProjectileGenerator.GenerateAt.SetRotationFacing(p.transform);
        }

        private void OnValidate()
        {
            if (ProjectileGenerator == null)
            {
                ProjectileGenerator = GetComponent<ProjectileGenerator>();
            }
        }
    } 
}
