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
        public static List<CharacterPanel> characters;

        public Form1()
        {
            InitializeComponent();
            characters = new List<CharacterPanel>();
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
                                insertCharacter();
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
                            insertCharacter();
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
                        //actors.RemoveAt(Convert.ToInt32(removeBox.Text) - 1);
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
                        insertCharacter();
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
                    //actors.RemoveAt(Convert.ToInt32(removeBox.Text) - 1);
                    removeBox.Text = "";
                }
                catch (Exception ex) { }
                displayList();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            masterPanel.Controls.Clear();
            characters.Clear();
            //actors.Clear();
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
            /*
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
            */
        }

        private void displayList()
        {
            /*
            label1.Text = "";
            for (int x = 0; x < actors.Count(); x++)
            {
                label1.Text += "(" + (x + 1) + ") " + actors[x].name + " " + actors[x].initiative + "\n";
            }*/
        }

        private void addCharacter()
        {
            CharacterPanel cp = new CharacterPanel("The Hassell Family is Large", new Random().Next(1, 35), 45, masterPanel.Controls.Count, this);
            
            masterPanel.Controls.Add(cp);
            characters.Add(cp);
        }

        private void insertCharacter()
        {
            //create new character
            CharacterPanel tempCP = new CharacterPanel(nameBox.Text, Convert.ToInt32(initiativeBox.Text), 0, masterPanel.Controls.Count+1, this);

            //determine where to place character
            if(characters.Count > 0)
            {
                if(characters[0].initiative > tempCP.initiative)
                {
                    characters.Insert(0, tempCP);
                }
                else if(characters[characters.Count-1].initiative <= tempCP.initiative)
                {
                    characters.Add(tempCP);
                }
                else
                {
                    for (int x = 0; x < characters.Count; x++)
                    {
                        if (tempCP.initiative < characters[x].initiative)
                        {
                            characters.Insert(x, tempCP);
                            break;
                        }
                    }
                }
            }
            else
            {
                characters.Add(tempCP);
            }

            displayCharacterPanels();
        }

        private void displayCharacterPanels()//TODO try to optimize redrawing of panels
        {
            masterPanel.Controls.Clear();
            for (int x = 0; x < characters.Count; x++)
            {
                characters[x].updateLocation(x);
                masterPanel.Controls.Add(characters[x]);
            }
            masterPanel.Invalidate();
        }

        public void moveUp(CharacterPanel cp)
        {
            int index = characters.IndexOf(cp);
            MessageBox.Show(index.ToString());
            try
            {
                CharacterPanel temp = characters[index + 1];
                characters[index] = temp;
                characters[index + 1] = cp;
                displayCharacterPanels();
            }
            catch (Exception ex) { System.Media.SystemSounds.Beep.Play(); MessageBox.Show("error up" + ex.Message + index); }
        }

        public void moveDown(CharacterPanel cp)
        {
            int index = characters.IndexOf(cp);
            try
            {
                CharacterPanel temp = characters[index - 1];
                characters[index] = temp;
                characters[index - 1] = cp;
                displayCharacterPanels();
            }
            catch (Exception ex) { System.Media.SystemSounds.Beep.Play(); MessageBox.Show("error down " + ex.Message + index); }
        }

        public void removeCharacterPanel(CharacterPanel cp)
        {
            masterPanel.Controls.Remove(cp);
            characters.Remove(cp);
            displayCharacterPanels();
        }

        private void masterPanel_DoubleClick(object sender, EventArgs e)
        {
            //insertCharacter();//addCharacter();
            displayCharacterPanels();
            
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