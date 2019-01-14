using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace stackTraceYontemi
{
    class Program
    {
       
        static SqlConnection con;
        static void Main(string[] args)
        {
            uygulama();
            Console.ReadLine();
        }

        public static void uygulama()
        {
            try
            {
                string str = "qwe";
                int i = Convert.ToInt32(str);

            }
            catch (Exception ex)
            {
                
                con = new SqlConnection("Data Source=SEM-BILGISAYAR;Initial Catalog=test;User ID=test;Password=test");
                //hatanın satır numarasına erişmek içn true değeri veriliyor.
                StackTrace stack = new StackTrace(true);
                foreach (StackFrame frame in stack.GetFrames())
                {
                    if (!string.IsNullOrEmpty(frame.GetFileName()))
                    {


                        con.Execute("Insert into HataLoglari(DosyaAdi,MethodAdi,LineNumber,ColumnNumber,Message) Values(@DosyaAdi,@MethodAdi,@LineNumber,@ColumnNumber,@message)",
                           new
                           {
                               @DosyaAdi = "emre "+Path.GetFileName(frame.GetFileName()),
                               @MethodAdi = frame.GetMethod().ToString(),
                               @LineNumber = frame.GetFileLineNumber(),
                               @ColumnNumber = frame.GetFileColumnNumber(),
                               @message = ex.StackTrace
                           }

                            );

                        Console.WriteLine($"dosya adı:{frame.GetFileName()}");
                        Console.WriteLine($"line number:{frame.GetFileLineNumber()}");
                        Console.WriteLine($"column number:{frame.GetFileColumnNumber()}");
                        Console.WriteLine($"method Name:{frame.GetMethod()}");
                        
                    }

                }

                Console.ReadLine();
            }


        }
    }
}
