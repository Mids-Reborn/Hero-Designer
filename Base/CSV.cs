﻿using System;
using System.Text.RegularExpressions;

// Token: 0x02000023 RID: 35
public static class CSV
{

    public static string[] ToArray(string iLine)
    {
        string[] strArray = CSV.Reg.Split(iLine);
        char[] chArray = new char[]
        {
            '"'
        };
        for (int index = 0; index < strArray.Length; index++)
        {
            strArray[index] = strArray[index].Trim(chArray);
        }
        return strArray;
    }


    private static readonly Regex Reg = new Regex(",(?=(?:[^\"]|\"[^\"]*\")*$)", RegexOptions.CultureInvariant);


    internal enum HPower
    {

        PowerId,

        DisplayName,

        Available,

        Requires,

        ModesRequired,

        ModesDisallowed,

        Type,

        Accuracy,

        AttackTypes,

        GroupMembership,

        AIGroups,

        EntsAffected,

        EntsAutohit,

        Target,

        TargetVisibility,

        TimeToConfirm,

        DisplayConfirm,

        Range,

        TargetSecondary,

        RangeSecondary,

        EnduranceCost,

        IdeaCost,

        InterruptTime,

        CastTime,

        RechargeTime,

        ActivatePeriod,

        EffectArea,

        Radius,

        Arc,

        MaxTargetsHit,

        MaxBoosts,

        Misc,

        AIreport,

        NumCharges,

        UsageTime,

        Lifetime,

        LifetimeInGame,

        NumAllowed,

        DoNotSave,

        BoostsAllowed,

        AnimMainTargetOnly,

        CastThroughHold,

        CastThroughSleep,

        CastThroughStun,

        CastThroughTerrorize,

        IgnoreStrength,

        MouseOverText,

        HelpText,

        AttackerHitMessage,

        VictimHitMessage,

        IconName,

        ActivateRequires,

        SlotRequires,

        TargetRequires,

        RewardRequires,

        AuctionRequires,

        RewardFallback,

        DisplayAttackerAttackFloater,

        ShowBuffIcon,

        ShowInInventory,

        ShowInManage,

        ShowInInfo,

        Deleteable,

        Tradeable,

        BoostIgnoresEffectiveness,

        BoostAlwaysCountForSet,

        BoostTradeable,

        BoostCombinable,

        BoostAccountBound,

        BoostBoostable,

        BoostUsePlayerLevel,

        BoostCatalystConversion,

        BoostLicenseLevel,

        MinSlotLevel,

        MaxSlotLevel,

        MaxBoostLevel,

        StrengthsDisallowed,

        ProcMainTargetOnly,

        HighlightEval,

        HighlightRingRed,

        HighlightRingGreen,

        HighlightRingBlue,

        HighlightRingAlpha,

        ChainIntoPower,

        InstanceLocked,

        PowerRedirector,

        Cancelable,

        IgnoreToggleMaxDistance,

        ToggleIgnoreHold,

        ToggleIgnoreSleep,

        ToggleIgnoreStun,

        IgnoreLevelBought,

        ShootThroughUntouchable,

        InterruptLikeSleep
    }


    internal enum HEffect
    {

        PowerID,

        Table,

        Aspect,

        Attrib,

        Target,

        Scale,

        Type,

        AllowStrength,

        AllowResistance,

        Suppress,

        CancelEvents,

        AllowCombatMobs,

        CostumeName,

        EntityDef,

        PriorityList,

        Delay,

        Duration,

        Magnitude,

        StackType,

        Period,

        Chance,

        NearGround,

        CancelOnMiss,

        VanishOnTimeout,

        RadiusInner,

        RadiusOuter,

        Requires,

        MagnitudeExpr,

        DurationExpr,

        Reward,

        IgnoreSuppressErrors,

        DisplayFloat,

        DisplayAttackerHit,

        DisplayVictimHit,

        Order,

        ShowFloaters,

        ModeName,

        EffectId,

        BoostIgnoreDiminishing,

        BoostTemplate,

        PrimaryStringList,

        SecondaryStringList,

        ApplicationType,

        UseMagnitudeResistance,

        UseDurationResistance,

        UseMagnitudeCombatMods,

        UseDurationCombatMods,

        CasterStackType,

        StackLimit,

        ContinuingFX,

        ConditionalFX,

        Params,

        DisplayOnlyIfNotZero,

        MatchExactPower,

        DoNotTint,

        KeepThroughDeath,

        DelayEval,

        BoostModAllowed,

        EvalFlags,

        ProcsPerMinute
    }


    internal enum BoostSet
    {

        Id,

        DisplayName,

        GroupName,

        MinLevel,

        MaxLevel
    }
}
