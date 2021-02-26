using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels Data", menuName = "Levels Data")]
public class LevelsData : ScriptableObject
{
    public List<GameObject> prefabsLevels;
}

