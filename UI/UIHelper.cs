using StardewValley;
using System.Collections.Generic;

namespace EnhancedSchoolsMod.UI
{
    /// <summary>
    /// Helper class for managing UI and menu interactions
    /// </summary>
    public static class UIHelper
    {
        /// <summary>
        /// Opens the school selection menu
        /// </summary>
        public static SchoolSelectionMenu OpenSchoolSelectionMenu(List<SchoolOption> schoolOptions)
        {
            SchoolSelectionMenu menu = new SchoolSelectionMenu(schoolOptions);
            Game1.activeClickableMenu = menu;
            return menu;
        }

        /// <summary>
        /// Creates a list of school options from configuration
        /// </summary>
        public static List<SchoolOption> CreateSchoolOptionsFromConfig(Dictionary<string, int> schoolCosts)
        {
            List<SchoolOption> options = new List<SchoolOption>();

            foreach (var school in schoolCosts)
            {
                string schoolName = FormatSchoolName(school.Key);
                options.Add(new SchoolOption(schoolName, school.Value, school.Key));
            }

            return options;
        }

        /// <summary>
        /// Formats school type names for display
        /// </summary>
        private static string FormatSchoolName(string schoolType)
        {
            return schoolType switch
            {
                "PublicSchool" => "Public School",
                "BoardingSchool" => "Boarding School",
                "ArtsAcademy" => "Arts Academy",
                "SportsAcademy" => "Sports Academy",
                "MagicalAcademy" => "Magical Academy",
                _ => schoolType
            };
        }
    }
}
