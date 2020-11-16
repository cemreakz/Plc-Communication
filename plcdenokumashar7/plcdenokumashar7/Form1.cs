using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;

namespace plcdenokumashar7
{
    public partial class Form1 : Form
    {
        static S7Client Plc = new S7Client();
        public int result = Plc.ConnectTo("192.168.0.1",0,1); //bağlanılacak plcnın ethernet adresi
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (result == 0)
            {
                ErorText.Text = "OK";
            }
            else
            {
                ErorText.Text = "Com Eror";
            }
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            var buffer = new byte[14];//guncelle
            int readresult = Plc.DBRead(2, 0, buffer.Length, buffer);
            TxtRead1.Text = Convert.ToString(S7.GetBitAt(buffer, 0, 0)); //0.byte'ın 0.biti
            TxtRead2.Text = Convert.ToString(S7.GetBitAt(buffer, 0, 1)); //0.byte'ın 1.biti
            TxtRead3.Text = Convert.ToString(S7.GetIntAt(buffer, 2)); // 2. byte (for plc 1int= 2 byte)
            TxtRead4.Text = Convert.ToString(S7.GetUDIntAt(buffer, 4)); //4. byte (for plc 1dint= 4 byte)
            TxtRead5.Text = Convert.ToString(S7.GetWordAt(buffer, 8)); //8.byte (1 word = 2 byte)
            TxtRead6.Text = Convert.ToString(S7.GetDWordAt(buffer, 10)); //10.byte (1 dword = 4 byte) =10+4=14 oldu bufferda 14tü.

        }

        private void BtnWrite_Click(object sender, EventArgs e)
        {
            var buffer1 = new byte[14];//guncelle
            S7.SetBitAt(ref buffer1, 0, 0, Convert.ToBoolean(TxtWrite1.Text)); //0.byte'ın 0.biti
            S7.SetBitAt(ref buffer1, 0, 1, Convert.ToBoolean(TxtWrite2.Text)); //0.byte'ın 0.biti //0.byte'ın 0.biti
            S7.SetIntAt(buffer1, 2, Convert.ToInt16(TxtWrite3.Text)); // 2. byte (for plc 1int= 2 byte)
            S7.SetDIntAt(buffer1, 4, Convert.ToInt32(TxtWrite4.Text)); //4. byte (for plc 1dint= 4 byte)
            S7.SetWordAt(buffer1, 8, Convert.ToUInt16(TxtWrite5.Text)); //8.byte (1 word = 2 byte)
            S7.SetDWordAt(buffer1, 10, Convert.ToUInt32(TxtWrite6.Text)); //10.byte (1 dword = 4 byte) =10+4=14 oldu buffer1da 14tü.
            int writeresult = Plc.DBWrite(2, 0, buffer1.Length, buffer1);
        }
    }

}
