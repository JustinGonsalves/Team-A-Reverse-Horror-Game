using UnityEngine;

public class CutsceneAnimatorResetManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimatorTarget
    {
        public Animator animator;
        public string defaultState;
    }

    public AnimatorTarget[] targets;

    public void ResetAllAnimators()
    {
        foreach (var target in targets)
        {
            if (target.animator != null)
            {
                target.animator.Play(target.defaultState, 0, 0f);
                target.animator.Update(0f);
            }
        }
    }
}
