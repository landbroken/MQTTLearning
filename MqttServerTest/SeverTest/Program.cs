using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace MqttServerTest
{
    class Program
    {
        private static IMqttServer mqttServer = null;

        static void Main(string[] args)
        {
            StartMqttServer_2_7_5();

            MqttNetTrace.TraceMessagePublished += MqttNetTrace_TraceMessagePublished;
            new Thread(StartMqttServer).Start();

            while (true)
            {
                var inputString = Console.ReadLine().ToLower().Trim();

                if (inputString == "exit")
                {
                    mqttServer?.StopAsync();
                    Console.WriteLine("MQTT服务已停止！");
                    break;
                }
                else if (inputString == "clients")
                {
                    foreach (var item in mqttServer.GetConnectedClients())
                    {
                        Console.WriteLine($"客户端标识：{item.ClientId}，协议版本：{item.ProtocolVersion}");
                    }
                }
                else
                {
                    Console.WriteLine($"命令[{inputString}]无效！");
                }
            }
        }

        #region 2.4.0

        private static void StartMqttServer()
        {
            if (mqttServer == null)
            {
                try
                {
                    var options = new MqttServerOptions
                    {
                        ConnectionValidator = p =>
                        {
                            if (p.ClientId == "c001")
                            {
                                if (p.Username != "u001" || p.Password != "p001")
                                {
                                    return MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                                }
                            }

                            return MqttConnectReturnCode.ConnectionAccepted;
                        }
                    };

                    mqttServer = new MqttServerFactory().CreateMqttServer(options) as MqttServer;
                    mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                    mqttServer.ClientConnected += MqttServer_ClientConnected;
                    mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            mqttServer.StartAsync();
            Console.WriteLine("MQTT服务启动成功！");
        }

        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.Client.ClientId}]已连接，协议版本：{e.Client.ProtocolVersion}");
        }

        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.Client.ClientId}]已断开连接！");
        }

        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 负荷：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
        }

        private static void MqttNetTrace_TraceMessagePublished(object sender, MqttNetTraceMessagePublishedEventArgs e)
        {
            /*Console.WriteLine($">> 线程ID：{e.ThreadId} 来源：{e.Source} 跟踪级别：{e.Level} 消息: {e.Message}");

            if (e.Exception != null)
            {
                Console.WriteLine(e.Exception);
            }*/
        }

        #endregion

        #region 2.7.5

        private static async void StartMqttServer_2_7_5()
        {
            if (mqttServer == null)
            {
                // Configure MQTT server.
                // Configure MQTT server.
                var optionsBuilder = new MqttServerOptionsBuilder()
                    .WithConnectionBacklog(100)
                    .WithDefaultEndpointPort(8222);

                // Start a MQTT server.
                IMqttServer mqttServer = new MqttFactory().CreateMqttServer();
                await mqttServer.StartAsync(optionsBuilder.Build());
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                await mqttServer.StopAsync();
            }
        }

        private static void ValidatingMqttClients()
        {
            // Setup client validator.
            var options = new MqttServerOptions();
            
            options.ConnectionValidator = c =>
            {
                if (c.ClientId.Length < 5)
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                    return;
                }

                if (c.Username != "username001")
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }

                if (c.Password != "psw001")
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }

                c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
            };

            if (false)
            {
                //如果需要加密证书
                //Usingcertificate(ref options);
            }
        }

        private static void Usingcertificate(ref MqttServerOptions options)
        {
            var certificate = new X509Certificate(@"C:\certs\test\test.cer", "");
            options.TlsEndpointOptions.Certificate = certificate.Export(X509ContentType.Cert);
        }

        #endregion
    }
}