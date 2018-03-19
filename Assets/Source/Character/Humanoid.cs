using Lomztein.PlaceholderName.Characters.Extensions;
using Lomztein.PlaceholderName.Characters.SerializableStats;
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
        public const float animationAimDegrees = 45f;

        public CharacterController character;
        public Transform toolTransform;
        private Vector3 gravVel = Vector3.zero;

        private float angle;
        private float animationAngle;

        private Vector3 movement;

        public Animator humanoidAnimator;

        public void Aim(Vector3 point, float deltaTime) {
            if (movement.sqrMagnitude < 0.1f)
                movement = (point - transform.position).normalized;

            angle = Trigonometry.CalculateAngleXZ (transform.position, point);
            transform.rotation = Quaternion.Euler (0f, Trigonometry.CalculateAngleXZ (transform.position, point), 0f);
            toolTransform.LookAt (point);
        }

        public void Move(Vector3 direction, float deltaTime) {
            if (direction.sqrMagnitude > 0.1f)
                movement = direction;

            character.Move ((direction * speed.GetAdditiveValue () + gravVel) * deltaTime);
        }

        private void UpdateAnimator () {
            float maxSpeed = speed.GetAdditiveValue ();
            float speed01 = character.velocity.magnitude / maxSpeed;
            humanoidAnimator.speed = maxSpeed / naturalAnimationSpeed;

            float movementAngle = Trigonometry.CalculateAngleXZ (transform.position, transform.position + movement); // The angle that the character is moving towards.
            float relative = Mathf.DeltaAngle (angle, movementAngle); // The delta-angle between the looking and the movement angle.

            Vector2 blendVector = new Vector2 (
                Mathf.Cos (relative * Mathf.Deg2Rad) * speed01,
                Mathf.Sin (relative * Mathf.Deg2Rad) * speed01
                );

            animationAngle = Mathf.Sin (Time.time) * animationAimDegrees;

            humanoidAnimator.SetFloat ("DirectionX", blendVector.x);
            humanoidAnimator.SetFloat ("DirectionY", blendVector.y);
            humanoidAnimator.SetFloat ("AimAngle", (animationAngle + animationAimDegrees) / (animationAimDegrees * 2f));
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
