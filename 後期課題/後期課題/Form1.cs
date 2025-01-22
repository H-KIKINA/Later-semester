using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace 後期課題
{
    public partial class FrmBorad : Form
    {
        private string[] cords = new string[] { "☆", "💧", "♪", "🐧", "🐈" };
        private const int yLen = 2;
        private const int xLen = 5;
        private Color cBack = SystemColors.Control;//裏
        private Color cCorrect = Color.Blue;//正解
        private Color cOpen = Color.Black;//今開いているところ
        private Button oBtn;
        int Count = 0;

        private bool is後半;
        public FrmBorad()
        {
            InitializeComponent();
            //２枚目を開くとfalse
            is後半 = false;

        }

        private void FrmBorad_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            List<int> val = new List<int>();

            while (true)
            {
                int a = r.Next(0, 10);

                //Listに１０個入っていいなければ入れる
                if (val.Contains(a) == false)
                {
                    val.Add(a);
                    Debug.WriteLine(a);
                }

                if (val.Count == 10)
                {
                    break;
                }
            }

            Control[] c;
            int Index = 0;
            Button btn = null;
            for (int y = 0; y < yLen; y++)
            {
                for (int x = 0; x < xLen; x++)
                {
                    c = this.Controls.Find("btn" + y.ToString() + x.ToString(), true);

                    btn = (Button)c[0];
                    btn.Text = cords[val[Index] % 5];
                    btn.ForeColor = cBack;
                    Index++;
                }
            }
        }
            private async void btn_Click(object sender, EventArgs e)
        {
            //後半の場合
            //正誤判定
            //ボタンの文字色＝背景色と一致している場合→裏側
            //            　＝赤　　の場合→正解している所
            //　　　　　　　＝黒　　の場合→そのターンで開いているところ
            Button btn = (Button)sender;
     
            if (btn.ForeColor == cOpen || btn.ForeColor ==cCorrect)
            {
                return;
            }
            btn.ForeColor = cOpen;

            if (is後半==false)
            {
                //前半の場合
                oBtn = btn;
                is後半 = true;
                return;
            }
            //正誤判定(後半)
            if (oBtn.Text == btn.Text)
            {
                oBtn.ForeColor = cCorrect;
                btn.ForeColor = cCorrect;
                Count = Count + 1;
                if (Count == 5)
                {
                  label1.Visible = true;
                }

            }
            else
            {
                await Task.Delay(500);//1秒後に裏に返す
                
                btn.ForeColor = cBack;
                    oBtn.ForeColor = cBack;
            }         
            oBtn = null;
            is後半 =!is後半;
        }
    }
}