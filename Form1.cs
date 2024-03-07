/*
Author: Osiame Moloro
Date: 02 May 2023
Purpose: Programmer source code
 */ 

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game
{
    public partial class Form1 : Form
    {
        // All the fields to be used.
        string word;
        int index;
        string asterisk = "";
        StringBuilder sbWord;
        StringBuilder sbAsterisk;
        // The attepmtsLeft is set to 10
        int attemptsLeft = 10;

        public Form1()
        {
            InitializeComponent();
            // Sets KeyPreview to true to allow the form to receive all key events.
            this.KeyPreview = true;
            
        }

        // Letters function that will accept a key as parameter.
        void Letters(char keyPress)
        {
            // Rreplaces the key pressed characters to upper case
            string charKey = keyPress.ToString().ToUpper();
            keyPress = Char.Parse(charKey);

            // An exeption is handled
            try
            {
                // Check whether the key pressed is contained in the word and returns a bool value.
                bool check = word.Contains(keyPress);
                if (check == true)
                {
                    // Initiallized a new StingBuilder using the word string.
                    sbWord = new StringBuilder(word);
                    // Iterates through the characters in the word and changes them to the words pressed.
                    for (int loop = 0; loop < sbWord.Length; loop++)
                    {
                        if (sbWord[loop] == keyPress)
                        {
                            sbAsterisk[loop] = sbWord[loop];
                        }
                        this.textBox1.Text = sbAsterisk.ToString();
                        this.textBox1.Show();
                    }
                }
                else
                {
                    // decrements the attemptsLeft by 1 if the character pressed is not contained int the word.
                    attemptsLeft--;
                    // Adds the wrong charater to the wrongAttempts list box.
                    wrongAttempts.Items.Add(keyPress.ToString());
                }

            }
            // An exeption is catched if a user tried to press a key before starting the game.
            catch (ArgumentNullException)
            {
                // A message box is dislayed to the form as an error message.
                MessageBox.Show("Please start first", "Hangman", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // An event handler for the key press event on the from.
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Checks whether the key pressed is a letter from A-Z and returns a bool value.
            bool alphaChecker = e.KeyChar.ToString().All(Char.IsLetter);

            // Checks the alphaChecker variable and invokes the Letters Method it is true.
            if (alphaChecker == true)
            {
                // Invokes the previous function.
                // Puts the character pressed in the pressedKey parameter.
                Letters(e.KeyChar);
            }

            if (sbWord == null)
            {
                this.lblAttempts.Text = $"Attempts Left: {attemptsLeft}";
            }
            else
            {
                // Checks whether all the keys pressed equal to the characters in the word and performs specified functions.
                if (sbAsterisk.ToString() == sbWord.ToString())
                {
                    lblWinner.Text = "Winner";
                    Score.Items.Add($"{word} {attemptsLeft}");
                    wrongAttempts.Items.Clear();
                    this.KeyPreview = false;

                }
                else if (attemptsLeft > 0)
                {
                    lblAttempts.Text = $"Attempts Left: {attemptsLeft.ToString()}";
                }
                // Checks the attemtsLeft and if it equals to 0 it will peform specified functions.
                else if (attemptsLeft == 0)
                {
                    lblAttempts.Text = $"Attempts Left: {attemptsLeft.ToString()}";
                    lblWinner.Text = "Game Over";
                    this.KeyPreview = false;
                }
            }

        }

        // The load event handler of the form.
        private void Form1_Load(object sender, EventArgs e)
        {
            // All the controls are hidden when the from is loaded.
            this.textBox1.Hide();
            this.wrongAttempts.Hide();
            this.Score.Hide();
            label1.Hide();
            label2.Hide();
            this.lblAttempts.Hide();
        }

        // The start button event hander that will do the specified actions when the user presses it.
        private void btnStart_Click(object sender, EventArgs e)
        {
            // The KeyPreview is set to true and all the controls that are needed to display information are shown.
            this.KeyPreview = true;
            this.wrongAttempts.Show();
            this.lblAttempts.Show();
            this.Score.Show();
            this.label1.Show();
            this.label2.Show();
            
            // All the words used in the game are stored in an ArrayList
            ArrayList Words = new ArrayList();
            {
                Words.Add("Beautiful");
                Words.Add("Doctor");
                Words.Add("House");
                Words.Add("Separate");
                Words.Add("Movie");
                Words.Add("Drive");
                Words.Add("Country");
                Words.Add("Mountain");
                Words.Add("Sleep");
                Words.Add("Queen");
                Words.Add("School");
                Words.Add("Pleasent");
            }

            lblWelcom.Hide();
            // The attemptsLeft variable is initiallized to 10.
            this.attemptsLeft = 10;
            lblAttempts.Text = $"Attempts Left: {attemptsLeft.ToString()}";

            // The random class is intantiated and given functionality as specified.
            Random random = new Random();
            index = random.Next(0, Words.Count - 1);
            word = Words[index].ToString().ToUpper();

            this.btnStart.Text = "New Game";
            // The words are changed to astricks and passed to a string builder.
            asterisk = new String('*', word.Length);
            sbAsterisk = new StringBuilder(asterisk);

            // Controls are cleared when the new game button is pressed.
            this.lblWinner.Text = null;
            this.Score.Items.Clear();
            this.wrongAttempts.Items.Clear();
            textBox1.Text = asterisk.ToString();
            this.textBox1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays a message box on the form when the quit button is pressed.
            DialogResult result = MessageBox.Show("Are you sure you want to quit the game?", "Hangman", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // The application will close if the user presses yes.
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
