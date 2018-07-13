///开源库地址：https://github.com/chkr1011/MQTTnet
///对应文档：https://github.com/chkr1011/MQTTnet/wiki/Client

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Implementations;
using MQTTnet.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Test.Communication;

namespace MqttServerTest
{
    public partial class FormClient : Form
    {
        private IMqttClient mqttClient = null;
        private bool isReconnect = true;

        public FormClient()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void BtnPublish_Click(object sender, EventArgs e)
        {
            await Publish();
        }

        private async void BtnSubscribe_ClickAsync(object sender, EventArgs e)
        {
            await Subscribe();
        }

        private async Task Publish()
        {
            string topic = txtPubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("发布主题不能为空！");
                return;
            }

            string inputString = txtSendMessage.Text.Trim();
            //2.4.0版本的
            //var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            //mqttClient.PublishAsync(appMsg);

            ///qos=0，WithAtMostOnceQoS,消息的分发依赖于底层网络的能力。
            ///接收者不会发送响应，发送者也不会重试。消息可能送达一次也可能根本没送达。
            ///感觉类似udp
            ///QoS 1: 至少分发一次。服务质量确保消息至少送达一次。
            ///QoS 2: 仅分发一次
            ///这是最高等级的服务质量，消息丢失和重复都是不可接受的。使用这个服务质量等级会有额外的开销。
            ///
            ///例如，想要收集电表读数的用户可能会决定使用QoS 1等级的消息，
            ///因为他们不能接受数据在网络传输途中丢失
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(inputString)
                .WithAtMostOnceQoS()
                .WithRetainFlag(true)
                .Build();

            await mqttClient.PublishAsync(message);
        }

        private async Task Subscribe()
        {
            string topic = txtSubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("订阅主题不能为空！");
                return;
            }

            if (!mqttClient.IsConnected)
            {
                MessageBox.Show("MQTT客户端尚未连接！");
                return;
            }

            // Subscribe to a topic
            await mqttClient.SubscribeAsync(new TopicFilterBuilder()
                .WithTopic(topic)
                .WithAtMostOnceQoS()
                .Build()
                );

