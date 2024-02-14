using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace UndergroundFights
{
    public class UGF : Script
    {
        internal static ScriptSettings _settings = ScriptSettings.Load(@"scripts\UGF.ini");

        internal static GTA.Control modifierKey = GTA.Control.Sprint;
        private bool modifierDown;

        internal static Ped opponent;
        internal static Ped player;

        public UGF()
        {
            Utils.LoadSettings();
            player = Game.Player.Character;

            Tick += OnTick;
            KeyUp += OnKeyUp;
            KeyDown += OnKeyDown;
        }

        void OnTick(object sender, EventArgs e)
        {
            if (Game.IsControlJustPressed(modifierKey)) modifierDown = true;

            if (Game.IsControlJustReleased(modifierKey)) modifierDown = false;
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J && modifierDown) Utils.LoadSettings();

            if (e.KeyCode == Keys.B && modifierDown) SpawnDebug();

            if (e.KeyCode == Keys.J && !modifierDown) opponent?.Delete();
            
            if (e.KeyCode == Keys.K && !modifierDown) Finisher.PerformFinisher(Game.Player.Character, opponent);

            if (e.KeyCode == Keys.K && modifierDown) PerformFinisher();
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        internal static void PerformFinisher (bool killOpponent = false)
        {
            Utils.LoadAnimation("hand_to_hand_finishers_p2");

            int flagP = _settings.GetValue<int>("Debug", "FLAG_P", 0);
            int flagO = _settings.GetValue<int>("Debug", "FLAG_O", 0);

            int animIndex = _settings.GetValue<int>("Debug", "ANIM_INDEX", 1);

            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY,
                opponent,
                player,
                0,
                _settings.GetValue<float>("Debug", "ANIM_X", 0),
                _settings.GetValue<float>("Debug", "ANIM_Y", 0),
                _settings.GetValue<float>("Debug", "ANIM_Z", 0),
                0.0f, 0.0f, -180f, // rotation
                true, // unknown
                true, // useSoftPinning
                true, // collision
                true, // isPed
                1, true
            );

            if (player.IsAttachedTo(opponent))
                Function.Call(Hash.DETACH_ENTITY, opponent, true, true);

            Utils.PlayAnimation(player, "hand_to_hand_finishers_p2", "atc_finisher_" + animIndex, flagP);
            Utils.PlayAnimation(opponent, "hand_to_hand_finishers_p2", "vic_finisher_" + animIndex, flagO);
        }

        internal static void SpawnDebug()
        {
            GTA.UI.Screen.ShowSubtitle("Debug Scene Load");

            Vector3 spawn = new Vector3(Game.Player.Character.Position.X + 1, Game.Player.Character.Position.Y + 1, Game.Player.Character.Position.Z);

            opponent = World.CreatePed(new Model(PedHash.Bodybuild01AFM), spawn, Game.Player.Character.Heading);
        }

    }
}
