using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            ThreadUtil.RunOnThread(() => {

                XPJClient client = new XPJClient("smile", "cs006366129");
                Console.WriteLine("开始执行登录!");
                client.Login(
                    (data) =>
                    {
                        Console.WriteLine("执行登录成功");
                        client.GetUserInfo((d) =>
                        {
                            Console.WriteLine("当前金币余额: " + d.money);
                        },
                        (status, code, msg) =>
                        {
                            Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                        },
                        (e) =>
                        {
                            Console.WriteLine(e.Message);
                        });
//                         client.GetOddData(XPJClient.GameType.FT_TD_MN,
//                             (d) => {
//                                 Console.WriteLine(d);
//                             }, (status, code, msg) =>
//                             {
//                                 Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
//                             }, null);
                    },
                    (status, code, msg) =>
                    {
                        Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                    },
                    (e) =>
                    {
                        Console.WriteLine(e.Message);
                    });

            });
        }
    }
}
