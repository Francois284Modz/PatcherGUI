using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patcher_GUI_By_Francois284ModzDev.Data;
using Patcher_GUI_By_Francois284ModzDev.Config;
using System.Xml;
using System.Text.RegularExpressions;

namespace Patcher_GUI_By_Francois284ModzDev
{


	public partial class Form1 : Form
	{
		public static Form1 form ;
		Datagridview Data = new Datagridview();
		public static DataSet dataSet = new DataSet();
		public static DataTable OffsetDataTable = new DataTable("OffsetData");
		public Form1()
		{
			InitializeComponent();

		
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Initilise();
			//inistialise game info 
			Userinformation.InitiliseModder();
		}

		/// <summary>
		/// Charger tout les data 
		/// </summary>
		public void Initilise()
		{
			//Verifier si tout les ficher config existe
			Userinformation.Config.CreateConfigFolder(); 

			//charger le datagried
			Datagridview.form = this;
			Datagridview.AddColumns();

			

		}

		private void logInButton1_Click(object sender, EventArgs e)
		{
			Datagridview.AddItems(CheatNameTextbox, OffsetTextbox,EditHexTextbox,RestoreHexTextbox,FlagsComboBox,TargetlibText);
		}

		private void logInButton4_Click(object sender, EventArgs e)
		{
			Data.SaveConfig();
		}

		private void logInButton3_Click(object sender, EventArgs e)
		{
			Data.LoadData();
		}

		private void logInButton2_Click(object sender, EventArgs e)
		{
			Data.StartGenerate();
	
		}

		private void logInButton5_Click(object sender, EventArgs e)
		{
			Userinformation.InitiliseModder();
		}

		private void logInGroupBox2_Click(object sender, EventArgs e)
		{

		}
	}
}
