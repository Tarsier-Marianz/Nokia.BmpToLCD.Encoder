using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.BmpToLCD.Encoder {
    public static class Conversions {
        public static  string Bin2Hex(this string strBinary) {
           
            string strHex = "0x"+Convert.ToInt32(strBinary, 2).ToString("X2");
            return strHex;
            /*
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i <= strBinary.Length; i++) {
                sb.Append(strBinary[i].ToString("X2"));
            }
            return sb.ToString();
            */
        }


        /*
        public static string Bin2Hex(this string bin) {
            string text = "";
            string text2 = "";
            for(int i = 0; i < 4; i++) {
                text += bin[i];
            }
            if(text.Bin2Dec() == 10) {
                text = "A";
            } else if(text.Bin2Dec() == 11) {
                text = "B";
            } else if(text.Bin2Dec() == 12) {
                text = "C";
            } else if(text.Bin2Dec() == 13) {
                text = "D";
            } else if(text.Bin2Dec() == 14) {
                text = "E";
            } else if(text.Bin2Dec() == 15) {
                text = "F";
            } else {
                text = text.Bin2Dec().ToString();
            }
            for(int i = 4; i < 8; i++) {
                text2 += bin[i];
            }
            if(text.Bin2Dec() == 10) {
                text2 = "A";
            } else if(text.Bin2Dec() == 11) {
                text2 = "B";
            } else if(text.Bin2Dec() == 12) {
                text2 = "C";
            } else if(text.Bin2Dec() == 13) {
                text2 = "D";
            } else if(text.Bin2Dec() == 14) {
                text2 = "E";
            } else if(text.Bin2Dec() == 15) {
                text2 = "F";
            } else {
                text2 = text.Bin2Dec().ToString();
            }
            return "0x" + text + text2;
        }*/

        public static int Bin2Dec(this string bin) {
            int num = 0;
            int num2 = 1;
            int i = bin.Length - 1;
            while(i >= 0) {
                int num3 = (int)(bin[i] - '0');
                num += num3 * num2;
                i--;
                num2 *= 2;
            }
            return num;
        }
    }
}