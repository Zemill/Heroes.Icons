﻿using Heroes.Models;
using Heroes.Models.AbilityTalents;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace Heroes.Icons.Tests
{
    public class HeroesDataXmlTests : HeroesIconsBase
    {
        private readonly IHeroesData HeroesData;

        public HeroesDataXmlTests()
        {
            HeroesData = HeroesIcons.HeroesData(67985);
        }

        [Fact]
        public void HeroExistsTest()
        {
            Assert.True(HeroesData.HeroExists("Mephisto"));

            IHeroesData heroData67621 = HeroesIcons.HeroesData(67621);
            Assert.False(HeroesData.HeroExists("Mephisto"));

            IHeroesData heroDataOldSplit = HeroesIcons.HeroesData(56705);
            Assert.True(heroDataOldSplit.HeroExists("Abathur"));

            IHeroesData heroDataLastest = HeroesIcons.HeroesData();
            Assert.True(heroDataLastest.HeroExists("Mephisto"));
        }

        [Fact]
        public void TotalCountOfHeroesTest()
        {
            Assert.Equal(82, HeroesData.GetTotalAmountOfHeroes());
        }

        [Fact]
        public void ListOfHeroNamesTest()
        {
            List<string> heroesList = HeroesData.HeroNames().ToList();

            Assert.Equal(82, heroesList.Count);
            Assert.Contains("Anub'arak", heroesList);
            Assert.Contains("Valeera", heroesList);
        }

        [Fact]
        public void HeroNameFromUnitIdTest()
        {
            Assert.Equal("Valeera", HeroesData.HeroNameFromUnitId("HeroValeera"));
            Assert.Equal("Anub'arak", HeroesData.HeroNameFromUnitId("HeroAnubarak"));
        }

        [Fact]
        public void HeroNameFromShortNameTest()
        {
            Assert.Equal("Valeera", HeroesData.HeroNameFromShortName("Valeera"));
            Assert.Equal("Anub'arak", HeroesData.HeroNameFromShortName("Anubarak"));
        }

        [Fact]
        public void HeroNameFromAttributeIdTest()
        {
            Assert.Equal("Valeera", HeroesData.HeroNameFromAttributeId("VALE"));
            Assert.Equal("Anub'arak", HeroesData.HeroNameFromAttributeId("Anub"));
        }

        [Fact]
        public void GetHeroDataAbathurTest()
        {
            Hero hero = HeroesData.HeroData("Abathur");

            Assert.Equal("Abathur", hero.ShortName);
            Assert.Equal("Abathur", hero.Name);
            Assert.Equal("Abathur", hero.CHeroId);
            Assert.Equal("HeroAbathur", hero.CUnitId);
            Assert.Equal("Abat", hero.AttributeId);
            Assert.Equal("Very Hard", hero.Difficulty);
            Assert.Equal(HeroFranchise.Starcraft, hero.Franchise);
            Assert.Equal(0.75, hero.InnerRadius);
            Assert.Equal(0.75, hero.Radius);
            Assert.Equal(new DateTime(2014, 3, 13), hero.ReleaseDate);
            Assert.Equal(12.0, hero.Sight);
            Assert.Equal(4.3984, hero.Speed);
            Assert.Equal("Melee", hero.Type);
            Assert.Equal(HeroRarity.Legendary, hero.Rarity);
            Assert.Equal("A unique Hero that can manipulate the battle from anywhere on the map.", hero.Description.RawDescription);

            // portraits
            Assert.Equal("storm_ui_ingame_heroselect_btn_infestor.png", hero.HeroPortrait.HeroSelectPortraitFileName);
            Assert.Equal("storm_ui_ingame_hero_leaderboard_abathur.png", hero.HeroPortrait.LeaderboardPortraitFileName);
            Assert.Equal("storm_ui_ingame_hero_loadingscreen_abathur.png", hero.HeroPortrait.LoadingScreenPortraitFileName);
            Assert.Equal("storm_ui_ingame_partypanel_btn_abathur.png", hero.HeroPortrait.PartyPanelPortraitFileName);
            Assert.Equal("ui_targetportrait_hero_abathur.png", hero.HeroPortrait.TargetPortraitFileName);

            // life
            Assert.Equal(685, hero.Life.LifeMax);
            Assert.Equal(0.04, hero.Life.LifeScaling);
            Assert.Equal(1.4257, hero.Life.LifeRegenerationRate);
            Assert.Equal(0.04, hero.Life.LifeRegenerationRateScaling);

            // energy
            Assert.Equal(0, hero.Energy.EnergyMax);
            Assert.Equal(0, hero.Energy.EnergyRegenerationRate);

            // roles
            Assert.Equal("Specialist", hero.Roles[0]);
            Assert.Equal(1, hero.Roles.Count);

            // ratings
            Assert.Equal(9, hero.Ratings.Complexity);
            Assert.Equal(3, hero.Ratings.Damage);
            Assert.Equal(1, hero.Ratings.Survivability);
            Assert.Equal(7, hero.Ratings.Utility);

            // weapons
            Assert.Equal("HeroAbathur", hero.Weapons[0].WeaponNameId);
            Assert.Equal(1, hero.Weapons[0].Range);
            Assert.Equal(0.7, hero.Weapons[0].Period);
            Assert.Equal(26, hero.Weapons[0].Damage);
            Assert.Equal(0.04, hero.Weapons[0].DamageScaling);

            // abilities
            Ability firstAbility = hero.Abilities["AbathurSymbiote"];
            Assert.Equal("AbathurSymbiote", firstAbility.ReferenceNameId);
            Assert.Equal("Symbiote", firstAbility.Name);
            Assert.Equal("AbathurSymbiote", firstAbility.ShortTooltipNameId);
            Assert.Equal("AbathurSymbiote", firstAbility.FullTooltipNameId);
            Assert.Equal("storm_ui_icon_abathur_symbiote.png", firstAbility.IconFileName);
            Assert.Equal("Cooldown: 4 seconds", firstAbility.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Equal("Assist an ally and gain new abilities", firstAbility.Tooltip.ShortTooltip.RawDescription);
            Assert.Equal("Spawn and attach a Symbiote to a target ally or Structure. While active, Abathur controls the Symbiote, gaining access to new Abilities. The Symbiote is able to gain XP from nearby enemy deaths.", firstAbility.Tooltip.FullTooltip.RawDescription);
            Assert.Equal(AbilityType.Q, firstAbility.AbilityType);

            Ability secondAbility = hero.Abilities["AbathurToxicNest"];
            Assert.Equal(3, secondAbility.Tooltip.Charges.CountMax);
            Assert.Equal(1, secondAbility.Tooltip.Charges.CountUse);
            Assert.Equal(3, secondAbility.Tooltip.Charges.CountStart);
            Assert.Equal(0.0625, secondAbility.Tooltip.Charges.RecastCooldown);
            Assert.Null(secondAbility.Tooltip.Charges.IsHideCount);
            Assert.Equal(AbilityType.W, secondAbility.AbilityType);

            // talents
            Talent talent = hero.Talents["AbathurHeroicAbilityUltimateEvolution"];
            Assert.Equal("AbathurHeroicAbilityUltimateEvolution", talent.ReferenceNameId);
            Assert.Equal("Ultimate Evolution", talent.Name);
            Assert.Equal("AbathurUltimateEvolution", talent.ShortTooltipNameId);
            Assert.Equal("AbathurUltimateEvolution", talent.FullTooltipNameId);
            Assert.Equal("storm_ui_icon_abathur_ultimateevolution.png", talent.IconFileName);
            Assert.Equal("Cooldown: 70 seconds", talent.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Equal("Clone target allied Hero and control it", talent.Tooltip.ShortTooltip.RawDescription);
            Assert.Equal("Clone target allied Hero and control it for <c val=\"#TooltipNumbers\">20</c> seconds. Abathur has perfected the clone, granting it <c val=\"#TooltipNumbers\">20%</c> Spell Power, <c val=\"#TooltipNumbers\">20%</c> bonus Attack Damage, and <c val=\"#TooltipNumbers\">10%</c> bonus Movement Speed. Cannot use their Heroic Ability.", talent.Tooltip.FullTooltip.RawDescription);
            Assert.Equal(AbilityType.Heroic, talent.AbilityType);
            Assert.True(talent.IsActive);

            // hero units
            Unit heroUnit = hero.HeroUnits[0];

            Assert.Equal("AbathurSymbiote", heroUnit.ShortName);
            Assert.Equal("Symbiote", heroUnit.Name);
            Assert.Equal("AbathurSymbiote", heroUnit.CUnitId);
            Assert.Equal(0, heroUnit.InnerRadius);
            Assert.Equal(0, heroUnit.Radius);
            Assert.Equal(4.0, heroUnit.Sight);
            Assert.Equal(0.0117, heroUnit.Speed);
            Assert.Equal("Ranged", heroUnit.Type);
            Assert.Empty(heroUnit.Description.RawDescription);

            Ability heroUnitAbility = heroUnit.Abilities["AbathurSymbioteCarapace"];
            Assert.Equal("AbathurSymbioteCarapace", heroUnitAbility.ReferenceNameId);
            Assert.Equal("Carapace", heroUnitAbility.Name);
            Assert.Equal("AbathurSymbioteCarapace", heroUnitAbility.ShortTooltipNameId);
            Assert.Equal("AbathurSymbioteCarapace", heroUnitAbility.FullTooltipNameId);
            Assert.Equal("storm_ui_icon_abathur_carapace.png", heroUnitAbility.IconFileName);
            Assert.Equal("Cooldown: 12 seconds", heroUnitAbility.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Empty(heroUnitAbility.Tooltip.ShortTooltip.RawDescription);
            Assert.Equal("Shields the assisted ally for <c val=\"#TooltipNumbers\">157~~0.04~~</c>. Lasts for <c val=\"#TooltipNumbers\">8</c> seconds.", heroUnitAbility.Tooltip.FullTooltip.RawDescription);
            Assert.Equal(AbilityType.E, heroUnitAbility.AbilityType);
        }

        [Fact]
        public void GetHeroDataTychusTest()
        {
            Hero hero = HeroesData.HeroData("Tychus");

            // energy
            Assert.Equal(500, hero.Energy.EnergyMax);
            Assert.Equal(3, hero.Energy.EnergyRegenerationRate);
            Assert.Equal("Mana", hero.Energy.EnergyType);

            // roles
            Assert.Equal("Assassin", hero.Roles[0]);
            Assert.Equal(1, hero.Roles.Count);

            // abilities
            Ability ability = hero.Abilities["TychusRunAndGun"];
            Assert.Equal(1, ability.Tooltip.Charges.CountMax);
            Assert.Equal(1, ability.Tooltip.Charges.CountUse);
            Assert.Equal(1, ability.Tooltip.Charges.CountStart);
            Assert.Equal(0.5, ability.Tooltip.Charges.RecastCooldown);
            Assert.True(ability.Tooltip.Charges.IsHideCount);
            Assert.Equal(AbilityType.E, ability.AbilityType);

            ability = hero.GetAbility("TychusRunAndGun");
            Assert.True(ability.Tooltip.Charges.IsHideCount);
            Assert.Equal(AbilityType.E, ability.AbilityType);

            // talents
            Talent talent = hero.Talents["TychusMasteryRunandGunDash"];
            Assert.Equal("TychusMasteryRunandGunDash", talent.ReferenceNameId);
            Assert.Equal("Dash", talent.Name);
            Assert.Equal("TychusRunandGunDashTalent", talent.ShortTooltipNameId);
            Assert.Equal("TychusRunandGunDashTalent", talent.FullTooltipNameId);
            Assert.Equal("storm_ui_icon_tychus_runandgun_a.png", talent.IconFileName);
            Assert.Empty(talent.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Equal(AbilityType.E, talent.AbilityType);
            Assert.False(talent.IsActive);
            Assert.True(talent.IsQuest);

            talent = hero.GetTalent("TychusMasteryRunandGunDash");
            Assert.Empty(talent.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Equal(AbilityType.E, talent.AbilityType);

            talent = hero.GetTalent(string.Empty);
            Assert.Equal("No Pick", talent.Name);

            talent = hero.GetTalent("SomeTalent");
            Assert.Equal("SomeTalent", talent.Name);

            // subAbilities
            ability = hero.SubAbilities(AbilityTier.Basic).ToList().First();
            Assert.Equal("TychusOdinAnnihilate", ability.ReferenceNameId);
            Assert.Equal("Annihilate", ability.Name);
            Assert.Equal("TychusCommandeerOdinAnnihilate", ability.ShortTooltipNameId);
            Assert.Equal("TychusCommandeerOdinAnnihilate", ability.FullTooltipNameId);
            Assert.Equal("storm_ui_icon_tychus_annihilate.png", ability.IconFileName);
            Assert.Equal("Cooldown: 7 seconds", ability.Tooltip.Cooldown.CooldownTooltip.RawDescription);
            Assert.Equal(AbilityType.Q, ability.AbilityType);
        }

        [Fact]
        public void HeroDataExistsTests()
        {
            Assert.NotNull(HeroesData.HeroData("Kerrigan"));
            Assert.NotNull(HeroesData.HeroData("The Lost Vikings"));
        }

        [Fact]
        public void HeroDataLimitedDataTests()
        {
            Assert.Empty(HeroesData.HeroData("Ragnaros", includeAbilities: false).Abilities);
            Assert.Empty(HeroesData.HeroData("Ragnaros", includeTalents: false).Talents);
            Assert.Empty(HeroesData.HeroData("Ragnaros", additionalUnits: false).HeroUnits);
        }

        [Fact]
        public void MultipleHeroDataTests()
        {
            List<string> heroNames = new List<string>
            {
                "Ragnaros",
                "Valeera",
                "Chen",
            };

            List<Hero> heroes = HeroesData.HeroesData(heroNames).ToList();
            Assert.Equal(3, heroes.Count);
        }

        [Fact]
        public void HeroDataInMemoryDontLoadFilesTest()
        {
            Assert.NotNull(HeroesIcons.HeroesData(67985).HeroData("Abathur"));
        }

        [Fact]
        public void HeroDataLocalizationTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

            Hero hero = HeroesData.HeroData("Abathur");
            Assert.Equal("0,75", hero.InnerRadius.ToString());
            Assert.Equal("0,75", hero.Radius.ToString());
            Assert.Equal(new DateTime(2014, 3, 13), hero.ReleaseDate);

            Assert.Equal(685, hero.Life.LifeMax);
            Assert.Equal("0,04", hero.Life.LifeScaling.ToString());
            Assert.Equal("1,4257", hero.Life.LifeRegenerationRate.ToString());
            Assert.Equal("0,04", hero.Life.LifeRegenerationRateScaling.ToString());

            // energy
            Assert.Equal(0, hero.Energy.EnergyMax);
            Assert.Equal(0, hero.Energy.EnergyRegenerationRate);

            // roles
            Assert.Equal("Specialist", hero.Roles[0]);
            Assert.Equal(1, hero.Roles.Count);

            // ratings
            Assert.Equal(9, hero.Ratings.Complexity);
            Assert.Equal(3, hero.Ratings.Damage);
            Assert.Equal(1, hero.Ratings.Survivability);
            Assert.Equal(7, hero.Ratings.Utility);

            // weapons
            Assert.Equal("HeroAbathur", hero.Weapons[0].WeaponNameId);
            Assert.Equal("0,7", hero.Weapons[0].Period.ToString());
            Assert.Equal("26", hero.Weapons[0].Damage.ToString());
        }
    }
}
