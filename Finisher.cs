using GTA;
using GTA.Math;
using GTA.Native;
using System;

namespace UndergroundFights
{
    public class Finisher : Script
    {
        private static Vector3[] animOffset = {
            new Vector3(0.1f, 1.4f, 0.0f),
            new Vector3(0.2f, 1.45f, -0.1f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.15f, 1.65f, 0.0f),
            new Vector3(-0.1f, 1.0f, 0.0f),
            new Vector3(-0.1f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.45f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.3f, 0.0f),
            new Vector3(0.0f, 1.3f, 0.0f),
        };

        public Finisher()
        {
            Tick += OnTick;
        }

        void OnTick(object sender, EventArgs e)
        {

        }

        internal static void PerformFinisher(Ped attacker, Ped victim, bool killOpponent = false)
        {
            Utils.LoadAnimation("hand_to_hand_finishers_p2");

            int animIndex = Utils.GetRandomInt(1, 11);

            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY,
                victim,
                attacker,
                0,
                animOffset[animIndex - 1].X,
                animOffset[animIndex - 1].Y,
                animOffset[animIndex - 1].Z,
                0.0f, 0.0f, -180f, // rotation
                true, // unknown
                true, // useSoftPinning
                true, // collision
                true, // isPed
                1, true
            );

            if (attacker.IsAttachedTo(victim))
                Function.Call(Hash.DETACH_ENTITY, victim, true, true);

            Utils.PlayAnimation(attacker, "hand_to_hand_finishers_p2", "atc_finisher_" + animIndex, 524296);
            Utils.PlayAnimation(victim, "hand_to_hand_finishers_p2", "vic_finisher_" + animIndex, 520);
        }

    }
}
