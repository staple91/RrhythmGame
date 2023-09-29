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

    const float perfectRange = 10.0f;
    const float greatRange = 30.0f;
    const float goodRange = 50.0f;
    const float badRange = 70.0f;
    const float missRange = 100.0f;

    public JudgeNode judgeNode = null;
    
    [SerializeField]
    JudgeComponent[] judges;
    void InitNodeList()
    {
        judgeNode = new JudgeNode(perfectRange, Result.Perfect);
        judgeNode.SetNextNode(new JudgeNode(greatRange, Result.Great).
            SetNextNode(new JudgeNode(goodRange, Result.Good).
            SetNextNode(new JudgeNode(badRange,Result.Bad).
            SetNextNode(new JudgeNode(missRange,Result.Miss)))));
        Debug.Log(judgeNode.nextNode.result);
    }
    private void Start()
    {
        InitNodeList();
        foreach (JudgeComponent judge in judges)
        {
            judge.judgeManager = this;
        }
    }

    bool TryCalcDist(int index, out float dist)
    {
        if(NoteSpawner.noteQueueList[index].Count > 0)
        {
            dist = (NoteSpawner.noteQueueList[index].Peek().gameObject.transform.position - judges[index].transform.position).magnitude;
            return true;
        }
        else
        {
            dist = 404;
            return false;
        }

    }
    void OnFirstButton()
    {
        if(TryCalcDist(0, out float tempDist))
            judges[0].Judge(tempDist);
        Debug.Log(judges[0].Judge(tempDist));
    }
    void OnSecondButton()
    {
        if (TryCalcDist(1, out float tempDist))
            judges[0].Judge(tempDist);
        Debug.Log(judges[1].Judge(tempDist));
    }
    void OnThirdButton()
    {
        if (TryCalcDist(2, out float tempDist))
            judges[0].Judge(tempDist);
        Debug.Log(judges[2].Judge(tempDist));
    }
    void OnForthButton()
    {
        if (TryCalcDist(3, out float tempDist))
            judges[0].Judge(tempDist);
        Debug.Log(judges[3].Judge(tempDist));
    }
    void OnFifthButton()
    {
        if (TryCalcDist(4, out float tempDist))
            judges[0].Judge(tempDist);
        Debug.Log(judges[4].Judge(tempDist));
    }

}
