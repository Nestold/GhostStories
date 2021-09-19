using System;

public class Consts
{
    public static float GetTimeMultiplier(EMapType mapType)
    {
        switch(mapType)
        {
            case EMapType.KellyStreet:
            case EMapType.NevilleStreet:
                return 1.2f;

            case EMapType.MainStreet:
            case EMapType.CecilStreet:
                return .6f;

            default:
                return 6f;
        }
    }

    public static int MaxAnomalyCount()
    {
        return 4;
    }

    public static int GetAnomalyCooldown()
    {
        return UnityEngine.Random.Range(10, 25);
    }

    public static int GetReportingTime()
    {
        return 6;
    }

    public static string[] GetDialog(int index)
    {
        switch (index)
        {
            case 0:
                return new string[]
                {
                    "Test Main Menu Text",
                    "Test Main Menu Text",
                    "Test Main Menu Text",
                };

            case 1:
                return new string[]
                {
                    "To begin with, I would like to apologize to you for not be with you at your first day at work.",
                    "However, don't worry, I will guide your training through this message.",
                    "Generally, our company is concerned with the observation of strange phenomena that occur in customers' homes.",
                    "Your task with the CCTV program is to observe such phenomena and report them through the 'Aberra' program.",
                    "Kelly Street is your assignment today. Hope the rules aren't too hard.",
                    "Good luck my friend."
                };

            case 2:
                return new string[]
                {
                    "Welcome to Neville Street."
                };

            case 3:
                return new string[]
                {
                    "Welcome to Main Street."
                };

            case 4:
                return new string[]
                {
                    "Welcome to Cecil Street."
                };

            default:
                return new string[]
            {
                    "Dialog Not Found",
                    "Dialog Not Found",
                    "Dialog Not Found",
            };
        }
    }

    public static string GetRoomName(ERoomType roomType)
    {
        switch (roomType)
        {
            default:
                return roomType.ToString();
        }
    }

    public static string[] GetRoomNames()
    {
        return new string[]
        {
            "<None>",
            "Living Room",
            "Bedroom",
            "Bedroom(2)",
            "Kitchen",
            "Batchroom",
            "Batchroom(2)",
            "Entrence"
        };
    }

    public static string[] GetAberrationsNames()
    {
        return new string[]
        {
            "<None>",
            "Object Disapear",
            "Object Movement",
            "Additional Object",
            "Broken Camera",
            "Obstructed View",
        };
    }
}