using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher_GUI_By_Francois284ModzDev.Data
{
	internal class MyJSON
	{
		public static void Create(object objectValue, string PathFile)
		{
			string jsonString = JsonConvert.SerializeObject(objectValue);

			JObject jObject = JsonConvert.DeserializeObject(jsonString) as JObject;

			File.WriteAllText(PathFile, jObject.ToString());
		}

		public static void Update(string KeyToken, JToken ReplaceValue, string PathFile)
		{
			if (File.Exists(PathFile))
			{
				string jsonString = File.ReadAllText(PathFile);

				JObject jObject = JsonConvert.DeserializeObject(jsonString) as JObject;

				JToken token = jObject.SelectToken(KeyToken);

				token.Replace(ReplaceValue);

				string updatedJsonString = jObject.ToString();

				File.WriteAllText(PathFile, updatedJsonString);
			}
		}

		public static string getValueByKey(string filepath, string key)
		{
			var Data = File.ReadAllText(filepath);
			var result = "";
			try
			{
				var jObject = JObject.Parse(Data);

				if(jObject != null)
				{
					result =   jObject[key].ToString();
				}
			}

			catch (Exception)
			{

				throw;
			}
			return result;
		}
	}
}
