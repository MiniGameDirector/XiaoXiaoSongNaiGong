using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController
{
    /// <summary>
    /// 通过动画的名称设置目标物体的动画
    /// </summary>
    /// <param name="targetAnim"></param>
    /// <param name="animState"></param>
    public static void SetAnimatorByName(Animator targetAnim, string animState) {
        targetAnim.SetBool(animState, true);
    }
}
