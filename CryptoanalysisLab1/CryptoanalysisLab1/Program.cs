using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace CryptoanalysisLab1 {
    class Program {
        static void Main(string[] args) {

            DoLab(6);
            Console.ReadKey();

        }

        static void DoLab(int variant) {
            
            double[] P_M = new double[20];
            double[] P_K = new double[20];
            double[] P_C = new double[20];
            double[,] P_MK = new double[20,20];
            int[,] CypherTable = new int[20,20];
            double[,] P_MC = new double[20,20];
            double[,] P_M1C = new double[20,20];


            Excel Distribution = new Excel(@"C:\Users\PRIDE\source\repos\CryptoanalysisLab\CryptoanalysisLab1\CryptoanalysisLab1\prob_0" + Convert.ToString(variant));
            
            for (int i = 0; i<20; i++) { 

                P_M[i] = Distribution.ReadCell(0,i);
                P_K[i] = Distribution.ReadCell(1, i);
            }

            Distribution.Quit();

            for (int i = 0; i<20; i++)
                for (int j = 0; j < 20; j++)
                    P_MK[i,j] = P_M[i]*P_K[j];

            Excel CipherTableExcel = new Excel(@"C:\Users\PRIDE\source\repos\CryptoanalysisLab\CryptoanalysisLab1\CryptoanalysisLab1\table_0" + Convert.ToString(variant));

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    CypherTable[i, j] = Convert.ToInt32(CipherTableExcel.ReadCell(i,j));

            CipherTableExcel.Quit();

            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 20; j++)
                    P_C[CypherTable[i,j]] += P_MK[j,i];
            }

            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 20; j++)
                    P_MC[i,CypherTable[i, j]] += P_MK[j, i];
            }

            for (int i = 0; i<20; i++)
                for (int j = 0; j < 20; j++)
                    P_M1C[i,j] = P_MC[i,j]/P_C[j];

            Console.WriteLine(DeterminingFunc(P_M1C));
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||

        static int DeterminingFunc(double[,] P_M1C) {
            
            int C = 16;
            int result = 0;
            for (int i = 1; i<20; i++) 
                if(P_M1C[i,C] > P_M1C[i-1, C]) result = i;       
            return result;
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||
    class Excel {

        string path = "";
        _Excel.Application excel = new _Excel.Application();
        _Excel.Workbook wb;
        _Excel.Worksheet ws;

        public Excel(string path) {
            
            this.path = path;
            this.wb = excel.Workbooks.Open(path);
            this.ws = wb.Worksheets[1];
        }

        public double ReadCell(int row, int col) {
        
            row++;
            col++;
            if (ws.Cells[row, col].Value2 == null) { 
                
                Console.WriteLine("Null is returned! Cell({0},{1})", row, col);
                return 0; 
            }
            double save = ws.Cells[row, col].Value;
            return save;
        }

        public void Quit() {
            
            wb.Close();
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        }
    }
}
