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
        private SaveFileDialog sfd;
        private OpenFileDialog ofd;

        public Form1()
        {
            InitializeComponent();
            nameBox.Focus();
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
                                e.Handled = e.SuppressKeyPress = true;
                                insertCharacter();
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            e.Handled = e.SuppressKeyPress = true;
                            initiativeBox.Focus();
                        }
                    }
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
                    if (initiativeBox.Text.Length > 0)
                    {
                        if(nameBox.Text.Length > 0)
                        {
                            try
                            {
                                e.Handled = e.SuppressKeyPress = true;
                                insertCharacter();
                            }
                            catch (Exception ex) { }
                        }
                        else
                        {
                            e.Handled = e.SuppressKeyPress = true;
                            nameBox.Focus();
                        }
                    }
                    break;
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
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    initiativeBox.Focus();
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            masterPanel.Controls.Clear();
            characters.Clear();
            nameBox.Focus();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ofd == null)
            {
                ofd = new OpenFileDialog();
                ofd.FileOk += ofd_FileOk;
                ofd.Filter = "Tracked Initiatives (TI) | *.TI";
                ofd.RestoreDirectory = true;
            }
            ofd.ShowDialog();
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            string[] lines = {};
            try
            {
                lines = System.IO.File.ReadAllLines(ofd.FileName);
            }
            catch (Exception ex) { }
            
            if(lines.Length > 0)
            {
                string[] chars;
                for (int x = 0; x < lines.Length; x++)
                {
                    chars = lines[x].Split('^');
                    insertCharacter(Convert.ToInt32(chars[0]), chars[1], Convert.ToInt32(chars[2]));
                }
            }
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sfd == null)
            {
                sfd = new SaveFileDialog();
                sfd.FileOk += sfd_FileOk;
                sfd.Filter = "Tracked Initiatives (TI) | *.TI";
                sfd.RestoreDirectory = true;
            }
            sfd.ShowDialog();
        }

        void sfd_FileOk(object sender, CancelEventArgs e)
        {
            List<string> panels = new List<string>();

            foreach(CharacterPanel x in characters)
            {
                panels.Add(x.getSaveData());
            }

            try
            {
                System.IO.File.WriteAllLines(sfd.FileName, panels.ToArray<string>());
            }
            catch (Exception ex) { }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void insertCharacter(int initiative = 0, string name = "", int health = 0)
        {
            CharacterPanel tempCP;
            //create new characterpanel
            if(name != "")
            {
                tempCP = new CharacterPanel(name, initiative, health, masterPanel.Controls.Count + 1, this);
            }
            else
            {
                nameBox.Text += " ";
                tempCP = new CharacterPanel(nameBox.Text, Convert.ToInt32(initiativeBox.Text), 0, masterPanel.Controls.Count + 1, this);
            }

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

            nameBox.Text = "";
            initiativeBox.Text = "";
            nameBox.Focus();

            masterPanel.Controls.Add(tempCP);
            masterPanel2.Controls.Add(tempCP);

            displayCharacterPanels();
        }

        //I'm trying a sort of screen-swap 'technique'. Kinda helps, kinda doesn't
        private void displayCharacterPanels()//TODO try to optimize redrawing of panels
        {
            if(masterPanel.Visible)
            {
                masterPanel2.Controls.Clear();
                for (int x = 0; x < characters.Count; x++)
                {
                    masterPanel2.Controls.Add(characters[x]);
                }
                masterPanel2.Invalidate();
                masterPanel2.Visible = true;
                masterPanel.Visible = false;
            }
            else
            {
                masterPanel.Controls.Clear();
                for (int x = 0; x < characters.Count; x++)
                {
                    masterPanel.Controls.Add(characters[x]);
                    masterPanel.Controls[x].Location = new Point(0, x * 35);
                }
                masterPanel.Invalidate();
                masterPanel.Visible = true;
                masterPanel2.Visible = false;
            }
        }

        public void moveUp(CharacterPanel cp)
        {
            int index = characters.IndexOf(cp);
            try
            {
                CharacterPanel temp = characters[index + 1];
                characters[index] = temp;
                characters[index + 1] = cp;
                displayCharacterPanels();
            }
            catch (Exception ex) { System.Media.SystemSounds.Beep.Play(); }
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
            catch (Exception ex) { System.Media.SystemSounds.Beep.Play(); }
        }

        public void removeCharacterPanel(CharacterPanel cp)
        {
            masterPanel.Controls.Remove(cp);
            characters.Remove(cp);
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