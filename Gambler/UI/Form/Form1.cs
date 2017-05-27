using Gambler.Module.HF;
using Gambler.Module.HF.Model;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler
{
    public partial class Form1 : Form
    {
        HFVerifyCode vedo;
        public Form1()
        {
            InitializeComponent();
            //vedo = new HFVerifyCode(Application.StartupPath + "\\Resources\\HF_trainData");
            //DownloadHFValid();
            //Test();
            TestHF();
            //new HFVerifyCode("").TrainData(Application.StartupPath + "\\Resources\\Train", Application.StartupPath + "\\Resources\\HF_trainData");

        }
        protected Dictionary<string, string> ConstructKeyValDict(params string[] data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int len = data.Length - 1;
            for (int i = 0; i < len; i += 2)
            {
                dict.Add(data[i], data[i + 1]);
            }
            return dict;
        }
        private void DownloadHFValid()
        {
            ThreadUtil.RunOnThread(() =>
            {
                Console.WriteLine("开始下载!");
                for (int i = 0; i < 30; i++)
                {
                    Dictionary<string, string> queryDict = ConstructKeyValDict("s", "" + new Random().NextDouble());

                    HttpUtil.Get<byte[]>(HFConfig.URL_VERICODE, null, null, queryDict,
                       (data) =>
                       {
                           return IOUtil.Read(data);
                       },
                       (statusCode, data, cookies) =>
                       {
                       if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                       {
                           string dir = Application.StartupPath + "\\Resources\\Download";
                           if (!Directory.Exists(dir))
                               Directory.CreateDirectory(dir);

                           using (FileStream fs = File.OpenWrite(dir + "\\" + i + ".jpg"))
                           {
                               fs.Write(data, 0, data.Length);
                               fs.Flush();
                           }
                           Console.WriteLine(vedo.ParseCode(data));
                               Console.WriteLine(i + " Finished!");
                           }

                       }, null);
                }
            });
        }

        private System.Threading.Timer _timer;
        public void TestHF()
        {
            ThreadUtil.RunOnThread(() =>
            {

                Console.WriteLine("开始执行");
                HFClient client = new HFClient("kaokkyyzz", "kaokkyyzz");
                client.Login(
                    (data) =>
                      {
                          Console.WriteLine("登录成功!");
                          client.LoginForH8((d) =>
                              {
                                  Console.WriteLine("H8登录成功!");
                                  client.GetOddData(
                                      (matchData) =>
                                      {
                                          Console.WriteLine("OddData: 获取Odd数据!");
                                          _timer = ThreadUtil.RunOnTimer((state) =>
                                          {
                                              Console.WriteLine("TimerCallback 执行中...");
                                              if (client.LiveMatchs != null)
                                              {
                                                  foreach (KeyValuePair<string, HFSimpleMatch> entry in client.LiveMatchs)
                                                  {
                                                      HFSimpleMatch m = entry.Value;
                                                      client.GetSpecLiveEvent(entry.Key,
                                                          (eventData) =>
                                                          {
                                                              Console.WriteLine(String.Format("联赛:{0}，{1}（主队） vs {2} (客队)，事件信息：{3}，下一事件ID：{4}",
                                                                  m.League, m.Home, m.Away, eventData.Info, eventData.EID));
                                                          }, null, null);
                                                  }
                                              } else
                                              {
                                                  Console.Write("当前直播列表为空");
                                                  _timer.Change(Timeout.Infinite, 0);
                                              }
                                          },
                                          null,
                                          500,
                                          1000);
                                      },
                                      (status, code, msg) =>
                                      {
                                          Console.WriteLine("OddData : Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                                      },
                                      (e) =>
                                      {
                                          Console.WriteLine("OddData : " + e.Message);
                                      });
                              },
                              (status, code, msg) =>
                              {
                                  Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                              },
                              (e) =>
                              {
                                  Console.WriteLine(e.Message);
                              });
                      },
                      (status, code, msg) =>
                      {
                          Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
                      },
                      (e) =>
                      {
                          Console.WriteLine(e.Message);
                      });
 //                 client.GetOddData(
 //                     (data) =>
 //                     {
 //                         Console.WriteLine("获取数据成功!");
 // 
 //                     },
 //                     (status, code, msg) =>
 //                     {
 //                         Console.WriteLine("Http: " + status + ", 错误码: " + code + ", 错误消息: " + msg);
 //                     },
 //                     (e) =>
 //                     {
 //                         Console.WriteLine(e.Message);
 //                     });

            });
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
