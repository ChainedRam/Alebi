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
        /// <summary>
        /// Speed in which the object will move. 
        /// </summary>
        public float Speed;

        private float UnEffectedSpeed; 

        public int Health;

        /// <summary>
        /// Holds active status effects on player. 
        /// </summary>
        private List<KeyValue<StatusEffect, float>> Effects;

        #endregion
        #region Private Variables
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
            UnEffectedSpeed = Speed;
            Effects = new List<KeyValue<StatusEffect, float>>(); 
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
            foreach (var pair in ArrowsDirectionDic)
            {
                if (Input.GetKey(pair.Key))
                {
                    StorePlayerState();
                    ApplyStatusEffects();
                    MovePlayer(pair.Value);
                    RestorePlayerState(); 
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
            rigidbody2D.velocity += direction * Speed * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Stored player status before getting effected by status effects.
        /// </summary>
        private void StorePlayerState()
        {
            UnEffectedSpeed = Speed;
        }

        /// <summary>
        /// Restores player status after getting effected by statusEfects. 
        /// </summary>
        private void RestorePlayerState()
        {
            Speed = UnEffectedSpeed;
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
                    Effects.RemoveAt(i);
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
        public void AddEffect(StatusEffect e, float Duration)
        {
            KeyValue<StatusEffect, float> effect = Effects.Where(p => p.Key == e).SingleOrDefault();

            if(effect == null)
            {
                Effects.Add(new KeyValue<StatusEffect, float>(e, Duration));
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
        public void RemoveEffect(StatusEffect e)
        {
            KeyValue<StatusEffect, float> effect = Effects.Where(p => p.Key == e).SingleOrDefault();

            if (effect != null)
            {
                Effects.Remove(effect);
            }
            
        }
        #endregion
    }
}
