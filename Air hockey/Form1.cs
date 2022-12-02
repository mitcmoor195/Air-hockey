using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//A scuffed air hockey game
//Mitchell Moore
//Dec 1 2022
namespace Air_hockey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Rectangle player1 = new Rectangle(10, 100, 40, 40);
        Rectangle player1T = new Rectangle(10, 100, 40, 1);
        Rectangle player1L = new Rectangle(10, 100, 1, 40);
        Rectangle player1R = new Rectangle(50, 100, 1, 40);
        Rectangle player1B = new Rectangle(10, 140, 40, 1);

        Rectangle player2 = new Rectangle(10, 400, 40, 40);
        Rectangle player2T = new Rectangle(10, 400, 40, 1);
        Rectangle player2L = new Rectangle(10, 400, 1, 40);
        Rectangle player2R = new Rectangle(50, 400, 1, 40);
        Rectangle player2B = new Rectangle(10, 440, 40, 1);

        Rectangle ball = new Rectangle(150, 150, 15, 15);
        Rectangle middleLine = new Rectangle(0, 300, 600, 4);
        Rectangle player1Goal = new Rectangle(0, 0, 600, 4);
        Rectangle player2Goal = new Rectangle(0, 300, 600, 4);


        int player1Score = 0;
        int player2Score = 0;



        double bxs = 0.5;
        double bys = 0.5;

        Random random = new Random();

        int playerSpeed = 10;

        string player1YDirection = "";
        string player1XDirection = "";

        string player2YDirection = "";
        string player2XDirection = "";


        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;
        bool rDown = false;
        
        Pen bluePen = new Pen(Color.DodgerBlue);
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Crimson);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Lime);
        Pen whitePen = new Pen(Color.White, 4);
        SolidBrush greyBrush = new SolidBrush(Color.DarkGray);


        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            middleLine.Y = this.Height / 2;
            e.Graphics.FillRectangle(whiteBrush, middleLine);

            player1Goal.Width = this.Width / 3;
            player1Goal.X = this.Width / 3;
            e.Graphics.FillRectangle(greyBrush, player1Goal);

            player2Goal.Width = this.Width / 3;
            player2Goal.X = this.Width / 3;
            player2Goal.Y = this.Height - player2Goal.Height;
            e.Graphics.FillRectangle(greyBrush, player2Goal);

            e.Graphics.DrawEllipse(whitePen, 120, 225, 200, 200);
            e.Graphics.DrawRectangle(whitePen, this.Width / 4, 560, this.Width / 2, this.Width / 3);
            e.Graphics.DrawRectangle(whitePen, this.Width / 4, -60, this.Width / 2, this.Width / 3);


            e.Graphics.FillEllipse(blueBrush, player1.X, player1.Y, player1.Width, player1.Height);

            e.Graphics.FillEllipse(redBrush, player2.X, player2.Y, player2.Width, player2.Height);

            e.Graphics.FillEllipse(greenBrush, ball.X, ball.Y, ball.Width, ball.Height);




        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.R:
                    rDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    player1YDirection = "up";
                    break;
                case Keys.S:
                    sDown = true;
                    player1YDirection = "down";
                    break;
                case Keys.A:
                    aDown = true;
                    player1XDirection = "left";
                    break;
                case Keys.D:
                    dDown = true;
                    player1XDirection = "right";
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    player2YDirection = "up";
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    player2YDirection = "down";
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    player2XDirection = "left";
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    player2XDirection = "right";
                    break;
                case Keys.R:
                    rDown = true;
                    break;
            }
        }

        private void gameTick_Tick(object sender, EventArgs e)
        {
            

            //move ball 
            ball.X += Convert.ToInt16(bxs);
           ball.Y += Convert.ToInt16(bys);

            //find ball and player coordinates
            int player1x = player1.X;
            int player1y = player1.Y;

            int player2x = player2.X;
            int player2y = player2.Y;

            int ballx = ball.X;
            int bally = ball.Y;



            //move player 1 
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            else if (sDown == true && player1.Y < this.Height /2 - 40)
            {
                player1.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            else if (dDown == true && player1.X < this.Width - 40)
            {
                player1.X += playerSpeed;
            }

            //move player 2 
            if (upArrowDown == true && player2.Y > this.Height / 2 )
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height -40)
            {
                player2.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width -40)
            {
                player2.X += playerSpeed;
            }

            // draw player sides to correct spots
            player1T = new Rectangle(player1.X, player1.Y, 40, 1);
            player1L = new Rectangle(player1.X, player1.Y, 1, 40);
            player1R = new Rectangle (player1.X+40, player1.Y, 1, 40);
            player1B = new Rectangle(player1.X, player1.Y+40, 40, 1);

            player2T = new Rectangle(player2.X, player2.Y, 40, 1);
            player2L = new Rectangle(player2.X, player2.Y, 1, 40);
            player2R = new Rectangle(player2.X + 40, player2.Y, 1, 40);
            player2B = new Rectangle(player2.X, player2.Y + 40, 40, 1);

            



            //if puck intersects with left side of player 1
            if (ball.IntersectsWith(player1L))
            {
                // change direction if going opposite direction
                if (bxs > 0)
                {
                    bxs *= -1;
                }
                ///if same direction increase puck speed
                else if (bxs < 0)
                {
                    bxs -= 1.5;
                }
                // if ball standing still (at start of round)
                else if (bxs == 0)
                {
                    bxs = random.Next(-9, -4);

                }
                //if same direction increase puck speed
                if (player1XDirection == "left")
                {
                    bxs -= 0.5;
                }
                //if player moving down
                if (player1YDirection == "up" && bys > 0)
                {
                    bys *= -1;
                }
                //if same direction increase puck speed
                if (player1YDirection == "up" && bys < 0)
                {
                    bys -= 0.5;
                }
                //if player moving down
                if (player1YDirection == "down" && bys < 0)
                {
                    bys *= -1;
                }
                if (player1YDirection == "down" && bys > 0)
                {
                    bys += 0.5;
                }
                // if ball standing still (at start of round)
                if (player1YDirection == "down" && bys == 0)
                {
                    bys = random.Next(-9, -4); 
                }
                if (player1YDirection == "up" && bys == 0)
                {
                    bys = random.Next(4, 9);

                }
            }
            //if puck intersects with right side of player 1
            if (ball.IntersectsWith(player1R))
            {
                //if same direction increase puck speedn
                if (bxs > 0)
                {
                    bxs += 1.5;
                }
                // change direction if going opposite direction
                else if (bxs < 0)
                {
                    bxs *= -1;
                }
                // if ball standing still (at start of round)
                else if (bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                //if same direction increase puck speed
                if (player1XDirection == "right")
                {
                    bxs += 0.5;
                }
                // if ball standing still (at start of round)
                if (player1YDirection == "up" && bys == 0)
                {
                    bys = random.Next(-9, -4);
                }
                if (player1YDirection == "down" && bys == 0)
                {
                    bys = random.Next(4, 9);

                }
                // if player moving up
                if (player1YDirection == "up" && bys > 0)
                {
                    bys *= -1;
                }
                if (player1XDirection == "up" && bys < 0)
                {
                    bxs -= 0.5;
                }
                //if player moving down
                if (player1YDirection == "down" && bys < 0)
                {
                    bys *= -1;
                }
                if (player1YDirection == "down" && bys > 0)
                {
                    bys += 0.5;
                }
            }
            // if puck intersects with top  of player1
            if (ball.IntersectsWith(player1T))
            {
                // increase speed if going same direction
                if (bys < 0)
                {
                    bys -= 1.5;
                }
                // change direction if going opposite direction
                else if (bys > 0)
                {
                    bys *= -1;
                }
                // if ball standing still (at start of round)
                else if (bys == 0)
                {
                    bys = random.Next(-9, -4);
                }
                //if same direction increase puck speed
                if (player1YDirection == "up")
                {
                    bys -= 0.5;
                }
                // if player moving right
                if (player1XDirection == "right" && bxs < 0)
                {
                    bxs *= -1;
                }
                if (player1XDirection == "right" && bxs > 0)
                {
                    bxs += 0.5;
                }
                // if player moving left
                if (player1XDirection == "left" && bxs > 0)
                {
                    bxs *= -1;
                }
                if (player1XDirection == "left" && bxs < 0)
                {
                    bxs -= 0.5;
                }
                // if ball standing still (at start of round)
                if (player1XDirection == "right" && bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                if (player1XDirection == "left" && bxs == 0)
                {
                    bxs = random.Next(-9, -4);
                }
            }
            // if puck intersects with bottom of player1
            if (ball.IntersectsWith(player1B))
            {
                // increase speed if going same direction
                if (bys > 0)
                {
                    bys += 1.5;
                }
                // change direction if going opposite direction
                else if (bys < 0)
                {
                    bys *= -1;
                }
                // if ball standing still (at start of round)
                else if (bys == 0)
                {
                    bys = random.Next(4, 9);
                }
                //if same direction increase puck speed
                if (player1YDirection == "down")
                {
                    bys += 0.5;
                }
                // if player moving right
                if (player1XDirection == "right" && bxs < 0)
                {
                    bxs *= -1;
                }
                if (player1XDirection == "right" && bxs > 0)
                {
                    bxs += 0.5;
                }
                // if player moving left
                if (player1XDirection == "left" && bxs > 0)
                {
                    bxs *= -1;
                }
                if (player1XDirection == "left" && bxs < 0)
                {
                    bxs -= 0.5;
                }
                // if ball standing still (at start of round)
                if (player1XDirection == "right" && bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                if (player1XDirection == "left" && bxs == 0)
                {
                    bxs = random.Next(-9, -4);
                }
            }




            // if ball intersects with left side of player 2
            if (ball.IntersectsWith(player2L))
            {
                // change direction if going opposite direction
                if (bxs > 0)
                {
                    bxs *= -1;
                }
                // change direction if going opposite direction
                else if (bxs < 0)
                {
                    bxs -= 1.5;
                }
                // if ball standing still (at start of round)
                else if (bxs == 0)
                {
                    bxs = random.Next(-9, -4);

                }
                if (player2XDirection == "left")
                {
                    bxs -= 0.5;
                }
                //if player moving down
                if (player2YDirection == "up" && bys > 0)
                {
                    bys *= -1;
                }
                //if same direction increase puck speed
                if (player2YDirection == "up" && bys < 0)
                {
                    bys -= 0.5;
                }
                //if player moving down
                if (player2YDirection == "down" && bys < 0)
                {
                    bys *= -1;
                }
                if (player2YDirection == "down" && bys > 0)
                {
                    bys += 0.5;
                }
                // if ball standing still (at start of round)
                if (player2YDirection == "down" && bys == 0)
                {
                    bys = random.Next(-9, -4);
                }
                if (player2YDirection == "up" && bys == 0)
                {
                    bys = random.Next(4, 9);

                }
            }

            // if puck intersects with right side of player 2
            if (ball.IntersectsWith(player2R))
            {
                // change direction if going opposite direction
                if (bxs > 0)
                {
                    bxs += 1.5;
                }
                // change direction if going opposite direction
                else if (bxs < 0)
                {
                    bxs *= -1;
                }
                // if ball standing still (at start of round)
                else if (bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                //if same direction increase puck speed
                if (player2XDirection == "right")
                {
                    bxs += 0.5;
                }
                // if ball standing still (at start of round)
                if (player2YDirection == "up" && bys == 0)
                {
                    bys = random.Next(-9, -4);
                }
                if (player2YDirection == "down" && bys == 0)
                {
                    bys = random.Next(4, 9);

                }
                // if player moving up
                if (player2YDirection == "up" && bys > 0)
                {
                    bys *= -1;
                }
                if (player2XDirection == "up" && bys < 0)
                {
                    bxs -= 0.5;
                }
                //if player moving down
                if (player2YDirection == "down" && bys < 0)
                {
                    bys *= -1;
                }
                if (player2YDirection == "down" && bys > 0)
                {
                    bys += 0.5;
                }
            }

            //if puck intersects with to of player 2
            if (ball.IntersectsWith(player2T))
            {
                // increase speed if going same direction
                if (bys < 0)
                {
                    bys -= 1.5;
                }
                // change direction if going opposite direction
                else if (bys > 0)
                {
                    bys *= -1;
                }
                // if ball standing still (at start of round)
                else if (bys == 0)
                {
                    bys = random.Next(-9, -4);
                }
                //if same direction increase puck speed
                if (player2YDirection == "up")
                {
                    bys -= 0.5;
                }
                // if player moving right
                if (player2XDirection == "right" && bxs < 0)
                {
                    bxs *= -1;
                }
                if (player2XDirection == "right" && bxs > 0)
                {
                    bxs += 0.5;
                }
                // if player moving left
                if (player2XDirection == "left" && bxs > 0)
                {
                    bxs *= -1;
                }
                if (player2XDirection == "left" && bxs < 0)
                {
                    bxs -= 0.5;
                }
                // if ball standing still (at start of round)
                if (player2XDirection == "right" && bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                if (player2XDirection == "left" && bxs == 0)
                {
                    bxs = random.Next(-9, -4);
                }
            }

            // if puck intersects with bottome of player 2
            if (ball.IntersectsWith(player2B))
            {
                // increase speed if going same direction
                if (bys > 0)
                {
                    bys += 1.5;
                }
                // change direction if going opposite direction
                else if (bys < 0)
                {
                    bys *= -1;
                }
                // if ball standing still (at start of round)
                else if (bys == 0)
                {
                    bys = random.Next(4, 9);
                }
                //if same direction increase puck speed
                if (player2YDirection == "down")
                {
                    bys += 0.5;
                }
                // if player moving right
                if (player2XDirection == "right" && bxs < 0)
                {
                    bxs *= -1;
                }
                if (player2XDirection == "right" && bxs > 0)
                {
                    bxs += 0.5;
                }
                // if player moving left
                if (player2XDirection == "left" && bxs > 0)
                {
                    bxs *= -1;
                }
                if (player2XDirection == "left" && bxs < 0)
                {
                    bxs -= 0.5;
                }
                // if ball standing still (at start of round)
                if (player2XDirection == "right" && bxs == 0)
                {
                    bxs = random.Next(4, 9);
                }
                if (player2XDirection == "left" && bxs == 0)
                {
                    bxs = random.Next(-9, -4);
                }
            }


            // if puck intersects with a player set x and y values to what they were
            // to ensure ball does not go inside of player
            if (ball.IntersectsWith(player1))
            {
                ball.Y = bally;
                ball.X = ballx;

                player1.Y = player1y;
                player1.X = player1x;
            }
            if (ball.IntersectsWith(player2))
            {
                ball.Y = bally;
                ball.X = ballx;

                player2.Y = player2y;
                player2.X = player2x;
            }

            // redraw player side rectangles to ensure they are still merged with player square
            player1T = new Rectangle(player1.X, player1.Y, 40, 1);
            player1L = new Rectangle(player1.X, player1.Y, 1, 40);
            player1R = new Rectangle(player1.X + 40, player1.Y, 1, 40);
            player1B = new Rectangle(player1.X, player1.Y + 40, 40, 1);

            player2T = new Rectangle(player2.X, player2.Y, 40, 1);
            player2L = new Rectangle(player2.X, player2.Y, 1, 40);
            player2R = new Rectangle(player2.X + 40, player2.Y, 1, 40);
            player2B = new Rectangle(player2.X, player2.Y + 40, 40, 1);

            // if puck hits edge of arena (edge of screen) make it bounce off
            if (ball.Y < 0 || ball.Y > this.Height - 15)
            {
                bys *= -1;
            }

            if (ball.X < 0 || ball.X > this.Width - 15 )
            {
                bxs *= -1;
            }

            // if player 1 gets scored on
            if (ball.IntersectsWith(player1Goal))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                bxs = 0;
                bys = 0;

                ball.Y = this.Height / 4;
                ball.X = this.Width / 2;
            }
            //if player 2 gets scored on
            if (ball.IntersectsWith(player2Goal))
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                bxs = 0;
                bys = 0;

                ball.Y = this.Height - (this.Height / 4);
                ball.X = this.Width / 2;
            }


            // check score and stop game if either player is at 3 
             if (player1Score == 3)
             {
                 gameTick.Enabled = false;
                 winLabel.Visible = true;
                 winLabel.Text = "Player 1 Wins!!";
             }
             else if (player2Score == 3)
             {
                 gameTick.Enabled = false;
                 winLabel.Visible = true;
                 winLabel.Text = "Player 2 Wins!!";
             } 


          
             //slow puck down over time
            if(bxs > 0)
            {
                bxs -= 0.05;
            }
            if (bxs < 0)
            {
                bxs += 0.05;
            }
            if (bys > 0)
            {
                bys -= 0.05;
            }
            if (bys < 0)
            {
                bys += 0.05;
            }


            Refresh();
        }

        //reset game if game ended (by pressing the R button)
        private void resetDetectionTimer_Tick(object sender, EventArgs e)
        {
            if (player1Score == 3 || player2Score == 3)
            {
                if (rDown == true)
                {
                    player1Score = 0;
                    p1ScoreLabel.Text = $"{player1Score}";
                    player2Score = 0;
                    p2ScoreLabel.Text = $"{player2Score}";

                    gameTick.Enabled = true;
                    winLabel.Visible = false;
                }
            }
        }
    }
}
