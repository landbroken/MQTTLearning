# MQTTLearning
根据网络资料测试
MQTTNet库，2.7.5.0版本，需要.net4.5.2及以上
vs2015，C#

# 项目
包括SeverTest和ClientTest两个项目，都在MqttServerTest文件夹里。

发现有不知道怎么用示例程序的Σ(っ°Д°;)っ。。。。

1、clone或download项目
2、vs2015或以上版本打开MQTTLearning/MqttServerTest/MqttServerTest.sln
3、服务器端示例在SeverTest项目里面
4、客户端示例在ClientTest项目里面

目录一开始建的有点问题，调试稍微麻烦一点，懒得改了。
调试：
方法1）是vs里两个项目设为同时启动；
方法2）一端用生成的exe启动，一端在vs里用debug启动

一般可以直接打开的，万一vs有路径依赖问题：
1、如果项目路径依赖有问题，删掉重新添加一遍SeverTest.csproj和ClientTest.csproj
2、如果MQTTnet 库引用路径有问题，删掉从packages里面重新引用一遍，或者nuget里面添加一下

# 参考资料
MQTT协议中文版https://github.com/mcxiaoke/mqtt
MQTT入门https://blog.csdn.net/u012692537/article/details/80263150
使用 MQTTnet 实现 MQTT 通信示例https://blog.csdn.net/u012692537/article/details/80255010
