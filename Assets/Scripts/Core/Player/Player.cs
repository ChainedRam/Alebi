using ChainedRam.Core.Collection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChainedRam.Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        #region Inspecter Variables
        [Header("Player Properties")]
        /// <summary>
        /// Speed in which the object will move. 
        /// </summary>
        public float Speed; 
        public bool CanMove; 
        public float SpeedDelta;  
        public int Health;


        [Header("Status Properties")]
        public StatusDisplayList StatusDisplay;
        #endregion
        #region Private Variables
        /// <summary>
        /// Holds active status effects on player. 
        /// </summary>
        private List<KeyValue<IStatusEffect, float>> Effects;
        //my body is ready, do not inheret
        new private Rigidbody2D rigidbody2D;

        /// <summary>
        /// Holds key names and their direction values. 
        /// </summary>
        private Dictionary<string, Vector2> ArrowsDirectionDic; //can improve to be displayable in inspecter.  
        #endregion
        #region Unity Methods
        private void Start()
        {
            CanMove = true;
            SpeedDelta = 1; 
            Effects = new List<KeyValue<IStatusEffect, float>>(); 
            rigidbody2D = GetComponent<Rigidbody2D>();

            ArrowsDirectionDic = new Dictionary<string, Vector2>()
            {
                { "up", Vector2.up },
                { "down", Vector2.down },
                { "right", Vector2.right },
                { "left", Vector2.left }
            };

        }

        /// <summary>
        /// Move up or left right 
        /// </summary>
        void FixedUpdate()
        {
            StatusDisplay?.SetStatuses(Effects);

            foreach (var pair in ArrowsDirectionDic)
            {
                if (Input.GetKey(pair.Key))
                {
                    ApplyStatusEffects();
                    MovePlayer(pair.Value); ///define player profile struct
                }
            }

            ClearEndedStatudEffects(); 
        }

        /// <summary>
        /// Moves play to a direction based on it's speed. 
        /// </summary>
        /// <param name="direction"></param>
        private void MovePlayer(Vector2 direction)
        {
            if (CanMove)
            {
                rigidbody2D.velocity += direction * ((Speed * SpeedDelta)) * Time.fixedDeltaTime;
            }
        }

        /// <summary>
        /// Applys status efeects on player
        /// </summary>
        private void ApplyStatusEffects()
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                    Effects[i].Key.Apply(this);
            }
        }

        /// <summary>
        /// Removes status effects that have ran out of duration. 
        /// </summary>
        private void ClearEndedStatudEffects()
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].Value -= Time.fixedDeltaTime;

                if (Effects[i].Value <= 0.001f)
                {
                    RemoveEffect(Effects[i].Key); 
                    i--;
                }
            }
        }

        /// <summary>
        /// Handles trigger enter effects . 
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CollisionEffect effect = collision.gameObject.GetComponent<CollisionEffect>();

            if (effect != null)
            {
                effect.OnHit(this);
            }

            AreaEffect areaEffect = collision.gameObject.GetComponent<AreaEffect>();

            if (areaEffect != null)
            {
                areaEffect.AddEffect(this);
            }
        }

        /// <summary>
        /// Handles trigger stay effects.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerStay2D(Collider2D collision)
        {
            AreaEffect areaEffect = collision.gameObject.GetComponent<AreaEffect>();

            if (areaEffect != null)
            {
                areaEffect.RefreshEffect(this);
            }
        }

        /// <summary>
        /// Handles trigger exit effects.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            AreaEffect areaEffect = collision.gameObject.GetComponent<AreaEffect>();

            if (areaEffect != null)
            {
                areaEffect.RemoveEffect(this);
            }
        }

        /// <summary>
        /// Uniquely add a status effects if it's not already added. 
        /// </summary>
        /// <param name="e"></param>
        public void AddEffect(IStatusEffect e, float Duration)
        {
            KeyValue<IStatusEffect, float> effect = Effects.Where(p => p.Key == e).SingleOrDefault();

            if(effect == null)
            {
                Effects.Add(new KeyValue<IStatusEffect, float>(e, Duration));
                e.Init(this); 
            }
            else
            {
                effect.Value = Duration; 
            }
        }

        /// <summary>
        /// Removes status effect if exist. 
        /// </summary>
        /// <param name="e"></param>
        public void RemoveEffect(IStatusEffect e)
        {
            KeyValue<IStatusEffect, float> effect = Effects.Where(p => p.Key == e).SingleOrDefault();

            if (effect != null)
            {
                Effects.Remove(effect);
                e.Revert(this); 
            }    
        }

        public void velocityReset()
        {
            rigidbody2D.velocity = Vector2.zero;
        }
        #endregion
    }
}
