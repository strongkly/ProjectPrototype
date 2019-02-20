using UnityEngine;

public static class AnimationExtends
{
    public static void RewindToFirstFrame(this Animation animation)
    {
        if (animation.isPlaying)
        {
            animation.Rewind();
        }
        else
        {
            animation.Play();
            animation.Sample();
            animation.Stop();
        }
    }
}
