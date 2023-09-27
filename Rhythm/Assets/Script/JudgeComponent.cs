using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JudgeRange
{
    public const float perfectRange = 1.0f;
    public const float greatRange = 3.0f;
    public const float goodRange = 5.0f;
    public const float badRange = 7.0f;
    public const float missRange = 10.0f;
}
public class JudgeComponent : MonoBehaviour, IJudgeable
{
    public Result Judge(float dist)
    {
        //JudgeRange를 클래스로 해서 next범위를 가지고있고 foreach문을 돌려버린다.
        // 공통 - 노트 dequeue해서 삭제(objpoolReturn) / 마지막빼고.
        if (dist < JudgeRange.perfectRange)
        {
            //점수많이추가
            return Result.Perfect;
        }
        else if (dist < JudgeRange.greatRange)
        {
            //점수 덜많이 추가
            return Result.Great;
        }
        else if (dist < JudgeRange.goodRange)
        {
            //점수 보통추가 콤보끊김(초기화).
            return Result.Good;
        }
        else if (dist < JudgeRange.badRange)
        {
            //점수 조금추가 콤보끊김.
            return Result.Bad;
        }
        else if (dist < JudgeRange.missRange)
        {
            //점수 없음 콤보끊김.
            return Result.Miss;
        }
        else // 아무것도 안함. 노트도 안없어짐.
            return Result.None;
    }

}
