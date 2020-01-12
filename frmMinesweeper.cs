using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper_puzzle
{
	public partial class frmMinesweeper : Form
	{
		private int[] num = new int[26];
		private int[,] flag = new int[9, 9];
		public Button[,] btn = new Button[8, 8];
	
		public frmMinesweeper()
		{
			InitializeComponent();
		}

		private void btn1_Click(object sender, EventArgs e)
		{
			for (int i = 0; i <= 8 ; i++)
			{
				for (int j = 0; j <= 8; j++)
				{
					btn[i, j].Enabled = true;
				}
			}
			FillSq();
			btnStart.Enabled = false;
		}

		private void frmMinesweeper_Load(object sender, EventArgs e)
		{
			{
				btn = new Button[,] { { btnSq1, btnSq2, btnSq3, btnSq4, btnSq5, btnSq6, btnSq7, btnSq8, btnSq9 },
									  { btnSq10, btnSq11, btnSq12, btnSq13, btnSq14, btnSq15, btnSq16, btnSq17, btnSq18 },
									  { btnSq19, btnSq20, btnSq21, btnSq22, btnSq23, btnSq24, btnSq25, btnSq26, btnSq27 },
									  { btnSq28, btnSq29, btnSq30, btnSq31, btnSq32, btnSq33, btnSq34, btnSq35, btnSq36 },
									  { btnSq37, btnSq38, btnSq39, btnSq40, btnSq41, btnSq42, btnSq43, btnSq44, btnSq45 },
									  { btnSq46, btnSq47, btnSq48, btnSq49, btnSq50, btnSq51, btnSq52, btnSq53, btnSq54 },
									  { btnSq55, btnSq56, btnSq57, btnSq58, btnSq59, btnSq60, btnSq61, btnSq62, btnSq63 },
									  { btnSq64, btnSq65, btnSq66, btnSq67, btnSq68, btnSq69, btnSq70, btnSq71, btnSq72 },
									  { btnSq73, btnSq74, btnSq75, btnSq76, btnSq77, btnSq78, btnSq79, btnSq80,btnSq81 } };
			};

		}

		//fills the squares with the mines and call the method CheckSurroundingSq to fill in the numbers
		private void FillSq()
		{
			Random rand = new Random();

			for (int i = 0; i < 25; i += 2)
			{
				num[i] = rand.Next(0, 9);
				num[i + 1] = rand.Next(0, 9);
				btn[num[i], num[i + 1]].Tag = "bomb";
			}

			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 9; j++)
				{
					if (btn[i, j].Tag.Equals("bomb"))
						CheckSurroundingSq(i, j);
				}
			}

		}

		//fills the numbers around the mines
		private void CheckSurroundingSq(int row, int column)
		{
			if (column != 8 && btn[row, column + 1].Tag.ToString() != "bomb")   //right sq
			{
				btn[row, column + 1].Tag = int.Parse(btn[row, column + 1].Tag.ToString()) + 1;
			}
			
			if (column != 0 && btn[row, column - 1].Tag.ToString() != "bomb")//left sq
			{
				btn[row, column - 1].Tag = int.Parse(btn[row, column - 1].Tag.ToString()) + 1;
			}
			
			if (row != 8 && btn[row + 1, column].Tag.ToString() != "bomb")//bottom sq
			{
				btn[row + 1, column].Tag = int.Parse(btn[row + 1, column].Tag.ToString()) + 1;
			}
			
			if (row != 0 && btn[row - 1, column].Tag.ToString() != "bomb")//top sq
			{
				btn[row - 1, column].Tag = int.Parse(btn[row - 1, column].Tag.ToString()) + 1;
			}
			
			if (row != 0 && column != 8 && btn[row - 1, column + 1].Tag.ToString() != "bomb")//diagonal top right sq
			{
				btn[row - 1, column + 1].Tag = int.Parse(btn[row - 1, column + 1].Tag.ToString()) + 1;
			}
		
			if (row != 8 && column != 8 && btn[row + 1, column + 1].Tag.ToString() != "bomb")	//diagonal bottom right sq
			{
				btn[row + 1, column + 1].Tag = int.Parse(btn[row + 1, column + 1].Tag.ToString()) + 1;
			}
			
			if (row != 0 && column != 0 && btn[row - 1, column - 1].Tag.ToString() != "bomb")//diagonal top left sq
			{
				btn[row - 1, column - 1].Tag = int.Parse(btn[row - 1, column - 1].Tag.ToString()) + 1;
			}
			
			if (row != 8 && column != 0 && btn[row + 1, column - 1].Tag.ToString() != "bomb")//diagonal bottom left sq
			{
				btn[row + 1, column - 1].Tag = int.Parse(btn[row + 1, column - 1].Tag.ToString()) + 1;
			}
		}

		//checks the case and goes to the correct method
		private void RevealSquare(int row, int column)
		{
			if (btn[row, column].Enabled == false || flag[row,column] == 1)
			{
				return;
			}

			string btnTag = btn[row, column].Tag.ToString();
			btn[row, column].Enabled = false;

			if (btnTag.Equals("bomb"))
			{
				RevealMine(row, column);
			}

			else if (int.Parse(btnTag) > 0)
			{
				RevealNum(row, column);
				CheckCompletion();
			}

			else if (int.Parse(btnTag) == 0)
			{
				RevealBlank(row, column);
				CheckCompletion();
			}
		}

		//reveals bomb 
		private void RevealMine(int row, int column)
		{
			btn[row, column].BackgroundImage = Properties.Resources.bomb_clicked;
			MessageBox.Show("Game over");
			for (int i = 0; i <= 8; i++)
			{
				for (int j = 0; j <= 8; j++)
				{
					if ( i != row && j != column && btn[i, j].Tag.ToString().Equals("bomb") && flag[i, j] == 0)
					{
						btn[i, j].BackgroundImage = Properties.Resources.bomb_not_clicked;
					}
					if (btn[i, j].Tag.ToString() != "bomb" && flag[i,j] == 1)
					{
						btn[i, j].BackgroundImage = Properties.Resources.bomb_wrong_click;
					}
					btn[i, j].Enabled = false;
				}
			}
		}

		//reveals number
		private void RevealNum(int index1, int index2)
		{
			if (btn[index1, index2].Tag.Equals(1))
				btn[index1, index2].BackgroundImage = Properties.Resources._11;
			else if (btn[index1, index2].Tag.Equals(2))
				btn[index1, index2].BackgroundImage = Properties.Resources._21;
			else if (btn[index1, index2].Tag.Equals(3))
				btn[index1, index2].BackgroundImage = Properties.Resources._31;
			else if (btn[index1, index2].Tag.Equals(4))
				btn[index1, index2].BackgroundImage = Properties.Resources._41;
			else if (btn[index1, index2].Tag.Equals(5))
				btn[index1, index2].BackgroundImage = Properties.Resources._51;
			else if (btn[index1, index2].Tag.Equals(6))
				btn[index1, index2].BackgroundImage = Properties.Resources._6;
			else if (btn[index1, index2].Tag.Equals(7))
				btn[index1, index2].BackgroundImage = Properties.Resources._7;
			else if (btn[index1, index2].Tag.Equals(8))
				btn[index1, index2].BackgroundImage = Properties.Resources._8;
		}

		//reveals blank
		private void RevealBlank(int row, int column)
		{
			if (column != 8)//right sq
			{
				RevealSquare(row, column + 1);
			}

			if (column != 0)//left sq
			{
				RevealSquare(row, column - 1);
			}

			if (row != 8)//bottom sq
			{
				RevealSquare(row + 1, column);
			}

			if (row != 0)//top sq
			{
				RevealSquare(row - 1, column);
			}

			if (row != 0 && column != 8)//diagonal top right sq
			{
				RevealSquare(row - 1, column + 1);
			}

			if (row != 8 && column != 8)//diagonal bottom right sq
			{
				RevealSquare(row + 1, column + 1);
			}

			if (row != 0 && column != 0)//diagonal top left sq
			{
				RevealSquare(row - 1, column - 1);
			}

			if (row != 8 && column != 0)//diagonal bottom left sq
			{
				RevealSquare(row + 1, column - 1);
			}

		}

		private void CheckCompletion()
		{
			int count = 0;
			for (int row = 0; row <= 8; row++)
			{
				for (int column = 0; column <= 8; column++)
				{
					if (btn[row, column].Enabled == false)
					{
						count += 1;
					}
					if (btn[row, column].Tag.Equals("bomb") && flag[row,column] == 1 )
					{
						count += 1;			
					}

					if (count == 81)
					{
						MessageBox.Show("You Win");
						for (int i = 0; i <= 8; i++)
						{
							for (int j = 0; j <= 8; j++)
							{
								btn[i, j].Enabled = false;
							}
						}
					}
				}			
			}
		}

		//when right clicked on mouse the flag shows
		private void Flag(int row, int column)
		{
			if (flag[row, column] == 0)
			{
				flag[row, column] += 1;
				btn[row, column].BackgroundImage = Properties.Resources.flag;
			}
			else if (flag[row, column] == 1)
			{
				btn[row, column].BackgroundImage = null;
				flag[row, column] = 0;
			}
			CheckCompletion();
		}

		private void btnSq82_Click(object sender, EventArgs e)
		{
			RevealSquare(8, 8);
		}

		private void btnSq81_Click(object sender, EventArgs e)
		{
			RevealSquare(8,7);
		}

		private void btnSq80_Click(object sender, EventArgs e)
		{
			RevealSquare(8,6);
		}

		private void btnSq79_Click(object sender, EventArgs e)
		{
			RevealSquare(8,5);
		}

		private void btnSq78_Click(object sender, EventArgs e)
		{
			RevealSquare(8,4);
		}

		private void btnSq77_Click(object sender, EventArgs e)
		{
			RevealSquare(8,3);
		}

		private void btnSq76_Click(object sender, EventArgs e)
		{
			RevealSquare(8,2);
		}

		private void btnSq75_Click(object sender, EventArgs e)
		{
			RevealSquare(8,1);
		}

		private void btnSq74_Click(object sender, EventArgs e)
		{
			RevealSquare(8,0);
		}

		private void btnSq73_Click(object sender, EventArgs e)
		{
			RevealSquare(7,8);
		}

		private void btnSq72_Click(object sender, EventArgs e)
		{
			RevealSquare(7,7);
		}

		private void btnSq71_Click(object sender, EventArgs e)
		{
			RevealSquare(7,6);
		}

		private void btnSq70_Click(object sender, EventArgs e)
		{
			RevealSquare(7,5);
		}

		private void btnSq69_Click(object sender, EventArgs e)
		{
			RevealSquare(7,4);
		}

		private void btnSq68_Click(object sender, EventArgs e)
		{
			RevealSquare(7,3);
		}

		private void btnSq67_Click(object sender, EventArgs e)
		{
			RevealSquare(7,2);
		}

		private void btnSq66_Click(object sender, EventArgs e)
		{
			RevealSquare(7,1);
		}

		private void btnSq65_Click(object sender, EventArgs e)
		{
			RevealSquare(7,0);
		}

		private void btnSq64_Click(object sender, EventArgs e)
		{
			RevealSquare(6,8);
		}

		private void btnSq63_Click(object sender, EventArgs e)
		{
			RevealSquare(6,7);
		}

		private void btnSq62_Click(object sender, EventArgs e)
		{
			RevealSquare(6,6);
		}

		private void btnSq61_Click(object sender, EventArgs e)
		{
			RevealSquare(6,5);
		}

		private void btnSq60_Click(object sender, EventArgs e)
		{
			RevealSquare(6, 4);
		}

		private void btnSq59_Click(object sender, EventArgs e)
		{
			RevealSquare(6,3);
		}

		private void btnSq58_Click(object sender, EventArgs e)
		{
			RevealSquare(6,2);
		}

		private void btnSq57_Click(object sender, EventArgs e)
		{
			RevealSquare(6,1);
		}

		private void btnSq56_Click(object sender, EventArgs e)
		{
			RevealSquare(6,0);
		}

		private void btnSq55_Click(object sender, EventArgs e)
		{
			RevealSquare(5,8);
		}

		private void btnSq54_Click(object sender, EventArgs e)
		{
			RevealSquare(5,7);
		}

		private void btnSq53_Click(object sender, EventArgs e)
		{
			RevealSquare(5,6);
		}

		private void btnSq52_Click(object sender, EventArgs e)
		{
			RevealSquare(5,5);
		}

		private void btnSq51_Click(object sender, EventArgs e)
		{
			RevealSquare(5,4);
		}

		private void btnSq50_Click(object sender, EventArgs e)
		{
			RevealSquare(5,3);
		}

		private void btnSq49_Click(object sender, EventArgs e)
		{
			RevealSquare(5,2);
		}

		private void btnSq48_Click(object sender, EventArgs e)
		{
			RevealSquare(5,1);
		}

		private void btnSq47_Click(object sender, EventArgs e)
		{
			RevealSquare(5,0);
		}

		private void btnSq46_Click(object sender, EventArgs e)
		{
			RevealSquare(4,8);
		}

		private void btnSq45_Click(object sender, EventArgs e)
		{
			RevealSquare(4,7);
		}

		private void btnSq44_Click(object sender, EventArgs e)
		{
			RevealSquare(4,6);
		}

		private void btnSq43_Click(object sender, EventArgs e)
		{
			RevealSquare(4,5);
		}

		private void btnSq42_Click(object sender, EventArgs e)
		{
			RevealSquare(4,4);
		}

		private void btnSq40_Click(object sender, EventArgs e)
		{
			RevealSquare(4,3);
		}

		private void btnSq39_Click(object sender, EventArgs e)
		{
			RevealSquare(4,2);
		}

		private void btnSq38_Click(object sender, EventArgs e)
		{
			RevealSquare(4,1);
		}

		private void btnSq37_Click(object sender, EventArgs e)
		{
			RevealSquare(4,0);
		}

		private void btnSq36_Click(object sender, EventArgs e)
		{
			RevealSquare(3,8);
		}

		private void btnSq35_Click(object sender, EventArgs e)
		{
			RevealSquare(3,7);
		}

		private void btnSq34_Click(object sender, EventArgs e)
		{
			RevealSquare(3,6);
		}

		private void btnSq33_Click(object sender, EventArgs e)
		{
			RevealSquare(3,5);
		}

		private void btnSq32_Click(object sender, EventArgs e)
		{
			RevealSquare(3,4);
		}

		private void btnSq31_Click(object sender, EventArgs e)
		{
			RevealSquare(3,3);
		}

		private void btnSq30_Click(object sender, EventArgs e)
		{
			RevealSquare(3,2);
		}

		private void btnSq29_Click(object sender, EventArgs e)
		{
			RevealSquare(3,1);
		}

		private void btnSq28_Click(object sender, EventArgs e)
		{
			RevealSquare(3,0);
		}

		private void btnSq27_Click(object sender, EventArgs e)
		{
			RevealSquare(2,8);
		}

		private void btnSq26_Click(object sender, EventArgs e)
		{
			RevealSquare(2,7);
		}

		private void btnSq25_Click(object sender, EventArgs e)
		{
			RevealSquare(2,6);
		}

		private void btnSq24_Click(object sender, EventArgs e)
		{
			RevealSquare(2,5);
		}

		private void btnSq23_Click(object sender, EventArgs e)
		{
			RevealSquare(2,4);
		}

		private void btnSq22_Click(object sender, EventArgs e)
		{
			RevealSquare(2,3);
		}

		private void btnSq21_Click(object sender, EventArgs e)
		{
			RevealSquare(2,2);
		}

		private void btnSq20_Click(object sender, EventArgs e)
		{
			RevealSquare(2,1);
		}

		private void btnSq19_Click(object sender, EventArgs e)
		{
			RevealSquare(2,0);
		}

		private void btnSq18_Click(object sender, EventArgs e)
		{
			RevealSquare(1,8);
		}

		private void btn17_Click(object sender, EventArgs e)
		{
			RevealSquare(1, 7);
		}

		private void btnSq16_Click(object sender, EventArgs e)
		{
			RevealSquare(1,6);
		}

		private void btnSq15_Click(object sender, EventArgs e)
		{
			RevealSquare(1,5);
		}

		private void btnSq14_Click(object sender, EventArgs e)
		{
			RevealSquare(1,4);
		}

		private void btnSq13_Click(object sender, EventArgs e)
		{
			RevealSquare(1,3);
		}

		private void btnSq12_Click(object sender, EventArgs e)
		{
			RevealSquare(1,2);
		}

		private void btnSq11_Click(object sender, EventArgs e)
		{
			RevealSquare(1,1);
		}

		private void btnSq10_Click(object sender, EventArgs e)
		{
			RevealSquare(1,0);
		}

		private void btnSq9_Click(object sender, EventArgs e)
		{
			RevealSquare(0,8);
		}

		private void btnSq8_Click(object sender, EventArgs e)
		{
			RevealSquare(0,7);
		}

		private void btnSq7_Click(object sender, EventArgs e)
		{
			RevealSquare(0,6);
		}

		private void btnSq6_Click(object sender, EventArgs e)
		{
			RevealSquare(0,5);
		}

		private void btnSq5_Click(object sender, EventArgs e)
		{
			RevealSquare(0,4);
		}

		private void btnSq4_Click(object sender, EventArgs e)
		{
			RevealSquare(0,3);
		}

		private void btnSq3_Click(object sender, EventArgs e)
		{
			RevealSquare(0,2);
		}

		private void btnSq2_Click(object sender, EventArgs e)
		{
			RevealSquare(0,1);
		}

		private void btnSq1_Click(object sender, EventArgs e)
		{
			RevealSquare(0, 0);
		}

		private void btnSq1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 0);
		}

		private void btnSq2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 1);
		}

		private void btnSq3_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 2);
		}

		private void btnSq4_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 3);
		}

		private void btnSq5_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 4);
		}

		private void btnSq6_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 5);
		}

		private void btnSq7_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 6);
		}

		private void btnSq8_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 7);
		}

		private void btnSq9_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(0, 8);
		}

		private void btnSq10_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 0);
		}

		private void btnSq11_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 1);
		}

		private void btnSq12_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 2);
		}

		private void btnSq13_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 3);
		}

		private void btnSq14_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 4);
		}

		private void btnSq15_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 5);
		}

		private void btnSq16_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 6);
		}

		private void btn17_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 7);
		}

		private void btnSq18_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(1, 8);
		}

		private void btnSq19_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 0);
		}

		private void btnSq20_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 1);
		}

		private void btnSq21_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 2);
		}

		private void btnSq22_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 3);
		}

		private void btnSq23_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 4);
		}

		private void btnSq24_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 5);
		}

		private void btnSq25_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 6);
		}

		private void btnSq26_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 7);
		}

		private void btnSq27_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(2, 8);
		}

		private void btnSq28_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 0);
		}

		private void btnSq29_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 1);
		}

		private void btnSq30_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 2);
		}

		private void btnSq31_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 3);
		}

		private void btnSq32_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 4);
		}

		private void btnSq33_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 5);
		}

		private void btnSq34_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 6);
		}

		private void btnSq35_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 7);
		}

		private void btnSq36_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(3, 8);
		}

		private void btnSq37_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 0);
		}

		private void btnSq38_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 1);
		}

		private void btnSq39_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 2);
		}

		private void btnSq40_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 3);
		}

		private void btnSq41_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 4);
		}

		private void btnSq42_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 5);
		}

		private void btnSq43_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 6);
		}

		private void btnSq44_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 7);
		}

		private void btnSq45_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(4, 8);
		}

		private void btnSq46_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 0);
		}

		private void btnSq47_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 1);
		}

		private void btnSq48_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 2);
		}

		private void btnSq49_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 3);
		}

		private void btnSq50_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 4);
		}

		private void btnSq51_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 5);
		}

		private void btnSq52_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 6);
		}

		private void btnSq53_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 7);
		}

		private void btnSq54_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(5, 8);
		}

		private void btnSq55_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 0);
		}

		private void btnSq56_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 1);
		}

		private void btnSq57_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 2);
		}

		private void btnSq58_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 3);
		}

		private void btnSq59_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 4);
		}

		private void btnSq60_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 5);
		}

		private void btnSq61_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 6);
		}

		private void btnSq62_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 7);
		}

		private void btnSq63_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(6, 8);
		}

		private void btnSq64_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 0);
		}

		private void btnSq65_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 1);
		}

		private void btnSq66_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 2);
		}

		private void btnSq67_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 3);
		}

		private void btnSq68_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 4);
		}

		private void btnSq69_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 5);
		}

		private void btnSq70_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 6);
		}

		private void btnSq71_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 7);
		}

		private void btnSq72_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(7, 8);
		}

		private void btnSq73_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 0);
		}

		private void btnSq74_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 1);
		}

		private void btnSq75_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 2);
		}

		private void btnSq76_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 3);
		}

		private void btnSq77_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 4);
		}

		private void btnSq78_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 5);
		}

		private void btnSq79_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 6);
		}

		private void btnSq80_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 7);
		}

		private void btnSq81_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Flag(8, 8);
		}

	}
}
