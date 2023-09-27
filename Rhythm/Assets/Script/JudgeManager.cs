using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Result
{
    Perfect,
    Great,
    Good,
    Bad,
    Miss,
    None
}
public interface IJudgeable
{
    Result Judge(float dist);
}

public class JudgeManager : MonoBehaviour
{

    [SerializeField]
    JudgeComponent[] judges;

    float CalcDist(int index)
    {
        return (NoteSpawner.noteQueueList[index].Peek().gameObject.transform.position - judges[index].transform.position).magnitude;
    }
    void OnFirstButton()
    {
        judges[0].Judge(CalcDist(0));
    }
    void OnSecondButton()
    {
        judges[1].Judge(CalcDist(1));
    }
    void OnThirdButton()
    {
        judges[2].Judge(CalcDist(2));
    }
    void OnForthButton()
    {
        judges[3].Judge(CalcDist(3));
    }
    void OnFifthButton()
    {
        judges[4].Judge(CalcDist(4));
    }

}
