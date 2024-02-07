using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Configuration;	
using System.Web.UI.HtmlControls;
using System.Text;

namespace RecursosNavegador
{
	public delegate void CSVWriterCallBack(TextWriter output);

	public class Recursos
	{
		public static string GetConfiguration(string key)		
		{		
			AppSettingsReader reader=new AppSettingsReader();	
			return reader.GetValue(key,typeof(String)).ToString();	
		}		

		public static void GenLog(string cont)
		{
			string PathLog ;
			DateTime date = System.DateTime.Now;
			string GenLog = GetConfiguration("genLog");
			if ( GenLog.Equals("1") )
			{
				PathLog = string.Format(GetConfiguration("pathIn"),date.ToString("yyyyMMdd"));
				System.IO.StreamWriter St;
				St = new StreamWriter( PathLog, true);
				try
				{
					St.WriteLine(string.Format("{0} | {1} | >> {2}",date.ToString("dd/MM/yyyy"),date.ToString("HH:mm:ss"),cont));
				}
				finally
				{					
					St.Close() ;
				}
			}
		}
        
        public static void GenResultado(string cont)
        {
            string PathLog;
            DateTime date = System.DateTime.Now;
            PathLog = string.Format(GetConfiguration("pathLog"), date.ToString("yyyyMMdd"));
            System.IO.StreamWriter St;
            St = new StreamWriter(PathLog, true);
            try
            {
                St.WriteLine(string.Format("{0} | {1} | >> {2}", date.ToString("dd/MM/yyyy"), date.ToString("HH:mm:ss"), cont));
            }
            finally
            {
                St.Close();
            }
        }
    }
}