using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Net;
using System.Management;
using System.Windows;

/// <summary>
/// Summary description for CommonConnection
/// </summary>
public static class MySqlCommonConnection
{

    static string connectionstring = "server=dbmy0027.whservidor.com;User Id=ploobs_6;Persist Security Info=True;database=ploobs_6;password=tcc123";

    public static void WriteClientToDB(String PloobsVersion)
    {
        MySqlConnection myConnection = null;
        try
        {
            myConnection = new MySqlConnection(connectionstring);
            myConnection.Open();
            String processor = System.Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");

            System.OperatingSystem osInfo = System.Environment.OSVersion;
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            var ip = (from addr in hostEntry.AddressList where addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork select addr.ToString()).FirstOrDefault();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
            string graphicsCard = string.Empty;
            foreach (ManagementObject mo in searcher.Get())
            {
                foreach (PropertyData property in mo.Properties)
                {
                    if (property.Name == "Description")
                    {
                        graphicsCard = property.Value.ToString();
                    }
                }
            }

            String mySelectQuery = "INSERT INTO UsersInfo (Name, Os, Ip, Processors,Date,VideoCard,Version) VALUES (\"" + Environment.MachineName + "\",\"" + osInfo.VersionString + "\", \"" + ip + "\", \"" + processor + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\" , \"" + graphicsCard + "\", \"" + PloobsVersion + "\")";
            MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
            myCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ///nothing =P
        }
        finally
        {            
            myConnection.Close();
        }     
    }

    

}



