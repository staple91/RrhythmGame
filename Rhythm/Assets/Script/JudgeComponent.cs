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
        //JudgeRange�� Ŭ������ �ؼ� next������ �������ְ� foreach���� ����������.
        // ���� - ��Ʈ dequeue�ؼ� ����(objpoolReturn) / ����������.
        if (dist < JudgeRange.perfectRange)
        {
            //���������߰�
            return Result.Perfect;
        }
        else if (dist < JudgeRange.greatRange)
        {
            //���� ������ �߰�
            return Result.Great;
        }
        else if (dist < JudgeRange.goodRange)
        {
            //���� �����߰� �޺�����(�ʱ�ȭ).
            return Result.Good;
        }
        else if (dist < JudgeRange.badRange)
        {
            //���� �����߰� �޺�����.
            return Result.Bad;
        }
        else if (dist < JudgeRange.missRange)
        {
            //���� ���� �޺�����.
            return Result.Miss;
        }
        else // �ƹ��͵� ����. ��Ʈ�� �Ⱦ�����.
            return Result.None;
    }

}
