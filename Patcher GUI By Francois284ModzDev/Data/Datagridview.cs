using LogIn_Theme_Dll_By_xVenoxi;
using Patcher_GUI_By_Francois284ModzDev.Config;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Patcher_GUI_By_Francois284ModzDev.Data
{
	internal class Datagridview
	{


		public static Form1 form { get; set; }


		/// <summary>
		/// Le Resulta est strocker en static 
		/// </summary>
		public static string GeneratedData { get; set; }
		public static string FinalData { get; set; }

		/// <summary>
		/// Initialiser le datagriedview et rajouter les collone
		/// </summary>
		public static void AddColumns()
		{
			Form1.OffsetDataTable.Columns.Add("CheatName", typeof(String));
			Form1.OffsetDataTable.Columns.Add("Offset", typeof(String));
			Form1.OffsetDataTable.Columns.Add("EditHex", typeof(String));
			Form1.OffsetDataTable.Columns.Add("RestoreHex", typeof(String));
			Form1.OffsetDataTable.Columns.Add("FlagType", typeof(String));
			Form1.OffsetDataTable.Columns.Add("TargetLib", typeof(String));
			form.dataGridView1.DataSource = Form1.OffsetDataTable;
			form.dataGridView1.Columns[0].Width = 130;
			form.dataGridView1.Columns[1].Width = 80;
			form.dataGridView1.Columns[2].Width = 140;
			form.dataGridView1.Columns[3].Width = 140;
			form.dataGridView1.Columns[4].Width = 130;
			form.dataGridView1.Columns[5].Width = 130;
		}

		/// <summary>
		/// Ajouter des items aux data gridview
		/// </summary>
		/// <param name="cheatname"></param>
		/// <param name="Offset"></param>
		/// <param name="Edithex"></param>
		/// <param name="RestoreHex"></param>
		/// <param name="GGFlags"></param>
		/// <param name="TargetLib"></param>
		public static void AddItems(LogInNormalTextBox cheatname, LogInNormalTextBox Offset, LogInNormalTextBox Edithex, LogInNormalTextBox RestoreHex, LogInComboBox GGFlags, LogInNormalTextBox TargetLib)
		{
			DataTable dataTable = (DataTable)form.dataGridView1.DataSource;
			DataRow drToAdd = dataTable.NewRow();
			drToAdd["CheatName"] = cheatname.Text;
			drToAdd["Offset"] = Offset.Text;
			drToAdd["EditHex"] = Edithex.Text;
			drToAdd["RestoreHex"] = RestoreHex.Text;
			drToAdd["FlagType"] = GGFlags.Text;
			drToAdd["TargetLib"] = TargetLib.Text;
			dataTable.Rows.Add(drToAdd);
			dataTable.AcceptChanges();


			cheatname.Text = "";
			Offset.Text = "";
			Edithex.Text = "";
			RestoreHex.Text = "";
			GGFlags.SelectedIndex = 0;
		}


		/// <summary>
		/// Verifier que datagriedview est pas vide puis enregister
		/// </summary>
		public void SaveConfig()
		{
			if (form.dataGridView1.Rows.Count > 0)
			{
				SaveData();
			}
			else
			{
				MessageBox.Show("Please load data or add to list before trying to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			}
		}

		/// <summary>
		/// Enregister les donner dans un ficher xml
		/// </summary>
		private void SaveData()
		{
			if (Userinformation.Config.FolderExist)
			{
				SaveFileDialog saveFileDialog1 = new SaveFileDialog
				{
					InitialDirectory = Userinformation.Config.ConfigFolder,
					DefaultExt = "xml",
					AddExtension = true,
					CheckPathExists = true
				};

				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					var path = saveFileDialog1.FileName;
					DataTable dt = new DataTable
					{
						TableName = "Patcher"
					};
					for (int i = 0; i < form.dataGridView1.Columns.Count; i++)
					{
						//if (dataGridView1.Columns[i].Visible) // Add's only Visible columns (if you need it)
						//{
						string headerText = form.dataGridView1.Columns[i].HeaderText;
						headerText = Regex.Replace(headerText, "[-/, ]", "_");

						DataColumn column = new DataColumn(headerText);
						dt.Columns.Add(column);
						//}
					}

					foreach (DataGridViewRow DataGVRow in form.dataGridView1.Rows)
					{
						DataRow dataRow = dt.NewRow();
						// Add's only the columns that you want
						dataRow["CheatName"] = DataGVRow.Cells["CheatName"].Value;
						dataRow["Offset"] = DataGVRow.Cells["Offset"].Value;
						dataRow["EditHex"] = DataGVRow.Cells["EditHex"].Value;
						dataRow["RestoreHex"] = DataGVRow.Cells["RestoreHex"].Value;
						dataRow["FlagType"] = DataGVRow.Cells["FlagType"].Value;
						dataRow["TargetLib"] = DataGVRow.Cells["TargetLib"].Value;
						dt.Rows.Add(dataRow);
					}

					DataSet ds = new DataSet();
					ds.Tables.Add(dt);

					//Finally the save part:
					XmlTextWriter xmlSave = new XmlTextWriter(path, Encoding.UTF8)
					{
						Formatting = Formatting.Indented
					};
					ds.DataSetName = "Data";
					ds.WriteXml(xmlSave);
					xmlSave.Close();
					MessageBox.Show($"File was successfully saved to {path}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}



		}


		/// <summary>
		/// Charger les donner depuis un ficher xml 
		/// </summary>
		public void LoadData()
		{


			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				form.dataGridView1.DataSource = null;
				form.dataGridView1.Columns.Clear();

				string path = openFileDialog.FileName;

				Form1.dataSet.ReadXml(path);
				form.dataGridView1.DataSource = Form1.dataSet.Tables[0];

				form.dataGridView1.Columns[0].Width = 130;
				form.dataGridView1.Columns[1].Width = 80;
				form.dataGridView1.Columns[2].Width = 170;
				form.dataGridView1.Columns[3].Width = 170;
				form.dataGridView1.Columns[4].Width = 110;
				form.dataGridView1.Columns[5].Width = 130;
			}
		}



		/// <summary>
		/// Generer les information du datagried
		/// </summary>
		public void StartGenerate()
		{
			string MainText1 = "";
			string DataResult = "";

			foreach (DataGridViewRow Datarow in form.dataGridView1.Rows)
			{
				// No Flags so using main 
				if (Datarow.Cells[4].Value.ToString() == "NONE")
				{
					if (Datarow.Cells[5].Value.ToString() != "libil2cpp.so")
					{
						MainText1 = string.Concat(new String[] {
						"{\n",
						"name =  \"" + Datarow.Cells[0].Value.ToString() + "\",\n",
						"offset =  \"" + Datarow.Cells[2].Value.ToString() + "\",\n",
						"libName =  \"" + Datarow.Cells[5].Value.ToString() + "\",\n",
						"valueTo =  \"" + Datarow.Cells[1].Value.ToString() + "\",\n",
						"valueFrom =  \"" + Datarow.Cells[3].Value.ToString() + "\",\n",
						"},\n"
					});
					}
					else
					{
						MainText1 = string.Concat(new String[] {
						"{\n",
						"name =  \"" + Datarow.Cells[0].Value.ToString() + "\",\n",
						"valueTo =  \"" + Datarow.Cells[1].Value.ToString() + "\",\n",
						"offset =  \"" + Datarow.Cells[2].Value.ToString() + "\",\n",
						"valueFrom =  \"" + Datarow.Cells[3].Value.ToString() + "\",\n",
						"},\n"
					});
					}

				}
				else
				{
						if (Datarow.Cells[5].Value.ToString() == "libil2cpp.so")
						{
							MainText1 = string.Concat(new String[] {
							"{\n",
							"name =  \"" + Datarow.Cells[0].Value.ToString() + "\",\n",
							"valueTo =  \"" + Datarow.Cells[1].Value.ToString() + "\",\n",
							"offset =  \"" + Datarow.Cells[2].Value.ToString() + "\",\n",
							"valueFrom =  \"" + Datarow.Cells[3].Value.ToString() + "\",\n",
							"flags =  \"" + Datarow.Cells[4].Value.ToString() + "\",\n",
							"},\n"
						});
						}else
					{
						MainText1 = string.Concat(new String[] {
							"{\n",
							"name =  \"" + Datarow.Cells[0].Value.ToString() + "\",\n",
							"offset =  \"" + Datarow.Cells[2].Value.ToString() + "\",\n",
							"libName =  \"" + Datarow.Cells[5].Value.ToString() + "\",\n",
							"valueTo =  \"" + Datarow.Cells[1].Value.ToString() + "\",\n",
							"valueFrom =  \"" + Datarow.Cells[3].Value.ToString() + "\",\n",
							"flags =  \"" + Datarow.Cells[4].Value.ToString() + "\",\n",
							"},\n"
						});
					}

				}
				DataResult += MainText1;

			}

			GeneratedData = DataResult;
			FormatResult();
		}


		public void FormatResult()
		{
			string Config = "";

			Config = String.Concat(new String[] {
				"local Patcher = require(\"Patcher\")\n\r",
				"\nlocal config = {\n",
				  $"title   = \" {Userinformation.Config.MenuTitle}  \" \n",
					$"author  =  \"{Userinformation.Config.ModderName}\" \n",
					"target  = { libName = \"libil2cpp.so\" }\n",
					"}\n\r",
					"local p = Patcher.new( config )\n",
					"local methods = {\n",
					GeneratedData,
					"}\n\r",
					"for _, m in ipairs(methods) do \n",
					"p:add(m) \n",
					"end \n",
					"p:run() \n"
			});

			using (StreamWriter sw = new StreamWriter(Userinformation.Config.SaveFolder + "\\patcher.lua"))
			{
				sw.WriteLine(Config);
				
				MessageBox.Show($"Patch have been saved with sucess in {Userinformation.Config.SaveFolder}", "Sucesss", MessageBoxButtons.OK,MessageBoxIcon.Information);
				sw.Close();
			}



		}
	}
}
