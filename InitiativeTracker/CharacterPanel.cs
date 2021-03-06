﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitiativeTracker
{
    public class CharacterPanel : TableLayoutPanel //FlowLayoutPanel works, but has large spaces between each control
    {
        public string name;
        public int initiative, health;

        private Label nameLabel, orderLabel;
        private TextBox healthBox;
        private Button upButton, downButton, removeButton;
        public Form1 parent;

        public CharacterPanel(string name, int initiative, int health, int tag, Form1 parent)
        {
            this.parent = parent;
            this.ColumnCount = 6;
            this.name = name;
            this.initiative = initiative;
            this.health = health;
            this.BackColor = Color.Aqua;
            this.Tag = tag;
            this.Size = new Size(100, 30);
            this.Location = new Point(0, tag * 35);
            this.Visible = true;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Dock = DockStyle.Top;

            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 23f));

            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));

            orderLabel = new Label();
            updateOrder(initiative);
            orderLabel.Font = new Font(orderLabel.Font.FontFamily, 12);
            Controls.Add(orderLabel);

            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 225f));

            nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Font = new Font(nameLabel.Font.FontFamily, 12);
            nameLabel.Size = new Size(name.Length * 10, 20);
            nameLabel.Location = new Point(nameLabel.Location.X, 5);
            Controls.Add(nameLabel);

            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize, 125f));

            healthBox = new TextBox();
            healthBox.TextChanged += healthBox_TextChanged;
            healthBox.Text += health;
            healthBox.Size = new Size(50, 30);
            Controls.Add(healthBox);

            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize, 100F));

            upButton = new Button();
            upButton.Click += upButton_Click;
            upButton.BackgroundImage = InitiativeTracker.Properties.Resources.Up_Arrow_PNG_Picture;
            upButton.BackgroundImageLayout = ImageLayout.Zoom;
            upButton.Anchor = AnchorStyles.Right;
            upButton.MaximumSize = new Size(30, 20);
            Controls.Add(upButton);

            downButton = new Button();
            downButton.Click += downButton_Click;
            downButton.BackgroundImage = InitiativeTracker.Properties.Resources.arrow_down_01_512;
            downButton.BackgroundImageLayout = ImageLayout.Zoom;
            downButton.Anchor = AnchorStyles.Right;
            downButton.MaximumSize = new Size(30, 20);
            Controls.Add(downButton);

            removeButton = new Button();
            removeButton.Click += removeButton_Click;
            removeButton.BackgroundImage = InitiativeTracker.Properties.Resources.x;
            removeButton.BackgroundImageLayout = ImageLayout.Zoom;
            removeButton.Anchor = AnchorStyles.Right;
            removeButton.Size = new Size(20, 20);
            Controls.Add(removeButton);
        }

        void healthBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                health = Convert.ToInt32(healthBox.Text);
            }
            catch (Exception ex) { }
        }

        public void updateName(string str)
        {
            name = str;
            nameLabel.Text = name;
            nameLabel.Size = new Size(name.Length * 10, 25);
        }

        public void updateOrder(int num)
        {
            initiative = num;
            orderLabel.Text = "(" + num.ToString() + ")";
        }

        public void updateLocation(int num)
        {
            this.Location = new Point(0, num * 35);
        }

        void upButton_Click(object sender, EventArgs e)
        {
            parent.moveUp(this);
        }

        void downButton_Click(object sender, EventArgs e)
        {
            parent.moveDown(this);
        }

        void removeButton_Click(object sender, EventArgs e)
        {
            parent.removeCharacterPanel(this);
        }

        public string getSaveData()
        {
            return this.initiative.ToString() + "^" + this.name + "^" + this.health.ToString() + "^" + this.Tag.ToString();
        }
    }
}