            //2.4.0
            //await mqttClient.SubscribeAsync(new List<TopicFilter> {
            //    new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
            //});

            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($"已订阅[{topic}]主题{Environment.NewLine}");
            })));
            //txtSubTopic.Enabled = false;
            //btnSubscribe.Enabled = false;
        }

        private async Task ConnectMqttServerAsync()
        {
            // Create a new MQTT client.

            if (mqttClient == null)
            {
                var factory = new MqttFactory();
                mqttClient = factory.CreateMqttClient();

                mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                mqttClient.Connected += MqttClient_Connected;
                mqttClient.Disconnected += MqttClient_Disconnected;
            }

            //非托管客户端
            try
            {
                ////Create TCP based options using the builder.
                //var options1 = new MqttClientOptionsBuilder()
                //    .WithClientId("client001")
                //    .WithTcpServer("192.168.88.3")
                //    .WithCredentials("bud", "%spencer%")
                //    .WithTls()
                //    .WithCleanSession()
                //    .Build();

                //// Use TCP connection.
                //var options2 = new MqttClientOptionsBuilder()
                //    .WithTcpServer("192.168.88.3", 8222) // Port is optional
                //    .Build();

                //// Use secure TCP connection.
                //var options3 = new MqttClientOptionsBuilder()
                //    .WithTcpServer("192.168.88.3")
                //    .WithTls()
                //    .Build();

                //Create TCP based options using the builder.
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(txtClientId.Text)
                    .WithTcpServer(txtIp.Text, Convert.ToInt32(txtPort.Text))
                    .WithCredentials(txtUsername.Text, txtPsw.Text)
                    //.WithTls()//服务器端没有启用加密协议，这里用tls的会提示协议异常
                    .WithCleanSession()
                    .Build();

                //// For .NET Framwork & netstandard apps:
                //MqttTcpChannel.CustomCertificateValidationCallback = (x509Certificate, x509Chain, sslPolicyErrors, mqttClientTcpOptions) =>
                //{
                //    if (mqttClientTcpOptions.Server == "server_with_revoked_cert")
                //    {
                //        return true;
                //    }

                //    return false;
                //};

                //2.4.0版本的
                //var options0 = new MqttClientTcpOptions
                //{
                //    Server = "127.0.0.1",
                //    ClientId = Guid.NewGuid().ToString().Substring(0, 5),
                //    UserName = "u001",
                //    Password = "p001",
                //    CleanSession = true
                //};

                await mqttClient.ConnectAsync(options);
            }
            catch (Exception ex)
            {
                Invoke((new Action(() =>
                {
                    txtReceiveMessage.AppendText($"连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
                })));
            }

            //托管客户端
            try
            {
                //// Setup and start a managed MQTT client.
                //var options = new ManagedMqttClientOptionsBuilder()
                //    .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                //    .WithClientOptions(new MqttClientOptionsBuilder()
                //        .WithClientId("Client_managed")
                //        .WithTcpServer("192.168.88.3", 8223)
                //        .WithTls()
                //        .Build())
                //    .Build();

                //var mqttClient = new MqttFactory().CreateManagedMqttClient();
                //await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("my/topic").Build());
                //await mqttClient.StartAsync(options);
            }
            catch (Exception)
            {

            }
        }

        private void MqttClient_Connected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.Clear();
                txtReceiveMessage.AppendText("已连接到MQTT服务器！" + Environment.NewLine);
            })));
        }

        private void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.Clear();
                DateTime curTime = new DateTime();
                curTime = DateTime.UtcNow;
                txtReceiveMessage.AppendText($">> [{curTime.ToLongTimeString()}]" );
                txtReceiveMessage.AppendText("已断开MQTT连接！" + Environment.NewLine);
            })));

            //Reconnecting
            if (isReconnect)
            {
                Invoke((new Action(() =>
                {
                    txtReceiveMessage.AppendText("正在尝试重新连接" + Environment.NewLine);
                })));
                
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(txtClientId.Text)
                    .WithTcpServer(txtIp.Text, Convert.ToInt32(txtPort.Text))
                    .WithCredentials(txtUsername.Text, txtPsw.Text)
                    //.WithTls()
                    .WithCleanSession()
                    .Build();
                Invoke((new Action(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    try
                    {
                        await mqttClient.ConnectAsync(options);
                    }
                    catch
                    {
                        txtReceiveMessage.AppendText("### RECONNECTING FAILED ###" + Environment.NewLine);
                    }
                })));
            }
            else
            {
                Invoke((new Action(() =>
                {
                    txtReceiveMessage.AppendText("已下线！" + Environment.NewLine);
                })));
            }
        }

        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> {"### RECEIVED APPLICATION MESSAGE ###"}{Environment.NewLine}");
            })));
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> Topic = {e.ApplicationMessage.Topic}{Environment.NewLine}");
            })));
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}{Environment.NewLine}");
            })));
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> QoS = {e.ApplicationMessage.QualityOfServiceLevel}{Environment.NewLine}");
            })));
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText($">> Retain = {e.ApplicationMessage.Retain}{Environment.NewLine}");
            })));
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            isReconnect = true;
            Task.Run(async () => { await ConnectMqttServerAsync(); });
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            isReconnect = false;
            Task.Run(async () => { await mqttClient.DisconnectAsync(); });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Dog dog = new Dog();
            dog.Alarm += new Dog.AlarmEventHandler(HostHandleAlarm);

            //当前时间，从2008年12月31日23:59:50开始计时
            DateTime now = new DateTime(2015, 12, 31, 23, 59, 55);
            DateTime midnight = new DateTime(2016, 1, 1, 0, 0, 0);
            var ttt = now.ToBinary();
            var ttt2 = ttt.ToString();
            var ree = Convert.ToInt64(ttt2);
            var rrrr = DateTime.FromBinary(ree);
            var clientId = System.Guid.NewGuid().ToString();
            System.Threading.Thread.Sleep(100);    //程序暂停一秒
            var clientId2 = System.Guid.NewGuid().ToString();

            now = DateTime.Now;
            midnight = now.AddSeconds(6);
            txtReceiveMessage.AppendText("now: " + now.ToString() + Environment.NewLine);
            txtReceiveMessage.AppendText("midnight: " + midnight.ToString() + Environment.NewLine);
            //等待午夜的到来
            txtReceiveMessage.AppendText("时间一秒一秒地流逝..." + Environment.NewLine);
            while (now < midnight)
            {
                string msg = "当前时间: " + DateTime.Now + Environment.NewLine;
                msg += "运行次数: " + (TaskCount++).ToString() + Environment.NewLine;
                txtReceiveMessage.AppendText(msg);
                Task.Run(async () => { await CompareNowMidnight(); });
                txtReceiveMessage.AppendText(m_msg+ m_msg2);
                System.Threading.Thread.Sleep(100);    //程序暂停
                now = DateTime.Now;                //时间增加
            }

            //午夜零点小偷到达,看门狗引发Alarm事件
            txtReceiveMessage.AppendText("\n月黑风高的午夜: " + now + Environment.NewLine);
            txtReceiveMessage.AppendText("小偷悄悄地摸进了主人的屋内..." + Environment.NewLine);
            dog.OnAlarm();
            txtReceiveMessage.AppendText("The End." + Environment.NewLine);
        }

        static string m_msg = "m_msg" + Environment.NewLine;
        static string m_msg2 = "m_msg2" + Environment.NewLine;
        static int TaskCount = 0;
        static int BeforeCount = 0;
        static int AlfterCount = 0;
        private async Task CompareNowMidnight()
        {
            m_msg = $">> TaskCount={TaskCount}+BeforeCount={BeforeCount++}+Time={DateTime.Now}{Environment.NewLine}";
            ///仅阻塞调用CompareNowMidnight的线程，由于本方法是async调用的，因此不影响主线程
            ///故可能会
            //System.Threading.Thread.Sleep(2900);    //程序暂停一秒
            await Task.Delay(TimeSpan.FromMilliseconds(3000));
            m_msg2 = ($">> TaskCount={TaskCount}+AlfterCount={AlfterCount++}+Time={DateTime.Now}{Environment.NewLine}");
        }

        void HostHandleAlarm(object sender, EventArgs e)
        {
            txtReceiveMessage.AppendText("\n狗报警: 有小偷进来了,汪汪~~~~~~~" + Environment.NewLine);
            txtReceiveMessage.AppendText("主人: 抓住了小偷！" + Environment.NewLine);
        }

        //事件发送者
        class Dog
        {
            //1.声明关于事件的委托；
            public delegate void AlarmEventHandler(object sender, EventArgs e);

            //2.声明事件；   
            public event AlarmEventHandler Alarm;

            //3.编写引发事件的函数；
            public void OnAlarm()
            {
                this.Alarm?.Invoke(this, new EventArgs());   //发出警报
            }
        }


    }
}
