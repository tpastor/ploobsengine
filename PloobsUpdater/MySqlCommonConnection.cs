using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for CommonConnection
/// </summary>
public class MySqlCommonConnection
{

    private static MySqlCommonConnection instance;


    public static MySqlCommonConnection Instance()
    {
        // Use 'Lazy initialization' 
        if (instance == null)
        {
            instance = new MySqlCommonConnection();
        }

        return instance;
    }
    
    
    private MySqlConnection connection;



    string connectionstring = "server=dbmy0027.whservidor.com;User Id=ploobs_6;Persist Security Info=True;database=ploobs_6;password=tcc123";
    public void CreateConnection()
    {

       
        //string MyConString = "SERVER=dbmy0027.whservidor.com;" +
        //          "DATABASE=ploobs_6;" +
        //          "UID=ploobs_6;" +
        //          "PASSWORD=tcc123;";
        connection = new MySqlConnection(connectionstring);    
    }

    public void CreateMySqlDataReader(string mySelectQuery)
    {

        MySqlConnection myConnection = new MySqlConnection(connectionstring);
        myConnection.Open();

        MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
        
        MySqlDataReader myReader;
        myReader = myCommand.ExecuteReader();
        try
        {
            while (myReader.Read())
            {

                for (int i = 0; i < myReader.FieldCount; i++)
                {
                    Console.WriteLine(myReader.GetString(i)); 
                }
            }
        }
        finally
        {
            myReader.Close();
            myConnection.Close();
        }
    } 

   public void CreateMySqlCommand() 
  {
    MySqlConnection myConnection = new MySqlConnection(connectionstring);
    myConnection.Open();
    MySqlTransaction myTrans = myConnection.BeginTransaction();
    string mySelectQuery = "SELECT * FROM ploobs_6.PLOOBS_PROFILING";
    MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection,myTrans);
    myCommand.CommandTimeout = 20;
  }
  
    public void executeSQL(string sql)
    {

        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = connection;
        connection.Open();
        int aff = cmd.ExecuteNonQuery();

        
        connection.Close();

    }
    
    protected MySqlCommonConnection() { }


	


}



