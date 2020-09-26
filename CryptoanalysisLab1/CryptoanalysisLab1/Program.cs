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

            DoLab(3);
            Console.ReadKey();

        }

        static void DoLab(int variant) {
            
            Excel A = new Excel(@"C:\Users\PRIDE\source\repos\CryptoanalysisLab\CryptoanalysisLab1\CryptoanalysisLab1\prob_0" + Convert.ToString(variant), 1);
            Console.WriteLine(A.ReadCell(0,19));
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~||
    class Excel {

        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int Sheet) {
            
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string ReadCell(int row, int col) {
        
            row++;
            col++;
            if (ws.Cells[row, col].Value2 == null)
                return "";
            string save = Convert.ToString(ws.Cells[row, col].Value);
            excel.Quit();
            return save;
        }
    }
}
