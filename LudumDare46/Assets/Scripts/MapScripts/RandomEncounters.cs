using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RandomEncounterData")]
public class RandomEncounters : ScriptableObject
{
    [SerializeField]
    public List<int> encounterBuildIDs;
    [SerializeField]
    public List<bool> usedEncounters;
}
