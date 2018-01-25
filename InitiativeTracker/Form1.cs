using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InitiativeTracker
{
    public partial class Form1 : Form
    {
        public static List<Actor> actors;
        Actor temp;

        public Form1()
        {
            InitializeComponent();
            actors = new List<Actor>();
            //masterPanel.Visible = false;
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            nameBox.SelectAll();
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (nameBox.Text.Length > 0)
                    {
                        if (initiativeBox.Text.Length > 0)
                        {
                            try
                            {
                                temp = new Actor(nameBox.Text, Convert.ToInt32(initiativeBox.Text));
                                actors.Add(temp);
                                nameBox.Text = "";
                                initiativeBox.Text = "";
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            initiativeBox.Focus();
                        }
                    }
                    sortList();
                    displayList();
                    break;
            }
        }

        private void initiativeBox_Enter(object sender, EventArgs e)
        {
            initiativeBox.SelectAll();
        }

        private void initiativeBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (nameBox.Text.Length > 0 && initiativeBox.Text.Length > 0)
                    {
                        try
                        {
                            temp = new Actor(nameBox.Text, Convert.ToInt32(initiativeBox.Text));
                            actors.Add(temp);
                            nameBox.Text = "";
                            initiativeBox.Text = "";
                            nameBox.Focus();
                        }
                        catch (Exception ex) { }
                    }
                    sortList();
                    displayList();
                    break;
            }
        }

        private void removeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (removeBox.Text.Length > 0)
                {
                    try
                    {
                        actors.RemoveAt(Convert.ToInt32(removeBox.Text) - 1);
                        removeBox.Text = "";
                    }
                    catch (Exception ex) { }
                    displayList();
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (nameBox.Text.Length > 0)
            {
                if (initiativeBox.Text.Length > 0)
                {
                    try
                    {
                        temp = new Actor(nameBox.Text, Convert.ToInt32(initiativeBox.Text));
                        actors.Add(temp);
                        nameBox.Text = "";
                        initiativeBox.Text = "";
                        nameBox.Focus();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    initiativeBox.Focus();
                }
            }
            sortList();
            displayList();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (removeBox.Text.Length > 0)
            {
                try
                {
                    actors.RemoveAt(Convert.ToInt32(removeBox.Text) - 1);
                    removeBox.Text = "";
                }
                catch (Exception ex) { }
                displayList();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            actors.Clear();
            removeBox.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void sortList()
        {
            for (int x = 0; x < actors.Count() - 1; x++)
            {
                for (int y = 0; y < actors.Count() - x - 1; y++)
                {
                    if (actors[y].initiative < actors[y + 1].initiative)
                    {
                        Actor temp = actors[y];
                        actors[y] = actors[y + 1];
                        actors[y + 1] = temp;
                    }
                }
            }
        }

        private void displayList()
        {
            label1.Text = "";
            for (int x = 0; x < actors.Count(); x++)
            {
                label1.Text += "(" + (x + 1) + ") " + actors[x].name + " " + actors[x].initiative + "\n";
            }
        }

        private void addCharacter()
        {
            CharacterPanel cp = new CharacterPanel("The Hassell Family is Large", new Random().Next(1, 35), 45, masterPanel.Controls.Count);
            
            masterPanel.Controls.Add(cp);
        }

        private void masterPanel_DoubleClick(object sender, EventArgs e)
        {
            addCharacter();
        }
    }

    public struct Actor
    {
        public string name;
        public int initiative;

        public Actor(string name, int initiative)
        {
            this.name = name;
            this.initiative = initiative;
        }
    }
}