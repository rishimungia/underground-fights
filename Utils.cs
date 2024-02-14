using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace UndergroundFights
{
    public class Utils : Script
    {
        public Utils()
        {

        }

        internal static void LoadSettings()
        {
            UGF._settings = ScriptSettings.Load(@"scripts\UGF.ini");
        }

        internal static string LoadAnimation(string animDict)
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, animDict);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, animDict)) Wait(50);

            return animDict;
        }

        internal static int PlayAnimation(Ped ped, string animDict, string animName, int animFlag = 0, float blendIn = 2.5f, float blendOut = 2.5f)
        {
            Function.Call(Hash.TASK_PLAY_ANIM,
                ped,
                animDict,
                animName,
                blendIn, (-1 * blendOut),
                -1, animFlag
            );
            return (int)(Function.Call<float>(Hash.GET_ANIM_DURATION, animDict, animName) * 1000);
        }
        
        internal static int PlayAnimationAdvanced(Ped ped, string animDict, string animName, Vector3 position, int animFlag = 0, float blendIn = 2.5f, float blendOut = 2.5f)
        {
            Function.Call(Hash.TASK_PLAY_ANIM_ADVANCED,
                ped,
                animDict,
                animName,
                position.X,
                position.Y,
                position.Z,
                ped.Rotation.X,
                ped.Rotation.Y,
                ped.Rotation.Z,
                blendIn, (-1 * blendOut),
                -1, animFlag
            );
            return (int)(Function.Call<float>(Hash.GET_ANIM_DURATION, animDict, animName) * 1000);
        }

        internal static int GetRandomInt(int min, int max)
        {
            return Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, min, max);
        }

    }
}
