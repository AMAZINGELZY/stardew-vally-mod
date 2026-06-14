using StardewValley.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EnhancedSchoolsMod.UI
{
    /// <summary>
    /// Simple menu for selecting which school type to enroll a child in
    /// </summary>
    public class SchoolSelectionMenu : IClickableMenu
    {
        private List<SchoolOption> schoolOptions;
        private int selectedIndex = 0;
        private const int OPTION_HEIGHT = 60;
        private const int OPTION_WIDTH = 300;

        public SchoolSelectionMenu(List<SchoolOption> options) : base(0, 0, Game1.viewport.Width, Game1.viewport.Height, true)
        {
            this.schoolOptions = options;
            this.width = 600;
            this.height = 400 + (options.Count * OPTION_HEIGHT);
            this.xPositionOnScreen = (Game1.viewport.Width - this.width) / 2;
            this.yPositionOnScreen = (Game1.viewport.Height - this.height) / 2;
        }

        public override void draw(SpriteBatch b)
        {
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);

            // Draw menu background
            drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), 
                this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, Color.White, 4f);

            // Draw title
            Utility.drawBoldText(b, "Select School", Game1.smallFont, 
                new Vector2(this.xPositionOnScreen + 20, this.yPositionOnScreen + 20), Color.Black);

            // Draw school options
            for (int i = 0; i < this.schoolOptions.Count; i++)
            {
                DrawSchoolOption(b, i);
            }

            this.drawMouse(b);
        }

        private void DrawSchoolOption(SpriteBatch b, int index)
        {
            SchoolOption option = this.schoolOptions[index];
            int yPosition = this.yPositionOnScreen + 70 + (index * OPTION_HEIGHT);
            bool isSelected = index == this.selectedIndex;

            // Draw background
            Color bgColor = isSelected ? new Color(200, 220, 255) : Color.White;
            drawTextureBox(b, Game1.mouseCursors, new Rectangle(403, 383, 6, 6),
                this.xPositionOnScreen + 20, yPosition, OPTION_WIDTH, OPTION_HEIGHT - 10, bgColor, 1f);

            // Draw school name
            Utility.drawBoldText(b, option.Name, Game1.smallFont,
                new Vector2(this.xPositionOnScreen + 30, yPosition + 10), Color.Black);

            // Draw cost
            string costText = "Cost: " + option.Cost + "g";
            b.DrawString(Game1.smallFont, costText, 
                new Vector2(this.xPositionOnScreen + 30, yPosition + 30), Color.DarkGray);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            for (int i = 0; i < this.schoolOptions.Count; i++)
            {
                int yPosition = this.yPositionOnScreen + 70 + (i * OPTION_HEIGHT);
                Rectangle optionBounds = new Rectangle(this.xPositionOnScreen + 20, yPosition, OPTION_WIDTH, OPTION_HEIGHT - 10);

                if (optionBounds.Contains(x, y))
                {
                    this.selectedIndex = i;
                    if (playSound)
                        Game1.playSound("select");
                    return;
                }
            }
        }

        public override void receiveKeyPress(Keys key)
        {
            if (key == Keys.Up)
            {
                this.selectedIndex = (this.selectedIndex - 1 + this.schoolOptions.Count) % this.schoolOptions.Count;
                Game1.playSound("select");
            }
            else if (key == Keys.Down)
            {
                this.selectedIndex = (this.selectedIndex + 1) % this.schoolOptions.Count;
                Game1.playSound("select");
            }
            else if (key == Keys.Enter)
            {
                this.exitThisMenu();
            }
            else if (key == Keys.Escape)
            {
                this.exitThisMenu();
            }
        }

        public SchoolOption GetSelectedSchool()
        {
            return this.schoolOptions[this.selectedIndex];
        }
    }

    /// <summary>
    /// Represents a school option in the selection menu
    /// </summary>
    public class SchoolOption
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string SchoolType { get; set; }

        public SchoolOption(string name, int cost, string schoolType)
        {
            this.Name = name;
            this.Cost = cost;
            this.SchoolType = schoolType;
        }
    }
}
