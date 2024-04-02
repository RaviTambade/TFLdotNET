## MQTT – Message Queuing Telemetry Transport
MQTT (Message Queuing Telemetry Transport) is a messaging protocol for restricted low-bandwidth networks and extremely high-latency IoT devices. Since Message Queuing Telemetry Transport is specialized for low-bandwidth, high-latency environments, it is an ideal protocol for machine-to-machine (M2M) communication.

MQTT works on the publisher / subscriber principle and is operated via a central broker. This means that the sender and receiver have no direct connection. The data sources report their data via a publish and all recipients with interest in certain messages (“marked by the topic”) get the data delivered because they have registered as subscribers.


## 1. What does MQTT stand for?
MQTT stands for Message Queuing Telemetry Transport. It is an extremely simple and lightweight messaging protocol (subscribe and publish) designed for limited devices and networks with high latency, low bandwidth or unreliable networks. Its design principles are designed to reduce the network bandwidth and resource requirements of devices and ensure security of supply. In addition, these principles are advantageous for M2M (machine-to-machine) or IoT devices because battery performance and bandwidth are very important.


## 2. What is a MQTT topic?
With MQ Telemetry Transport, resource-constrained IoT devices can send or publish information on a specific topic to a server that acts as an MQTT message broker. The broker then transmits the information to those customers who have previously subscribed to the customer’s topic. To a human, a topic looks like a hierarchical file path. Customers can subscribe to a specific hierarchy level of a topic or use a wildcard character to subscribe to multiple levels.
The MQTT protocol is a good choice for wireless networks that have varying latency due to occasional bandwidth limitations or unreliable connections. If the connection from a subscribing client to a broker is interrupted, the broker buffers the messages and sends them to the subscriber when the subscriber is back online. If the connection from the Publishing Client to the Broker is disconnected without notification, the Broker can disconnect and send the Subscriber a cached message with instructions from the Publisher.

## 3. What is a MQTT broker?
The MQTT broker is the center of every Publish / Subscribe protocol. Depending on the implementation, a broker can manage up to thousands of simultaneously connected MQTT clients. The broker is responsible for receiving all messages, filtering the messages, determining who subscribed to each message and sending the message to those subscribed clients. The Broker also holds the sessions of all persistent clients, including subscriptions and missed messages. Another task of the Broker is the authentication and authorization of clients. Usually the broker is extensible, which facilitates custom authentification, authorization and integration with backend systems. Integration is especially important, because the Broker is often the component directly exposed on the Internet, serves many clients and has to forward messages to downstream analysis and processing systems. In short, the Broker is the central hub through which every message must be routed. It is therefore important that your broker is highly scalable, can be integrated into back-end systems, is easy to monitor and, of course, is fail-safe. MQTT brokers used in the industry are, for example, the HiveMQ MQTT Broker and EMQX. Cloud providers such as Microsoft and Amazon also provide their own MQTT brokers with Azure IoT Hub and AWS IoT Core.

## 4. What is a MQTT payload?
Messages are shared with other devices or software via a broker using MQTT. Every message has a topic, based on which the message can be processed further by the Broker. Additionally, each message contains a message content, the so-called payload. The MQTT payload is not bound to a certain structure and can be designed freely. However, it is helpful to specify a particular structure for the message content so that it can be read by other devices or software. Potential message structures are JSON, XML or OPC UA. A defined structure enables smooth internal communication as soon as all devices and software communicate with the same structure.


## 5. What is a MQTT client and how does it work?
All devices and software, such as the OPC Router, that are connected to the broker in some way are referred to as MQTT clients. A client can send messages to the broker (publish) and receive messages from the broker (subscribe). When sending a message to the broker, an MQTT topic must be specified, which can be used to further process the message. Messages can be sent with different Quality of Service (QoS):

Quality of Service 0: The client’s message is sent exactly once, regardless of whether it has arrived at the broker.
Quality of Service 1: The client’s message is sent over and over again until the broker responds with an confirmation of receipt. This can result in a message arriving at the broker multiple times.
Quality of Service 2: The client sends a message once and simultaneously ensures that it has arrived at the broker. Quality of Service 2 communication requires more bandwidth than Quality of Service 0 or 1.
At the same time, a client can subscribe to an MQTT topic at the broker so that the broker automatically receives all information that arrives at the broker with this MQTT topic. For example, plant1 / hall1 / temperature. Using wildcards, a client can receive multiple pieces of information from the broker. For example, it receives all entries from the list plant1 / hall1 with the MQTT topic plant1 / hall1 / #. With the topic plant1 / + / temperature all temperature entries from plant1 are sent.

Finally, an MQTT client has the “Last Will” function. If the connection to the broker is lost, a last message is sent so that the connection error is noticed by the broker and can be passed on to the user.


## 6. When should you use MQ Telemetry Transport and when not?
With Message Queuing Telemetry Transport, data is sent from a large number of machines to a single destination – the cloud – where the data can be analyzed, interpreted and forwarded.
The cloud hosts an MQTT broker – an intermediary between machines and other machines and/or people. And this is an important distinction, as the machines do not communicate directly with each other, but through the broker.
MQTT uses the concept of “themes” to organize its data, and a publish/subscribe model to communicate themes to other parties through the cloud.
For example: an air conditioning system sends (or publishes) data on the “health” of its compressors to the cloud. All interested parties with approved credentials – machine or human – can subscribe to this topic to receive the information.
Subscribers can be maintenance engineers (human), parts procurement systems (software/machine) or maintenance planning systems (software/machine).
Suddenly every aspect of a machine’s lifecycle is available for review, and this represents an exciting and profound opportunity to connect with this information to find defects, save costs, increase efficiency, and make planning for the Internet of Things.

## 7. What is MQ Telemetry Transport in IoT (Internet of Things) used for?
The word Topic refers to a UTF-8 string that the broker uses to filter messages for each connected client. The topic consists of one or more topic levels. Each topic level is separated by a forward slash (topic level separator). Compared to a message queue, MQTT topics are very simple. The client does not have to create the desired topic before publishing or subscribing to it. The broker accepts any valid topic without prior initialization. Note that each topic must contain at least one character and that the topic string allows spaces. Topics are case-sensitive. For example _myhome / temperature and _MyHome / Temperature are two different topics. Furthermore, the slash alone is a valid topic.
In general, you can name your MQTT topics as you wish. However, there is one exception: __ Topics that start with a $ symbol have a different purpose. __ These topics are not part of the subscription if you subscribe to the multi-level placeholder as a Topic (#). The $ symbol topics are reserved for internal statistics of the MQTT broker. Customers cannot post messages on these topics. Currently there is no official standard for such topics. Usually $ SYS / is used for all of the following information, but the implementation of brokers varies. A suggestion for $ SYS topics is the MQTT-GitHub wiki.

## 8. How to get started easily with MQ Telemetry Transport ?
In order to ensure an easy start, HiveMQ is advisable as an MQTT broker. HiveMQ is an open IoT standards based broker. As a result, it provides access to a wide range of MQTT clients. It is built for fast, efficient and reliable transfer of data to and from connected devices and servers.
The MQTT protocol provides a simple method of performing messaging using the publish/subscribe model. That makes it particularly suitable for IoT and cloud applications, such as low-power sensors or mobile devices like phones, embedded computers, or microcontrollers.
In conjunction with the OPC Router, connections can be easily queried. Download the HiveMQ broker here and test it yourself.
