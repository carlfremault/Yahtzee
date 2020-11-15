/**
 * Yahtzee dice game
 * author : Carl Fremault
 * date : 15/11/2020
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Yahtzee1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        #region variables
        // Variables
        Random rnd = new Random();
        int valueDice1, valueDice2, valueDice3, valueDice4, valueDice5;
        bool dice1Locked, dice2Locked, dice3Locked, dice4Locked, dice5Locked;
        int countOnes, countTwos, countThrees, countFours, countFives, countSixes;
        int valueOnes, valueTwos, valueThrees, valueFours, valueFives, valueSixes;
        int valueThreeOfAKind, valueFourOfAKind, valueFullHouse, valueSmallStraight, valueLargeStraight, valueChance;
        int valueYahtzee = 0;
        int valueSubTotalUS = 0, valueBonusUS = 0, valueTotalUS = 0;
        int valueLS = 0, valueGrandTotal = 0;
        bool threeOfAKind, fourOfAKind, fullHouse, smallStraight, largeStraight, yahtzee;
        List<int> values = new List<int>();
        int roll;
        #endregion

        #region Links
        private void lklDelaPouite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://delapouite.com");
        }

        private void lklSbed_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://opengameart.org/content/95-game-icons");
        }

        private void lklGameIcons_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://game-icons.net/");
        }

        private void lklCarlFremault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.carlfremault.com/");
        }
        #endregion

        /// <summary>
        /// Event click on help button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Classic Yahtzee rules." + "\n" + "You can score up to two Yahtzees." + "\n" + "Bonus applied if Upper Section score of 63 or higher." + "\n" + "\n" + "Click a die if you wish to prevent it from re-rolling." + "\n" + "Click a scorebox to claim a score." + "\n" + "\n" + "Have fun!","Info");
        }

        /// <summary>
        /// Event click Restart button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplay_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        /// <summary>
        /// Game Initialization 
        /// </summary>
        public void InitializeGame()
        {

            btnRoll.Enabled = true;
            btnThrows.Text = "0/3";
            roll = 0;
            InitializeGameDice();
            InitializeScores();
            btnRoll.Focus();
        }

        /// <summary>
        /// Dice Initialization at the start of the game
        /// </summary>
        private void InitializeGameDice()
        {
            pcbDice1.Image = Properties.Resources._0;
            pcbDice2.Image = Properties.Resources._0;
            pcbDice3.Image = Properties.Resources._0;
            pcbDice4.Image = Properties.Resources._0;
            pcbDice5.Image = Properties.Resources._0;
            pnlDice1.BackColor = Color.White;
            pnlDice2.BackColor = Color.White;
            pnlDice3.BackColor = Color.White;
            pnlDice4.BackColor = Color.White;
            pnlDice5.BackColor = Color.White;
            valueDice1 = 0;
            valueDice2 = 0;
            valueDice3 = 0;
            valueDice4 = 0;
            valueDice5 = 0;
            dice1Locked = false;
            dice2Locked = false;
            dice3Locked = false;
            dice4Locked = false;
            dice5Locked = false;
        }

        /// <summary>
        /// Dice Initialization at the start of a turn
        /// </summary>
        private void ResetDice()
        {
            pnlDice1.BackColor = Color.White;
            pnlDice2.BackColor = Color.White;
            pnlDice3.BackColor = Color.White;
            pnlDice4.BackColor = Color.White;
            pnlDice5.BackColor = Color.White;
            valueDice1 = 0;
            valueDice2 = 0;
            valueDice3 = 0;
            valueDice4 = 0;
            valueDice5 = 0;
            dice1Locked = false;
            dice2Locked = false;
            dice3Locked = false;
            dice4Locked = false;
            dice5Locked = false;
        }
        
        /// <summary>
        /// Scorecard Initialization
        /// </summary>
        private void InitializeScores()
        {
            btnAces.Text = "";
            btnAces.BackColor = Color.White;
            btnTwos.Text = "";
            btnTwos.BackColor = Color.White;
            btnThrees.Text = "";
            btnThrees.BackColor = Color.White;
            btnFours.Text = "";
            btnFours.BackColor = Color.White;
            btnFives.Text = "";
            btnFives.BackColor = Color.White;
            btnSixes.Text = "";
            btnSixes.BackColor = Color.White;
            btnThreeOfAKind.Text = "";
            btnThreeOfAKind.BackColor = Color.White;
            btnFourOfAKind.Text = "";
            btnFourOfAKind.BackColor = Color.White;
            btnFullHouse.Text = "";
            btnFullHouse.BackColor = Color.White;
            btnSmallStraight.Text = "";
            btnSmallStraight.BackColor = Color.White;
            btnLargeStraight.Text = "";
            btnLargeStraight.BackColor = Color.White;
            btnYahtzee.Text = "";
            btnYahtzee.BackColor = Color.White;
            btnChance.Text = "";
            btnChance.BackColor = Color.White;
            LockScoreCard();
            countTotals();
        }

        /// <summary>
        /// Lock Scorecard
        /// </summary>
        private void LockScoreCard()
        {
            btnAces.Enabled = false;
            btnTwos.Enabled = false;
            btnThrees.Enabled = false;
            btnFours.Enabled = false;
            btnFives.Enabled = false;
            btnSixes.Enabled = false;
            btnThreeOfAKind.Enabled = false;
            btnFourOfAKind.Enabled = false;
            btnFullHouse.Enabled = false;
            btnSmallStraight.Enabled = false;
            btnLargeStraight.Enabled = false;
            btnYahtzee.Enabled = false;
            btnChance.Enabled = false;
        }

        /// <summary>
        /// Calculate Totals
        /// </summary>
        private void countTotals()
        {
            // Calculate totals Upper Section including conditional bonus
            valueSubTotalUS = valueOnes + valueTwos + valueThrees + valueFours + valueFives + valueSixes;
            if (valueSubTotalUS >= 63)
            {
                valueBonusUS = 35;
            }
            valueTotalUS = valueSubTotalUS + valueBonusUS;
            // Write values in textboxes
            txtSubTotalUS.Text = valueSubTotalUS.ToString();
            txtBonusUS.Text = valueBonusUS.ToString();
            txtTotalUS.Text = valueTotalUS.ToString();
            // Calculate Totals Lower Section
            valueLS = valueThreeOfAKind + valueFourOfAKind + valueFullHouse + valueSmallStraight + valueLargeStraight + valueYahtzee + valueChance;
            valueGrandTotal = valueLS + valueTotalUS;
            // Write values in textboxes
            txtUS.Text = valueTotalUS.ToString();
            txtLS.Text = valueLS.ToString();
            txtGrandTotal.Text = valueGrandTotal.ToString();

        }

        #region Lock dice
        /// <summary>
        /// Methods to lock dice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbDice1_Click(object sender, EventArgs e)
        {
            if (roll != 0)
            {
                if (dice1Locked)
                {
                    pnlDice1.BackColor = Color.White;
                    dice1Locked = false;
                }
                else
                {
                    pnlDice1.BackColor = Color.LimeGreen;
                    dice1Locked = true;
                }
            }
        }
        private void pcbDice2_Click(object sender, EventArgs e)
        {
            if (roll != 0)
            {
                if (dice2Locked)
                {
                    pnlDice2.BackColor = Color.White;
                    dice2Locked = false;
                }
                else
                {
                    pnlDice2.BackColor = Color.LimeGreen;
                    dice2Locked = true;
                }
            }
        }
        private void pcbDice3_Click(object sender, EventArgs e)
        {
            if (roll != 0)
            {
                if (dice3Locked)
                {
                    pnlDice3.BackColor = Color.White;
                    dice3Locked = false;
                }
                else
                {
                    pnlDice3.BackColor = Color.LimeGreen;
                    dice3Locked = true;
                }
            }
        }
        private void pcbDice4_Click(object sender, EventArgs e)
        {
            if (roll != 0)
            {
                if (dice4Locked)
                {
                    pnlDice4.BackColor = Color.White;
                    dice4Locked = false;
                }
                else
                {
                    pnlDice4.BackColor = Color.LimeGreen;
                    dice4Locked = true;
                }
            }
        }
        private void pcbDice5_Click(object sender, EventArgs e)
        {
            if (roll != 0)
            {
                if (dice5Locked)
                {
                    pnlDice5.BackColor = Color.White;
                    dice5Locked = false;
                }
                else
                {
                    pnlDice5.BackColor = Color.LimeGreen;
                    dice5Locked = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// Method add dice values to list
        /// </summary>
        private void addValuestoList()
        {
            values.Clear();
            values.Add(valueDice1);
            values.Add(valueDice2);
            values.Add(valueDice3);
            values.Add(valueDice4);
            values.Add(valueDice5);
        }

        /// <summary>
        /// Prep to roll dice
        /// </summary>
        private void nextRoll()
        {
            countTotals();
            ResetDice();
            LockScoreCard();
            if (btnAces.Text == "" || btnTwos.Text == "" || btnThrees.Text == "" || btnFours.Text == "" || btnFives.Text == "" || btnSixes.Text == "" || btnThreeOfAKind.Text == "" || btnFourOfAKind.Text == "" || btnFullHouse.Text == "" || btnSmallStraight.Text == "" || btnLargeStraight.Text == "" || btnYahtzee.Text == "" || btnChance.Text == "")
            {
                roll = 0;
                btnThrows.Text = roll.ToString() + "/3";
                btnRoll.Enabled = true;
                btnRoll.Focus();
            }
            else
            {
                btnRoll.Enabled = false;
                btnReplay.Focus();
            }

        }

        #region click roll
        /// <summary>
        /// Event click Roll button
        /// Generates random value for each unlocked die
        /// Blocks Roll button once three rolls have been made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoll_Click(object sender, EventArgs e)
        {
            LockScoreCard();
            if (roll == 0)
            {
                ResetDice();
            }
            roll++;
            btnThrows.Text = roll.ToString()+"/3";
            if (!dice1Locked)
            {
                valueDice1 = rnd.Next(1, 7);
                pcbDice1.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + valueDice1);
            }
            if (!dice2Locked)
            {
                valueDice2 = rnd.Next(1, 7);
                pcbDice2.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + valueDice2);
            }
            if (!dice3Locked)
            {
                valueDice3 = rnd.Next(1, 7);
                pcbDice3.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + valueDice3);
            }
            if (!dice4Locked)
            {
                valueDice4 = rnd.Next(1, 7);
                pcbDice4.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + valueDice4);
            }
            if (!dice5Locked)
            {
                valueDice5 = rnd.Next(1, 7);
                pcbDice5.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + valueDice5);
            }
            if (roll >= 3)
            {
                btnRoll.Enabled = false;
            }
            addValuestoList();
            checkValues();
            checkResults();
        }
        #endregion

        #region check values count
        /// <summary>
        /// Methods to verify occurce of each number
        /// </summary>
        private void checkValues()
        {
            // Check Ones
            countOnes = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 1)
                {
                    countOnes++;
                }
            }
            // Check Twos
            countTwos = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 2)
                {
                    countTwos++;
                }
            }
            // Check Threes
            countThrees = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 3)
                {
                    countThrees++;
                }
            }
            // Check Fours
            countFours = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 4)
                {
                    countFours++;
                }
            }
            // Check Fives
            countFives = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 5)
                {
                    countFives++;
                }
            }
            // Check Sixes
            countSixes = 0;
            for (int k = 0; k < values.Count(); k++)
            {
                if (values[k] == 6)
                {
                    countSixes++;
                }
            }
        }
        #endregion

        #region check results
        /// <summary>
        /// Methods to verify and unlock each available scorecard field
        /// </summary>
        private void checkResults()
        {
            // Check and enable ones twos threes fours fives sixes
            if (btnAces.Text == "")
            {
                btnAces.Enabled = true;
            }
            if (btnTwos.Text == "")
            {
                btnTwos.Enabled = true;
            }
            if (btnThrees.Text == "")
            {
                btnThrees.Enabled = true;
            }
            if (btnFours.Text == "")
            {
                btnFours.Enabled = true;
            }
            if (btnFives.Text == "")
            {
                btnFives.Enabled = true;
            }
            if (btnSixes.Text == "")
            {
                btnSixes.Enabled = true;
            }
            // Check and enable chance
            if (btnChance.Text == "")
            {
                btnChance.Enabled = true;
            }
            // Check and enable three of a kind
            if (btnThreeOfAKind.Text == "")
            {
                threeOfAKind = false;
                if (countOnes >= 3 || countTwos >= 3 || countThrees >= 3 || countFours >= 3 || countFives >= 3 || countSixes >= 3)
                {
                    threeOfAKind = true;
                }
                btnThreeOfAKind.Enabled = true;
            }
            // Check and enable four of a kind
            if (btnFourOfAKind.Text == "")
            {
                fourOfAKind = false;
                if (countOnes >= 4 || countTwos >= 4 || countThrees >= 4 || countFours >= 4 || countFives >= 4 || countSixes >= 4)
                {
                    fourOfAKind = true;
                }
                btnFourOfAKind.Enabled = true;
            }
            // Check and enable yahtzee
            if (btnYahtzee.Text == "" || btnYahtzee.Text =="50")
            {
                yahtzee = false;
                if (countOnes >= 5 || countTwos >= 5 || countThrees >= 5 || countFours >= 5 || countFives >= 5 || countSixes >= 5)
                {
                    yahtzee = true;
                }
                btnYahtzee.Enabled = true;
            }
            // Check and enable Full House
            if (btnFullHouse.Text == "")
            {
                fullHouse = false;
                if ((countOnes == 3 || countTwos == 3 || countThrees == 3 || countFours == 3 || countFives == 3 || countSixes == 3) && (countOnes == 2 || countTwos == 2 || countThrees == 2 || countFours == 2 || countFives == 2 || countSixes == 2))
                {
                    fullHouse = true;
                }
                btnFullHouse.Enabled = true;
            }
            // Check and enable Small Straight
            if (btnSmallStraight.Text == "")
            {
                smallStraight = false;
                // Small Straight 1-2-3-4
                if (countOnes >= 1 && countTwos >= 1 && countThrees >= 1 && countFours >= 1)
                {
                    smallStraight = true;
                }
                // Small Straight 2-3-4-5
                if (countTwos >= 1 && countThrees >= 1 && countFours >= 1 && countFives >=1)
                {
                    smallStraight = true;
                }
                // Small Straight 3-4-5-6
                if (countThrees >= 1 && countFours >= 1 && countFives >= 1 && countSixes >=1)
                {
                    smallStraight = true;
                }
                btnSmallStraight.Enabled = true;
            }
            // Check and enable Large Straight

            if (btnLargeStraight.Text == "")
            {
                largeStraight = false;
                // Large Straight 1-2-3-4-5
                if (countOnes == 1 && countTwos == 1 && countThrees == 1 && countFours == 1 && countFives == 1)
                {
                    largeStraight = true;
                }
                // Large Straight 2-3-4-5-6
                if (countTwos == 1 && countThrees == 1 && countFours == 1 && countFives == 1 && countSixes == 1)
                {
                    largeStraight = true;
                }
                btnLargeStraight.Enabled = true;
                }
        }
        #endregion

        #region scorecard click
        /// <summary>
        /// Events 'on click' for each scorecard field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAces_Click(object sender, EventArgs e)
        {
            valueOnes = countOnes;
            btnAces.Text = valueOnes.ToString();
            nextRoll();
        }
        private void btnTwos_Click(object sender, EventArgs e)
        {
            valueTwos = countTwos * 2;
            btnTwos.Text = valueTwos.ToString();
            nextRoll();
        }
        private void btnThrees_Click(object sender, EventArgs e)
        {
            valueThrees = countThrees * 3;
            btnThrees.Text = valueThrees.ToString();
            nextRoll();
        }
        private void btnFours_Click(object sender, EventArgs e)
        {
            valueFours = countFours * 4;
            btnFours.Text = valueFours.ToString();
            nextRoll();
        }
        private void btnFives_Click(object sender, EventArgs e)
        {
            valueFives = countFives * 5;
            btnFives.Text = valueFives.ToString();
            nextRoll();
        }
        private void btnSixes_Click(object sender, EventArgs e)
        {
            valueSixes = countSixes * 6;
            btnSixes.Text = valueSixes.ToString();
            nextRoll();
        }
        private void btnThreeOfAKind_Click(object sender, EventArgs e)
        {
            if (threeOfAKind)
            {
                valueThreeOfAKind = valueDice1 + valueDice2 + valueDice3 + valueDice4 + valueDice5;
            }
            else
            {
                valueThreeOfAKind = 0;
            }
            btnThreeOfAKind.Text = valueThreeOfAKind.ToString();
            nextRoll();
        }
        private void btnFourOfAKind_Click(object sender, EventArgs e)
        {
            if (fourOfAKind)
            {
                valueFourOfAKind = valueDice1 + valueDice2 + valueDice3 + valueDice4 + valueDice5;
            }
            else
            {
                valueFourOfAKind = 0;
            }
            btnFourOfAKind.Text = valueFourOfAKind.ToString();
            nextRoll();
        }
        private void btnFullHouse_Click(object sender, EventArgs e)
        {
            if (fullHouse)
            {
                valueFullHouse = 25;
            }
            else
            {
                valueFullHouse = 0;
            }
            btnFullHouse.Text = valueFullHouse.ToString();
            nextRoll();
        }
        private void btnSmallStraight_Click(object sender, EventArgs e)
        {
            if (smallStraight)
            {
                valueSmallStraight = 30;
            }
            else
            {
                valueSmallStraight = 0;
            }
            btnSmallStraight.Text = valueSmallStraight.ToString();
            nextRoll();
        }
        private void btnLargeStraight_Click(object sender, EventArgs e)
        {
            if (largeStraight)
            {
                valueLargeStraight = 40;
            }
            else
            {
                valueLargeStraight = 0;
            }
            btnLargeStraight.Text = valueLargeStraight.ToString();
            nextRoll();
        }
        private void btnYahtzee_Click(object sender, EventArgs e)
        {
            if (yahtzee)
            {
                valueYahtzee += 50;
            }
            else
            {
                valueYahtzee = 0;
            }
            btnYahtzee.Text = valueYahtzee.ToString();
            nextRoll();
        }
        private void btnChance_Click(object sender, EventArgs e)
        {
            valueChance = valueDice1 + valueDice2 + valueDice3 + valueDice4 + valueDice5;
            btnChance.Text = valueChance.ToString();
            nextRoll();
        }
        #endregion
    }
}
