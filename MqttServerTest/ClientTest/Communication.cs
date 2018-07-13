using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Communication
{
    /// <summary>
    /// client的尝试和form分离的草稿，假装自己是一个form
    /// </summary>
    public class Communication : Form
    {
        private MqttClientInfo info = MqttClientInfo.GetInstance();

        private IMqttClient mqttClient = null;
        public string ReceiveMsg = "";
        public string FlagMsg = "";

        public async Task Publish()
        {
            string topic = "host/datetime";

            if (string.IsNullOrEmpty(topic))
            {
                throw new Exception("发布主题不能为空！");
            }

            string inputString = DateTime.UtcNow.ToLongTimeString();

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

        public async Task Subscribe()
        {
            string topic = "slave/datetime";

            if (string.IsNullOrEmpty(topic))
            {
                throw new Exception("订阅主题不能为空！");
            }

            if (!mqttClient.IsConnected)
            {
                throw new Exception("MQTT客户端尚未连接！");
            }

            // Subscribe to a topic
            await mqttClient.SubscribeAsync(new TopicFilterBuilder()
                .WithTopic(topic)
                .WithAtMostOnceQoS()
                .Build()
                );

            FlagMsg += ($"已订阅[{topic}]主题{Environment.NewLine}");
        }

        public async Task ConnectMqttServerAsync()
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
                //Create TCP based options using the builder.
                var options = new MqttClientOptionsBuilder()
                    .WithClientId(info.ClientId)
                    .WithTcpServer("127.0.0.1", 8222)
                    .WithCredentials(info.Username, info.Password)
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

                await mqttClient.ConnectAsync(options);
            }
            catch (Exception ex)
            {
                FlagMsg+=($"连接到MQTT服务器失败！" + Environment.NewLine + ex.Message + Environment.NewLine);
            }
        }

        private void MqttClient_Connected(object sender, EventArgs e)
        {
            FlagMsg = ("已连接到MQTT服务器！" + Environment.NewLine);
        }

        private async void MqttClient_Disconnected(object sender, EventArgs e)
        {
            DateTime curTime = new DateTime();
            curTime = DateTime.UtcNow;
            FlagMsg = ($">> [{curTime.ToLongTimeString()}]");
            FlagMsg += ("已断开MQTT连接！" + Environment.NewLine);

            //Reconnecting
            if (info.IsReconnect)
            {
                FlagMsg += ("正在尝试重新连接" + Environment.NewLine);

                var options = new MqttClientOptionsBuilder()
                    .WithClientId(info.ClientId)
                    .WithTcpServer("127.0.0.1", 8222)
                    .WithCredentials(info.Username, info.Password)
                    //.WithTls()
                    .WithCleanSession()
                    .Build();
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch
                {
                    FlagMsg += ("### RECONNECTING FAILED ###" + Environment.NewLine);
                }
            }
            else
            {
                FlagMsg += ("已下线！" + Environment.NewLine);
            }
        }

        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            ReceiveMsg = ($">> {"### RECEIVED APPLICATION MESSAGE ###"}{Environment.NewLine}");
            ReceiveMsg += ($">> Topic = {e.ApplicationMessage.Topic}{Environment.NewLine}");
            ReceiveMsg += ($">> Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}{Environment.NewLine}");
            ReceiveMsg += ($">> QoS = {e.ApplicationMessage.QualityOfServiceLevel}{Environment.NewLine}");
            ReceiveMsg += ($">> Retain = {e.ApplicationMessage.Retain}{Environment.NewLine}");
        }

    }

    /// <summary>
    /// 基本信息
    /// </summary>
    /// <remarks>
    /// 试试从库源码里学的写法
    /// </remarks>
    public class MqttClientInfo
    {
        private bool _isReconnect = true;
        private string _username = "host001";
        private string _password = "psw001";
        private string _clientId = null;
        private int _port = 1883;//mqtt默认端口

        public bool IsReconnect
        {
            get
            {
                return _isReconnect;
            }

            set
            {
                _isReconnect = value;
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public string ClientId
        {
            get
            {
                return _clientId;
            }

            set
            {
                _clientId = value;
            }
        }

        public string Sever { get; set; }
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        //创建类的一个内部对象
        private static MqttClientInfo instance = new MqttClientInfo();


        //让构造函数为 private，这样该类就不会被实例化
        private MqttClientInfo() { }

        //获取唯一可用的对象
        public static MqttClientInfo GetInstance()
        {
            return instance;
        }

    }

    public class MqttClientInfoBuilder
    {
        private bool _isReconnect = true;
        private string _username = "host001";
        private string _password = "psw001";
        private string _clientId = null;

        public MqttClientInfoBuilder WithIsReconnect(bool isReconnect)
        {
            this._isReconnect = isReconnect;
            return this;
        }

        public MqttClientInfoBuilder WithClientInfo(string clientId, string username, string password)
        {
            this._clientId = clientId;
            this._username = username;
            this._password = password;
            return this;
        }

        public MqttClientInfo Build()
        {
            var ret=MqttClientInfo.GetInstance();
            ret.IsReconnect = this._isReconnect;
            ret.ClientId=this._clientId ?? System.Guid.NewGuid().ToString();//if=null，取右边的guid
            ret.Username = this._username;
            ret.Password = this._password;
            return ret;
        }
    }
}
