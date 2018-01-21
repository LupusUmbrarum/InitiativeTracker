using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitiativeTracker
{
    class CharacterPanel : Panel
    {
        public string name;
        public int initiative, health;

        private Label label;
        private TextBox healthBox;
        private Button upButton, downButton;

        public CharacterPanel(string name, int initiative, int health, int tag)
        {
            this.name = name;
            this.initiative = initiative;
            this.health = health;
            this.Size = new Size(25, 25);
            this.BackColor = Color.Aqua;
            this.Tag = tag;
            this.Size = new Size(25, 25);
            this.Location = new Point(0, tag * 30);
            this.Visible = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Dock = DockStyle.Top;
            label = new Label();
            label.Text = name;
            label.Font = new Font(label.Font.FontFamily, 12);
            label.Location = new Point(this.Location.X + 10, 1);
            Controls.Add(label);
        }

        public void updateLabel(string str)
        {
            label.Text = str + name;
        }
    }
}