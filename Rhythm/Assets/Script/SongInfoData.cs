using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongInfoData", menuName = "ScriptableObjects/SongInfoData", order = 1)]
public class SongInfoData : ScriptableObject
{
    [SerializeField]
    public List<SongInfo> songInfos;
}
