using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkieCustomTraits.Content
{
    public class DarkieTraitsBirthRate
    {
        private static int NoChance = 0;
        private static int Rare = 3;
        private static int LowChance = 15;
        private static int MediumChance = 30;
        private static int ExtraChance = 45;
        private static int HighChance = 75;

        public static int TurtleGuyRate { get; set; } = Rare;
        public static int FlashRate { get; set; } = Rare;
        public static int BerserkerRate { get; set; } = Rare;
        public static int HawkEyeRate { get; set; } = Rare;
        public static int TitanRate { get; set; } = Rare;
        public static int TitanShifterRate { get; set; } = Rare;
        public static int WiseOldOneRate { get; set; } = Rare;
        public static int IdiotRate { get; set; } = Rare;
        public static int MountaintRate { get; set; } = Rare;
        public static int AlmightyRate { get; set; } = Rare;
        public static int ThorRate { get; set; } = Rare;
        public static int NightCrawlerRate { get; set; } = Rare;
        public static int ShieldGuyRate { get; set; } = Rare;
        public static int WolfTamerRate { get; set; } = Rare;
        public static int BearTamerRate { get; set; } = Rare;
        public static int DragonTrainerRate { get; set; } = Rare;
        public static int MedicRate { get; set; } = Rare;
        public static int GangsterRate { get; set; } = NoChance;
        public static int MagisterRate { get; set; } = Rare;
        public static int WololoRate { get; set; } = Rare;
        public static int AntManRate { get; set; } = Rare;
        public static int CommanderRate { get; set; } = Rare;
        public static int BloodWolfRate { get; set; } = Rare;
        public static int StuffedRate { get; set; } = Rare;
        public static int HungerRate { get; set; } = Rare;
        public static int DuplikateRate { get; set; } = Rare;
        public static int PhoenixRate { get; set; } = Rare;
        public static int PowerMimicryRate { get; set; } = Rare;
        public static int NullifyRate { get; set; } = Rare;
        public static int ReviverOfDeathRate { get; set; } = Rare;
        public static int DarkNecromancerRate { get; set; } = Rare;
        public static int VampireRate { get; set; } = Rare;
        public static int MirrorManRate { get; set; } = Rare;
        public static int TimeStopperRate { get; set; } = Rare;
        public static int ElectroRate { get; set; } = Rare;


        // This method will be called when config value set. ATTENTION: It might be called when game start.
        public static void TurtleGuySliderConfigCallback(float pCurrentValue)
        {
            TurtleGuyRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Turtle Guy trait to '{TurtleGuyRate}'%");
        }

        public static void FlashSliderConfigCallback(float pCurrentValue)
        {
            FlashRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Flash trait to '{FlashRate}'%");
        }
        public static void BerserkerSliderConfigCallback(float pCurrentValue)
        {
            BerserkerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Berserker trait to '{BerserkerRate}'%");
        }

        public static void HawkEyeSliderConfigCallback(float pCurrentValue)
        {
            HawkEyeRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Hawk Eye trait to '{HawkEyeRate}'%");
        }

        public static void TitanSliderConfigCallback(float pCurrentValue)
        {
            TitanRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Titan trait to '{TitanRate}'%");
        }

        public static void TitanShifterSliderConfigCallback(float pCurrentValue)
        {
            TitanShifterRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Titan Shifter trait to '{TitanShifterRate}'%");
        }

        public static void WiseOldOneSliderConfigCallback(float pCurrentValue)
        {
            WiseOldOneRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Wise Old One trait to '{WiseOldOneRate}'%");
        }

        public static void IdiotSliderConfigCallback(float pCurrentValue)
        {
            IdiotRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Idiot trait to '{IdiotRate}'%");
        }

        public static void MountaintSliderConfigCallback(float pCurrentValue)
        {
            MountaintRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Mountaint trait to '{MountaintRate}'%");
        }

        public static void AlmightySliderConfigCallback(float pCurrentValue)
        {
            AlmightyRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Almighty trait to '{AlmightyRate}'%");
        }

        public static void ThorSliderConfigCallback(float pCurrentValue)
        {
            ThorRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Thor trait to '{ThorRate}'%");
        }

        public static void NightCrawlerSliderConfigCallback(float pCurrentValue)
        {
            NightCrawlerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Night Crawler trait to '{NightCrawlerRate}'%");
        }

        public static void ShieldGuySliderConfigCallback(float pCurrentValue)
        {
            ShieldGuyRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Shield Guy trait to '{ShieldGuyRate}'%");
        }

        public static void WolfTamerSliderConfigCallback(float pCurrentValue)
        {
            WolfTamerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Wolf Tamer trait to '{WolfTamerRate}'%");
        }

        public static void BearTamerSliderConfigCallback(float pCurrentValue)
        {
            BearTamerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Bear Tamer trait to '{BearTamerRate}'%");
        }

        public static void DragonTrainerSliderConfigCallback(float pCurrentValue)
        {
            DragonTrainerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Dragon Trainer trait to '{DragonTrainerRate}'%");
        }

        public static void MedicSliderConfigCallback(float pCurrentValue)
        {
            MedicRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Medic trait to '{MedicRate}'%");
        }

        public static void GangsterSliderConfigCallback(float pCurrentValue)
        {
            GangsterRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Gangster trait to '{GangsterRate}'%");
        }

        public static void MagisterSliderConfigCallback(float pCurrentValue)
        {
            MagisterRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Magister trait to '{MagisterRate}'%");
        }

        public static void WololoSliderConfigCallback(float pCurrentValue)
        {
            WololoRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Wololo trait to '{WololoRate}'%");
        }

        public static void AntManSliderConfigCallback(float pCurrentValue)
        {
            AntManRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Ant Man trait to '{AntManRate}'%");
        }

        public static void CommanderSliderConfigCallback(float pCurrentValue)
        {
            CommanderRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Commander trait to '{CommanderRate}'%");
        }

        public static void BloodWolfSliderConfigCallback(float pCurrentValue)
        {
            BloodWolfRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Blood Wolf trait to '{BloodWolfRate}'%");
        }

        public static void StuffedSliderConfigCallback(float pCurrentValue)
        {
            StuffedRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Stuffed trait to '{StuffedRate}'%");
        }

        public static void HungerSliderConfigCallback(float pCurrentValue)
        {
            HungerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Hunger trait to '{HungerRate}'%");
        }

        public static void DuplikateSliderConfigCallback(float pCurrentValue)
        {
            DuplikateRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Duplikate trait to '{DuplikateRate}'%");
        }

        public static void PhoenixSliderConfigCallback(float pCurrentValue)
        {
            PhoenixRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Phoenix trait to '{PhoenixRate}'%");
        }

        public static void PowerMimicrySliderConfigCallback(float pCurrentValue)
        {
            PowerMimicryRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Power Mimicry trait to '{PowerMimicryRate}'%");
        }

        public static void NullifySliderConfigCallback(float pCurrentValue)
        {
            NullifyRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Nullify trait to '{NullifyRate}'%");
        }

        public static void ReviverOfDeathSliderConfigCallback(float pCurrentValue)
        {
            ReviverOfDeathRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Reviver of Death trait to '{ReviverOfDeathRate}'%");
        }

        public static void DarkNecromancerSliderConfigCallback(float pCurrentValue)
        {
            DarkNecromancerRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Dark Necromancer trait to '{DarkNecromancerRate}'%");
        }

        public static void VampireSliderConfigCallback(float pCurrentValue)
        {
            VampireRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Vampire trait to '{VampireRate}'%");
        }

        public static void MirrorManSliderConfigCallback(float pCurrentValue)
        {
            MirrorManRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Mirror Man trait to '{MirrorManRate}'%");
        }

        public static void TimeStopperSliderConfigCallback(float pCurrentValue)
        {
            TimeStopperRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Time Stopper trait to '{TimeStopperRate}'%");
        }

        public static void ElectroSliderConfigCallback(float pCurrentValue)
        {
            ElectroRate = Convert.ToInt32(pCurrentValue);
            DarkieTraitsMain.LogInfo($"Set birth rate of Electro trait to '{ElectroRate}'%");
        }

    }
}
