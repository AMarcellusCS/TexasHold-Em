using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TexasHoldEm
{
    public partial class TexasHoldem : Form
    {
        PictureBox[] cardBox = new PictureBox[13];
        int[] cardVal = new int[13];
        Random rnd = new Random();
        int[] players = new int[4];
        int count = 0;
        public TexasHoldem()
        {
            InitializeComponent();
            nGame();
        }
        public void nGame()
        {
            cardBox[0] = pictureBox1;
            cardBox[1] = pictureBox2;
            cardBox[2] = pictureBox3;
            cardBox[3] = pictureBox4;
            cardBox[4] = pictureBox5;
            cardBox[5] = playerCardOne;
            cardBox[6] = playerCard2;
            cardBox[7] = comp11;
            cardBox[8] = comp12;
            cardBox[9] = comp21;
            cardBox[10] = comp22;
            cardBox[11] = comp31;
            cardBox[12] = comp32;

            cardVal[5] = randomCard(cardBox[5]);
            cardVal[6] = randomCard(cardBox[6]);
            cardVal[7] = randomCard(cardBox[7]);
            cardVal[9] = randomCard(cardBox[9]);
            cardVal[11] = randomCard(cardBox[11]);

            chooseCard(cardBox[0], "204");
            chooseCard(cardBox[1], "204");
            chooseCard(cardBox[2], "204");
            chooseCard(cardBox[3], "204");
            chooseCard(cardBox[4], "204");
         
            chooseCard(cardBox[8], "203");
            chooseCard(cardBox[10], "202");
            chooseCard(cardBox[12], "201");

            count = 0;

            playerLabel.Text = "";
            comp1.Text = "";
            comp2.Text = "";
            comp3.Text = "";
            hitButton.Text = "HIT";

            int[] temp = {cardVal[5], cardVal[6]};
            playerLabel.Text = getString(check(temp));
            int[] temp2 = { cardVal[7] };
            comp1.Text = getString(check(temp2));
            int[] temp3 = { cardVal[9] };
            comp2.Text = getString(check(temp3));
            int[] temp4 = { cardVal[11] };
            comp3.Text = getString(check(temp4));


        }

        private void playerCardOne_Click(object sender, EventArgs e)
        {

        }

        private void playerCard2_Click(object sender, EventArgs e)
        {

        }

        private void comp11_Click(object sender, EventArgs e)
        {

        }

        private void comp12_Click(object sender, EventArgs e)
        {

        }

        private void comp21_Click(object sender, EventArgs e)
        {

        }

        private void comp22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        private int getValue(int card)
        {
            card %= 13;
            if (card == 0)
                return 13;
            if (card == 1)
                return 14;
            return card;
        }
        public int randomCard(PictureBox picBox)
        {
            System.Resources.ResourceManager rm = TexasHoldEm.Properties.Resources.ResourceManager;
            int card = rnd.Next(1, 53);
            String cardName = "" + card;
            Bitmap myImage = (Bitmap)rm.GetObject(cardName);
            picBox.Image = myImage;
            return card;
        }
        public void chooseCard(PictureBox picBox, string card)
        {
            System.Resources.ResourceManager rm = TexasHoldEm.Properties.Resources.ResourceManager;
            Bitmap myImage = (Bitmap)rm.GetObject(card);
            picBox.Image = myImage;
        }
        private int check(int[] x)
        {
            if (checkRF(x))
            {
                return 23;
            }
            else if (checkSF(x))
            {
                return 22;
            }
            else if (checkFK(x))
            {
                return 21;
            }
            else if (checkFH(x))
            {
                return 20;
            }
            else if (checkF(x))
            {
                return 19;
            }
            else if (checkS(x))
            {
                return 18;
            }
            else if (checkTK(x))
            {
                return 17;
            }
            else if (checkTP(x))
            {
                return 16;
            }
            else if (checkOP(x))
            {
                return 15;
            }
            else
            {
                return highest(x);
            }
        }

        private int highest(int[] v)
        {
            int highest = 0;
            for (int x = 0; x < v.Length; x++)
            {
                if (getValue(v[x]) > highest)
                    highest = getValue(v[x]);              
            }
            return highest;
        }

        private int[] createArr(int x, int y)
        {
            int[] temp = new int[7];
            for (int z = 0; z < 5; z++)
                temp[z] = cardVal[z];
            temp[5] = x;
            temp[6] = y;
            Array.Sort(temp);
            return temp;
        }

        private void removeDuplicates(int[] temp)
        {
            int[] next = new int[temp.Length];
            int count = 0;
            for (int x = 0; x < temp.Length; x++)
            {
                bool unique = true;
                for (int y = x+1; y < temp.Length; y++)
                {
                    if (x!=y && getValue(temp[x]) == getValue(temp[y]))
                    {
                        unique = false;
                    }
                } 
                if (unique)
                {
                    next[count] = temp[x];
                    count++;
                }             
            }

            count = 0;

            for (int x = 0; x < next.Length; x++)
            {
                if (next[x] != 0)
                {
                    count++;
                }
            }

            int[] final = new int[count];
            for (int x = 0; x < final.Length; x++)
            {
                final[x] = next[x];
            }
            temp = final;
        }

        private bool checkRF(int[] temp)
        {
            removeDuplicates(temp);
            if (temp.Length < 5)
                return false;
            bool over = true;
            for (int x = temp.Length%5; x < temp.Length; x++)
            {
                if (getValue(temp[x]) < 10)
                    over = false;
            }
            if (over && checkS(temp) && checkF(temp))
                return true;
            return false;
        }

        private bool checkSF(int[] v)
        {
            if (checkS(v) && checkF(v))
                return true;
            return false;
        }

        private bool checkFK(int[] v)
        {
            for (int x = 0; x < v.Length; x++)
            {
                int count = 1;
                for (int y = 0; y < v.Length; y++)
                {
                    if (x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        count++;
                        if (count == 4)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool checkFH(int[] v)
        {
            bool three = false;
            int valofthree = 0;
            bool two = false;
            for (int x = 0; x < v.Length; x++)
            {
                int count = 1;
                for (int y = 0; y < v.Length; y++)
                {
                    if (x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        count++;
                        if (count == 3) { 
                            valofthree = getValue(v[x]);
                            three = true;
                        }
                    }
                }
            }
            for (int x = 0; x < v.Length; x++)
            {
                int count = 1;
                for (int y = 0; y < v.Length; y++)
                {
                    if (getValue(v[x]) != valofthree && x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        count++;
                        if (count == 2)
                            two = true;
                    }
                }
            }
            return (three && two);
        }

        private bool checkF(int[] v)
        {
            int count;
            for (int x = 0; x < v.Length; x++)
            {
                count = 1;
                for (int y = 0; y < v.Length; y++) {
                        if (x != y && getSuit(v[x]) == getSuit(v[y]))
                        {
                            count++;
                            if (count == 5)
                                return true;
                        }
                }
            }
            return false;
        }

        private int getSuit(int v)
        {
            if (v >= 1 && v <= 13)
                return 1;
            else if (v >= 14 && v <= 26)
                return 2;
            else if (v >= 27 && v <= 39)
                return 3;
            else
                return 4;
        }

        private bool checkS(int[] v)
        {
            removeDuplicates(v);
            if (v.Length < 5)
                return false;
            int count = 1;
            for (int x = 0; x < v.Length-1; x++)
            {
                if (getValue(v[x]) + 1 == getValue(v[x + 1]))
                    count++;
                if (count == 5)
                    return true;
                else
                    count = 1;
            }
            return false;
        }
        private bool checkTK(int[] v)
        {
            for (int x = 0; x < v.Length; x++)
            {
                int count = 1;
                for (int y = 0; y < v.Length; y++)
                {
                    if (x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        count++;
                        if (count == 3)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool checkTP(int[] v)
        {
            bool three = false;
            int valofthree = 0;
            bool two = false;
            for (int x = 0; x < v.Length; x++)
            {
                for (int y = 0; y < v.Length; y++)
                {
                    if (x != y && getValue(v[x]) == getValue(v[y]))
                    {
                            valofthree = getValue(v[x]);
                            three = true;
                    }
                }
            }
            for (int x = 0; x < v.Length; x++)
            {
                for (int y = 0; y < v.Length; y++)
                {
                    if (getValue(v[x]) != valofthree && x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        two = true;
                    }
                }
            }
            return (three && two);
        }

        private bool checkOP(int[] v)
        {
            for (int x = 0; x < v.Length; x++)
            {
                int count = 1;
                for (int y = 0; y < v.Length; y++)
                {
                    if (x != y && getValue(v[x]) == getValue(v[y]))
                    {
                        count++;
                        if (count == 2)
                            return true;
                    }
                }
            }
            return false;
        }

        private void playerLabel_Click(object sender, EventArgs e)
        {

        }

        private void hitButton_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                cardVal[0] = randomCard(cardBox[0]);
                cardVal[1] = randomCard(cardBox[1]);
                cardVal[2] = randomCard(cardBox[2]);
                int[] temp = {cardVal[0], cardVal[1], cardVal[2], cardVal[5], cardVal[6]};
                playerLabel.Text = getString(check(temp));
                int[] temp2 = { cardVal[0], cardVal[1], cardVal[2], cardVal[7] };
                comp1.Text = getString(check(temp2));
                int[] temp3 = { cardVal[0], cardVal[1], cardVal[2], cardVal[9] };
                comp2.Text = getString(check(temp3));
                int[] temp4 = { cardVal[0], cardVal[1], cardVal[2], cardVal[11] };
                comp3.Text = getString(check(temp4));
                count++;
            }
            else if (count == 1)
            {
                cardVal[3] = randomCard(cardBox[3]);
                int[] temp = { cardVal[0], cardVal[1], cardVal[2],cardVal[3], cardVal[5], cardVal[6] };
                playerLabel.Text = getString(check(temp));
                int[] temp2 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[7] };
                comp1.Text = getString(check(temp2));
                int[] temp3 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[9] };
                comp2.Text = getString(check(temp3));
                int[] temp4 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[11] };
                comp3.Text = getString(check(temp4));
                count++;
            }
            else if (count == 2)
            {
                cardVal[4] = randomCard(cardBox[4]);
                int[] temp = {cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[4], cardVal[5], cardVal[6]};
                playerLabel.Text = getString(check(temp));
                int[] temp2 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[4], cardVal[7] };
                comp1.Text = getString(check(temp2));
                int[] temp3 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[4], cardVal[9] };
                comp2.Text = getString(check(temp3));
                int[] temp4 = { cardVal[0], cardVal[1], cardVal[2], cardVal[3], cardVal[4], cardVal[11] };
                comp3.Text = getString(check(temp4));
                hitButton.Text = "CALL";
                count++;
            }
            else if (count == 3)
            {
                cardVal[8] = randomCard(cardBox[8]);
                cardVal[10] = randomCard(cardBox[10]);
                cardVal[12] = randomCard(cardBox[12]);
                playerLabel.Text = getString(check(createArr(cardVal[5], cardVal[6])));
                comp1.Text = getString(check(createArr(cardVal[7], cardVal[8])));
                comp2.Text = getString(check(createArr(cardVal[9], cardVal[10])));
                comp3.Text = getString(check(createArr(cardVal[11], cardVal[12])));

                hitButton.Text = "New Game?";
                count++;
            }
            else if (count > 3)
            {
                nGame();
            }  
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            
        }

        private string getString(int v)
        {
            if (v == 23)
            
                return "Royal Flush";
            
            else if (v == 22)
            
                return "Straight Flush";
            
            else if (v == 21)

                return "Four of a kind";
            
            else if (v == 20)

                return "Full House";
            
            else if (v == 19)

                return "Flush";
            
            else if (v == 18)

                return "Straight";
            
            else if (v == 17)

                return "Three of a kind";
            
            else if (v == 16)

                return "Two pair";
            
            else if (v == 15)

                return "One pair";
            
            else
            {
                if (v == 14)
                    return "Ace high.";
                else if (v == 13)
                    return "King high.";
                else if (v == 12)
                    return "Queen high.";
                else if (v == 11)
                    return "Jack high.";
                else
                    return v + " high.";
            }
        }

        private void comp1_Click(object sender, EventArgs e)
        {

        }

        private void comp2_Click(object sender, EventArgs e)
        {

        }

        private void comp3_Click(object sender, EventArgs e)
        {

        }
    }
}
