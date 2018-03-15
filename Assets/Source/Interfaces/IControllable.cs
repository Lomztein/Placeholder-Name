using Lomztein.PlaceholderName.Characters.PhysicalEquipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable {

    void Move (Vector3 direction, float deltaTime);

    void Aim(Vector3 point, float deltaTime);

    void PressTool(Tool tool);

    void ReleaseTool(Tool tool);

    void HoldTool(Tool tool);

}
