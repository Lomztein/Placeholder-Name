﻿using Lomztein.PlaceholderName.Characters;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;
using Lomztein.PlaceholderName.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.PlaceholderName.AI {

    /// <summary>
    /// A very basic AI character that finds a target, aims towards it, and fires its tool in a burst after a set delay. Not too bright, should work well as basic enemies.
    /// </summary>
    public class BasicHumanoidAI : MonoBehaviour {

        public Humanoid humanoid;

        private CharacterEquipment.Slot toolSlot;
        private Tool currentTool;

        public Transform target;

        public float sightRange;
        public float attackRange;

        private bool isAttacking = false;

        public float attackDelayTime;
        public float attackBurstTime;

        // Use this for initialization
        void Start() {
            toolSlot = humanoid.equipment.GetSlot (CharacterEquipment.Type.Tool);
            toolSlot.OnItemChanged += ToolSlot_OnItemChanged;
        }

        private void ToolSlot_OnItemChanged(ItemSlot itemSlot, Item oldItem, Item newItem) {
            currentTool = toolSlot.currentObject.GetComponent<Tool> ();
        }

        // Update is called once per frame
        void Update() {
            if (target == null) {
                target = TargetFinder.FindClosest (transform.position, sightRange, humanoid.targetLayer);
                humanoid.Move (Vector3.zero, Time.deltaTime);
            } else {
                float distanceToTarget = Vector3.Distance (target.position, transform.position);

                if (!isAttacking) {
                    Vector3 direction = (target.position - transform.position).normalized;

                    humanoid.Aim (target.position + humanoid.center, Time.deltaTime);

                    if (distanceToTarget < attackRange) {
                        humanoid.Move (Vector3.zero, Time.deltaTime);
                        StartCoroutine (Attack ());
                    } else {
                        humanoid.Move (direction, Time.deltaTime);
                    }
                }

                if (distanceToTarget > sightRange)
                    target = null;
            }
        }

        private IEnumerator Attack () {
            isAttacking = true;

            yield return new WaitForSeconds (attackDelayTime);

            float remainingAttackTime = attackBurstTime;
            while (remainingAttackTime > 0f) {

                humanoid.HoldTool (currentTool);
                remainingAttackTime -= Time.deltaTime;

                yield return null;
            }

            isAttacking = false;
        }
    }

}