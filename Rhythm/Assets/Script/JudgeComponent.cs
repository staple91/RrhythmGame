using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JudgeNode
{
    public JudgeNode(float rangeValue, Result resultValue)
    {
        range = rangeValue;
        result = resultValue;
    }
    public JudgeNode SetNextNode(JudgeNode nextNodeValue)
    {
        nextNode = nextNodeValue;
        return this;
    }

    public Result CalcNode(float dist)
    {
        if (dist < range)
            return result;
        else
        {
            if(nextNode == null)
            {
                return Result.None;
            }
            return nextNode.CalcNode(dist);
        }
    }

    public float range;
    public Result result;
    public JudgeNode nextNode;
}

public class JudgeComponent : MonoBehaviour, IJudgeable
{
    public JudgeManager judgeManager;

    public Result Judge(float dist)
    {
        return judgeManager.judgeNode.CalcNode(dist);
    }

}
