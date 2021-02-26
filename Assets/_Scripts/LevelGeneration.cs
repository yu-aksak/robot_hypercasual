using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private List<LevelsData> _levelsDatas = new List<LevelsData>();
    
    public void Generate(int typeLevel, int level)
    {
        GameObject _level = Instantiate(_levelsDatas[typeLevel].prefabsLevels[level - 1], new Vector3(0, 0, 0), Quaternion.identity,
            gameObject.transform);
    }
}
