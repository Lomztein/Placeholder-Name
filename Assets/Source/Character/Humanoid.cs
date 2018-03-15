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

        public CharacterController character;
        public Transform toolTransform;
        private Vector3 gravVel = Vector3.zero;

        public void Aim(Vector3 point, float deltaTime) {
            transform.rotation = Quaternion.Euler (0f, Trigonometry.CalculateAngleXZ (transform.position, point), 0f);
            toolTransform.LookAt (point);
        }

        public void Move(Vector3 direction, float deltaTime) {
            character.Move ((direction * speed.GetAdditiveValue () + gravVel) * deltaTime);
        }

        private void FixedUpdate() {
            if (character.isGrounded)
                gravVel = Vector3.zero;
            gravVel += Physics.gravity * Time.fixedDeltaTime;
        }

        // Use this for initialization

        // Update is called once per frame
        void Update() {

        }
    }

}
