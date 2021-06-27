using SB.CoreTest;
using System.Collections.Generic;

/// <summary>
/// SchoolsBuddy Technical Test.
///
/// Your task is to find the highest floor of the building from which it is safe
/// to drop a marble without the marble breaking, and to do so using the fewest
/// number of marbles. You can break marbles in the process of finding the answer.
///
/// The method Building.DropMarble should be used to carry out a marble drop. It
/// returns a boolean indicating whether the marble dropped without breaking.
/// Use Building.NumberFloors for the total number of floors in the building.
///
/// A very basic solution has already been implemented but it is up to you to
/// find your own, more efficient solution.
///
/// Please use the function Attempt2 for your answer.
/// </summary>
namespace SB.TechnicalTest
{
    public class BuildingExtended : Building
    {
        private static int StartFloor { get; set; }
        private static int TopFloor { get; set; }
        public static Dictionary<int, bool?> FloorList { get; set; }

        /// <summary>
        /// This method will construct a building for testing purposes.
        /// </summary>
        /// <param name="topFloor"></param>
        public static void ConstructBuilding(int topFloor = 100)
        {
            StartFloor = 0;
            TopFloor = topFloor;
            BuildingExtended.FloorList = new Dictionary<int, bool?>();

            //there is no point to go under 0 as we assume the impact level is the ground level (floor 0)
            for (int i = 0; i <= topFloor; i++)
            {
                BuildingExtended.FloorList.Add(i, null);
            }
        }

        /// <summary>
        ///  Find the highest safe floor to drop the marble without to break it.  
        /// </summary>
        /// <remarks>
        ///  If you don't construct the building running the ConstructBuilding() method before calling this method, a 100 floors building will be constructed by default.
        /// </remarks>
        /// <returns> Returns the lowest safe floor number</returns>
        public static int FindHighestSafeFloor()
        {
            if (FloorList == null) ConstructBuilding();
            return FindHighestSafeFloor(StartFloor, TopFloor);
        }


        /// <summary>
        ///  Find the highest safe floor to drop the marble without to break it. This method is to allow special tests, if you don't use that option you can run the FindHighestSafeFloor() instead
        /// </summary>
        /// <param name="lowestFTT">The lowest floor to start testing</param>
        /// <param name="maxFTT">The top floor to test</param>
        /// <remarks>
        ///  If you don't construct the building running the ConstructBuilding() method before calling this method, a 100 floors building will be constructed by default.
        /// </remarks>
        /// <returns> Returns the lowest safe floor number</returns>
        public static int FindHighestSafeFloor(int lowestFTT = 1, int maxFTT = 100)
        {
            if (FloorList == null) ConstructBuilding(maxFTT);
            FloorList[lowestFTT] = FloorList[lowestFTT] != null ? (bool)FloorList[lowestFTT] : !Building.DropMarble(lowestFTT);

            if ((bool)FloorList[lowestFTT])
            {
                if (FloorList[lowestFTT - 1] == true) return lowestFTT - 1;
                // if you run a breaktest and set for example 75 as a test starting floor we still want to get the first safe floor so 
                // let's start testing the floors bellow 75 or we can return a not found instead, that's depending how we define this in specs.

                //there is no point to go under 0 as we assume the impact level is the ground level (floor 0)
                return FindHighestSafeFloor(0, lowestFTT - 1);
            }
            else
            {
                FloorList[lowestFTT] = true;
            }

            int middle = GetTheMiddleValue(lowestFTT, maxFTT);
            if (middle == lowestFTT) middle = lowestFTT + 1;
            FloorList[middle] = FloorList[middle] != null ? (bool)FloorList[middle] : !Building.DropMarble(middle);

            if ((bool)FloorList[middle])
            {
                if (FloorList[middle - 1] == true) return middle - 1;
                if (middle > lowestFTT + 1)
                {
                    return FindHighestSafeFloor(lowestFTT + 1, middle);
                }
                else
                {
                    return lowestFTT;
                }
            }
            else
            {
                if (maxFTT > middle + 1)
                {
                    return FindHighestSafeFloor(middle + 1, maxFTT);
                }
                else
                {
                    return middle;
                }
            }
        }

        static int GetTheMiddleValue(int min, int max)
        {
            return (min + max) / 2;
        }
    }

    
}
