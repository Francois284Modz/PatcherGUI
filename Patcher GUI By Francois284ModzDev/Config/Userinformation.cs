using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Patcher_GUI_By_Francois284ModzDev.Data;
using System;
using System.IO;
using System.Windows.Forms;

namespace Patcher_GUI_By_Francois284ModzDev.Config
{
	internal class Userinformation
	{
		public class Config
		{
			public static bool FolderExist { get; set; }
			public static string SaveFolder { get; set; }
			public static string ConfigFolder { get; set; }
			public static string MainFolder { get; set; }

			public static string MenuTitle { get; set; }

			public static string ModderName { get; set; }

			public static bool ModderNameSet { get; set; }

			public static bool MenuTittleSet { get; set; }
			/// <summary>
			/// Verifier que les dossier config sont exister
			/// </summary>
			public static void CreateConfigFolder()
			{
				//Chemin de lexecutable
				string StartPath = Application.StartupPath;

				// verifier si le dossier existe
				if (!Directory.Exists(StartPath + "\\Patcher"))
				{
					Directory.CreateDirectory(StartPath + "\\Patcher");
					Directory.CreateDirectory(StartPath + "\\Patcher\\Save");
					Directory.CreateDirectory(StartPath + "\\Patcher\\Config");
					Console.WriteLine("Configuration folder is been created");
					FolderExist = true;
					SaveFolder = StartPath + "\\Patcher\\Save";
					MainFolder = StartPath;
					ConfigFolder = StartPath + "\\Patcher\\Config";
				}
				else
				{
					Console.WriteLine("Configuration folder is allready created");
					FolderExist = true;
					ConfigFolder = StartPath + "\\Patcher\\Config";
					SaveFolder = StartPath + "\\Patcher\\Save";
					MainFolder = StartPath;
				}
			}
		}



		public static void InitiliseModder()
		{
			string modder = "";


			if (!File.Exists(Config.MainFolder + "//config.json"))
			{
				Config.ModderNameSet = false;
				while (Config.ModderNameSet == false)
				{
					DialogResult dialogResult = Utils.InputBox("Who are you ?", "Please let me know your modder name", ref modder);
					if (dialogResult == DialogResult.OK)
					{
						if (modder == "")
						{
							MessageBox.Show("Erreur", "Please insert your modder name", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							Config.ModderNameSet = true;

							JObject UserInformation = new JObject(
							new JProperty("Name", modder));

							File.WriteAllText(Config.MainFolder + "//config.json", UserInformation.ToString());

							// write JSON directly to a file
							using (StreamWriter file = File.CreateText(Config.MainFolder + "//config.json"))
							using (JsonTextWriter writer = new JsonTextWriter(file))
							{
								UserInformation.WriteTo(writer);
							}
							MessageBox.Show("Modder Name have been save", "Modder Name", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							SelectGameName();

						}
					}
				}
		
			}
			else
			{


				// grabe modder name and assign it 
				string userName = MyJSON.getValueByKey(Config.MainFolder + "//config.json", "Name");
				MessageBox.Show($"Welcome back {userName} to PatcherGui", "PatcherGui By Francois284ModzDev", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Config.ModderName = userName;
				Config.ModderNameSet = true;

				while(Config.MenuTittleSet == false)
				{
					SelectGameName();
				}
			
			}
		}


		public static void SelectGameName()
		{
			string Game = "Super ModMenu !";
			DialogResult dialogResult = Utils.InputBox("Menu Tittle ", "Insert your menu Title", ref Game);
			if (dialogResult == DialogResult.OK)
			{
				if (Game == "")
				{
					MessageBox.Show("Erreur", "Please insert your Menu Tittle", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					Userinformation.Config.MenuTitle = Game;
					Config.MenuTittleSet = true;
					MessageBox.Show("Menu Tittle have been save with succes", "Modder Name", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
	}
}
