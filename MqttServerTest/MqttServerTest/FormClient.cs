///开源库地址：https://github.com/chkr1011/MQTTnet
///对应文档：https://github.com/chkr1011/MQTTnet/wiki/Client

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Implementations;
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

namespace MqttServerTest
{
    public partial class FormClient : Form
    {
        private IMqttClient mqttClient = null;

        public FormClient()
        {
            InitializeComponent();

            Task.Run(async () => { await ConnectMqttServerAsync(); });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

            try
            {
                //Create TCP based options using the builder.
                var options1 = new MqttClientOptionsBuilder()
                    .WithClientId("Client1")
                    .WithTcpServer("192.168.88.3")
                    .WithCredentials("bud", "%spencer%")
                    .WithTls()
                    .WithCleanSession()
                    .Build();

                // Use TCP connection.
                var options2 = new MqttClientOptionsBuilder()
                    .WithTcpServer("192.168.88.3", 8222) // Port is optional
                    .Build();

                // Use secure TCP connection.
                var options3 = new MqttClientOptionsBuilder()
                    .WithTcpServer("192.168.88.3")
                    .WithTls()
                    .Build();

                //Create TCP based options using the builder.
                var options = new MqttClientOptionsBuilder()
                    .WithClientId("Client1")
                    .WithTcpServer("192.168.88.3", 8222)
                    .WithCredentials("username001", "psw001")
                    .WithTls()
                    .WithCleanSession()
                    .Build();

                // For .NET Framwork & netstandard apps:
                MqttTcpChannel.CustomCertificateValidationCallback = (x509Certificate, x509Chain, sslPolicyErrors, mqttClientTcpOptions) =>
                {
                    if (mqttClientTcpOptions.Server == "server_with_revoked_cert")
                    {
                        return true;
                    }

                    return false;
                };
                //
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
        }

        private async void BtnPublish_Click(object sender, EventArgs e)
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

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("topic_hello")
                .WithPayload("Hello World")
                .WithAtMostOnceQoS()
                .WithRetainFlag()
                .Build();

            await mqttClient.PublishAsync(message);
        }

        private async void BtnSubscribe_ClickAsync(object sender, EventArgs e)
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
            txtSubTopic.Enabled = false;
            btnSubscribe.Enabled = false;
        }

        private void MqttClient_Connected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText("已连接到MQTT服务器！" + Environment.NewLine);
            })));
        }

        private void MqttClient_Disconnected(object sender, EventArgs e)
        {
            Invoke((new Action(() =>
            {
                txtReceiveMessage.AppendText("已断开MQTT连接！" + Environment.NewLine);
            })));

            //Reconnecting
            Invoke((new Action(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    //Create TCP based options using the builder.
                    var options = new MqttClientOptionsBuilder()
                        .WithClientId("Client1")
                        .WithTcpServer("192.168.88.3", 8222)
                        .WithCredentials("username001", "psw001")
                        .WithTls()
                        .WithCleanSession()
                        .Build();

                    await mqttClient.ConnectAsync(options);
                }
                catch
                {
                    Invoke((new Action(() =>
                    {
                        txtReceiveMessage.AppendText("### RECONNECTING FAILED ###" + Environment.NewLine);
                    })));
                }
            })));

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
    }
}
