using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.PhysicalEquipment;
using Lomztein.PlaceholderName.Characters.SerializableStats;
using Lomztein.PlaceholderName.Items;
using Lomztein.PlaceholderName.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lomztein.PlaceholderName.Characters {

    /// <summary>
    /// Humanoid characters are any character that resembles human form. Meaning a head, torso, two arms and two legs.
    /// </summary>
    public class Humanoid : Character, IControllable {

        public const float naturalAnimationSpeed = 12f;
        public const float aimTreshold = 45f;
        public const float aimLerpTime = 2f;

        public CharacterController character;

        private Vector3 gravVel = Vector3.zero;

        public float movementAngle;
        private float angle;
        private float animationAngle;

        private Vector3 movement;

        public Transform toolTransform;
        public CharacterEquipment.Slot toolSlot;
        public Tool currentTool;

        public Animator humanoidAnimator;

        void Start() {
            toolSlot = equipment.GetSlot (CharacterEquipment.Type.Tool);
            toolSlot.OnItemChanged += ToolSlot_OnItemChanged;
        }

        private void ToolSlot_OnItemChanged(ItemSlot itemSlot, Item oldItem, Item newItem) {
            currentTool = toolSlot.currentObject.GetComponent<Tool> ();
        }

        public void Aim(Vector3 point, float deltaTime) {
            if (movement.sqrMagnitude < 0.1f)
                movement = (point - transform.position).normalized;

            angle = Trigonometry.CalculateAngleXZ (transform.position, point);
            float delta = Mathf.DeltaAngle (movementAngle, angle);
            float sign = Mathf.Sign (delta);

            if (Mathf.Abs (delta) > aimTreshold)
                movementAngle += (delta - aimTreshold * sign);
            else
                movementAngle = Mathf.LerpAngle (movementAngle, angle, aimLerpTime * deltaTime);

            transform.rotation = Quaternion.Euler (0f, movementAngle, 0f);
            toolTransform.LookAt (point);
        }

        public void Move(Vector3 direction, float deltaTime) {
            if (movement.sqrMagnitude > 0.1f)
                movement = direction;
            character.Move ((direction * speed.GetAdditiveValue () + gravVel) * deltaTime);
        }

        private void UpdateAnimator () {
            float maxSpeed = speed.GetAdditiveValue ();
            float speed01 = character.velocity.magnitude / maxSpeed;
            humanoidAnimator.speed = maxSpeed / naturalAnimationSpeed;

            float aimAngle = Trigonometry.CalculateAngleXZ (transform.position, transform.position + movement); // The angle that the character is moving towards.
            float relative = Mathf.DeltaAngle (aimAngle, movementAngle); // The delta-angle between the looking and the movement angle.

            animationAngle = Mathf.DeltaAngle (movementAngle, angle);

            Vector2 blendVector = new Vector2 (
                Mathf.Cos (relative * Mathf.Deg2Rad) * speed01,
                Mathf.Sin (relative * Mathf.Deg2Rad) * speed01
                );

            humanoidAnimator.SetFloat ("DirectionX", blendVector.x);
            humanoidAnimator.SetFloat ("DirectionY", blendVector.y);
            humanoidAnimator.SetFloat ("AimAngle", (animationAngle + aimTreshold) / (aimTreshold * 2f));
        }

        public override void FixedUpdate() {
            base.FixedUpdate ();

            if (character.isGrounded)
                gravVel = Vector3.zero;
            gravVel += Physics.gravity * Time.fixedDeltaTime;
        }

        // Use this for initialization

        // Update is called once per frame
        void Update() {
            UpdateAnimator ();
        }
    }

}
